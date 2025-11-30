using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Classification;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using WebApp_BanNhacCu.Areas.Admin.MyModels;
using WebApp_BanNhacCu.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WebApp_BanNhacCu.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SanPhamController : Controller
    {
        ZyuukiMusicStoreContext db = new ZyuukiMusicStoreContext();
        public IActionResult Index(string MaLoai = "", string MaNsx = "", string MaSp = "", int trang = 1)
        {
            List<CSanPham> ds = db.SanPhams.Select(t => CSanPham.chuyenDoi(t)).ToList();
            ViewBag.flag = false;
            if (!MaLoai.IsNullOrEmpty() && !MaNsx.IsNullOrEmpty())
            {
                if (MaLoai != "all" && MaNsx != "all")
                    ds = ds.Where(sp => sp.MaLoai == MaLoai && sp.MaNsx == MaNsx).ToList();
                else if (MaLoai != "" && MaNsx == "all")
                    ds = ds.Where(sp => sp.MaLoai == MaLoai).ToList();
                else if (MaLoai == "all" && MaNsx != "")
                    ds = ds.Where(sp => sp.MaNsx == MaNsx).ToList();
            }
            else if (!MaSp.IsNullOrEmpty())
            {
                CSanPham? sp = CSanPham.chuyenDoi(db.SanPhams.Find(MaSp));
                if (sp != null) {
                    int vt = ds.FindIndex(item => item.MaSp == sp.MaSp);
                    ds.RemoveAt(vt);
                    ds.Insert(0, sp);
                    ViewBag.FindMaSp = sp.MaSp;
                }
                else
                {
                    ds.Clear();
                    List<SanPham> dsSP = db.SanPhams
                                   .Where(sp => sp.Tensp.Contains(MaSp))
                                   .ToList();
                    foreach (SanPham s in dsSP)
                    {
                        CSanPham csp = CSanPham.chuyenDoi(s);
                        ds.Add(csp);
                    }
                }
            }
            ViewBag.DsLoai = new SelectList(db.LoaiSanPhams, "MaLoai", "Tenloai", MaLoai);
            ViewBag.DsNsx = new SelectList(db.NhaSanXuats, "MaNsx", "Tennsx", MaNsx);

            int soSP = 4;
            int tongSP = ds.Count;
            int soTrang = (int)Math.Ceiling((double)tongSP / soSP);

            List<CSanPham> sanPhams = ds.Skip((trang - 1) * soSP).Take(soSP).ToList();

            ViewBag.trangHienTai = trang;
            ViewBag.tongTrang = soTrang;

            var maSpList = sanPhams.Select(sp => sp.MaSp).ToList();
            List<Hinh> dshinh = db.Hinhs
                                        .Where(g => maSpList.Contains(g.MaSp))
                                        .GroupBy(t => t.MaSp)
                                        .Select(g => g.First())
                                        .ToList();
            ViewBag.DsHinh = dshinh;
            TempData["maloai"] = MaLoai;
            TempData["mansx"] = MaNsx;
            TempData["masp"] = MaSp;
            return View(sanPhams); 
        }

        public IActionResult timKiem(string MaSp)
        {
            return RedirectToAction("Index", new { MaLoai = (string)null, MaNsx = (string)null, MaSp = MaSp });
        }

        public IActionResult locSanPham(string MaLoai, string MaNsx)
        {
            if(MaLoai == "all" && MaNsx == "all")
                return RedirectToAction("Index", new { MaLoai = (string)null, MaNsx = (string)null, MaSp = (string)null });
            return RedirectToAction("Index", new { MaLoai = MaLoai, MaNsx = MaNsx, MaSp = (string)null });
        }

        public IActionResult formThemSP()
        {
            ViewBag.DSNsx = new SelectList(db.NhaSanXuats.ToList(), "MaNsx", "Tennsx");
            ViewBag.DSLoai = new SelectList(db.LoaiSanPhams.ToList(), "MaLoai", "Tenloai");
            return View();
        }

        public IActionResult themSanPham(CSanPham x, List<IFormFile> filehinh)
        {
            ViewBag.DSNsx = new SelectList(db.NhaSanXuats.ToList(), "MaNsx", "Tennsx");
            ViewBag.DSLoai = new SelectList(db.LoaiSanPhams.ToList(), "MaLoai", "TenLoai");
            if (ModelState.IsValid)
            {
                if (db.SanPhams.Find(x.MaSp) != null)
                {
                    TempData["MessageError_ThemSP"] = "Sản phẩm đã tồn tại!!!";
                    return RedirectToAction("formThemSP");
                }
                try
                {
                    SanPham sp = CSanPham.chuyenDoi(x);
                    db.SanPhams.Add(sp);

                    CapNhat cn = new CapNhat();
                    string email = HttpContext.Session.GetString("UserEmail") ?? "";
                    foreach (NhanVien n in db.NhanViens.ToList())
                    {
                        if (n.Email.Trim() == email.Trim())
                        {
                            cn.MaNv = n.MaNv;
                            break;
                        }
                    }
                    if (cn.MaNv == null)
                    {
                        TempData["MessageError_ThemSP"] = "Có lỗi khi thêm sản phẩm (không tìm thấy nhân viên)!!!";
                        return RedirectToAction("formThemSP");
                    }
                    cn.MaSp = sp.MaSp;
                    cn.Ngaycapnhat = DateTime.Now;
                    db.CapNhats.Add(cn);

                    if (filehinh != null && filehinh.Count > 0)
                    {
                        if(filehinh.Count > 10)
                        {
                            TempData["MessageError_ThemSP"] = "Chỉ được tải lên tối đa 10 hình ảnh cho mỗi sản phẩm!!!";
                            return RedirectToAction("formThemSP");
                        }
                        if (filehinh.Any(f => f.Length > 2 * 1024 * 1024))
                        {
                            TempData["MessageError_ThemSP"] = "Kích thước mỗi hình ảnh không được vượt quá 2MB!!!";
                            return RedirectToAction("formThemSP");
                        }
                        string thuMucAnh = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot","images","anhsp", sp.MaSp.Trim());
                        if (!Directory.Exists(thuMucAnh))
                        {
                            Directory.CreateDirectory(thuMucAnh);
                        }
                        foreach (IFormFile file in filehinh)
                        {
                            string chuoiRandom = Guid.NewGuid().ToString();
                            string tenfile = sp.MaSp +"_"+ chuoiRandom + Path.GetExtension(file.FileName);
                            string duongdan = Path.Combine(thuMucAnh, tenfile);
                            using (FileStream f = new FileStream(duongdan, FileMode.Create))
                            {
                                file.CopyTo(f);
                            }
                            Hinh hinh = new Hinh();
                            hinh.MaSp = sp.MaSp;
                            hinh.Tenhinh = tenfile;
                            db.Hinhs.Add(hinh);
                        }
                    }
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    TempData["MessageError_ThemSP"] = "Có lỗi khi thêm sản phẩm!!!";
                    return RedirectToAction("formThemSP");
                }
            }
            return View("formThemSP");
        }

        public IActionResult formXoaSP(string id)
        {
            SanPham? s = db.SanPhams.Find(id);
            if (s == null)
            {
                return RedirectToAction("Index");
            }
            CSanPham sp = CSanPham.chuyenDoi(s);
            return View(sp);
        }

        public IActionResult xoaSanPham(string id)
        {
            SanPham? sp = db.SanPhams.Find(id);
            if(sp == null)
            {
                TempData["MessageError_XoaSP"] = "Sản phẩm không tồn tại!!!";
                return RedirectToAction("formXoaSP", new { id = id });
            }
            if (db.ChiTietDonDatHangs.Any(t => t.MaSp == id))
            {
                TempData["MessageError_XoaSP"] = "Không thể xóa sản phẩm này!!!";
                return RedirectToAction("formXoaSP", new {id = id});
            }
            try
            {
                db.CapNhats.RemoveRange(db.CapNhats.Where(t => t.MaSp == sp.MaSp));
                db.SanPhams.Remove(sp);
                List<Hinh> dshinh = db.Hinhs.Where(t => t.MaSp == sp.MaSp).ToList();
                if(dshinh != null && dshinh.Count > 0)
                {
                    string thuMucAnh = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "anhsp", sp.MaSp.Trim());
                    if (Directory.Exists(thuMucAnh))
                    {
                        Directory.Delete(thuMucAnh, true);
                    }
                    db.Hinhs.RemoveRange(dshinh);
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["MessageError_XoaSP"] = "Có lỗi khi xóa sản phẩm!!!";
                return RedirectToAction("formXoaSP", new {id = id});
            }
        }

        public IActionResult formSuaSP(string id)
        {
            SanPham? sp = db.SanPhams.Find(id);
            if (sp == null)
            {
                return RedirectToAction("Index");
            }
            CSanPham csp = CSanPham.chuyenDoi(sp);
            ViewBag.DSNsx = new SelectList(db.NhaSanXuats.ToList(), "MaNsx", "MaNsx");
            ViewBag.DSLoai = new SelectList(db.LoaiSanPhams.ToList(), "MaLoai", "MaLoai");
            return View(csp);
        }

        public IActionResult suaSanPham(CSanPham x)
        {
            ViewBag.DSNsx = new SelectList(db.NhaSanXuats.ToList(), "MaNsx", "MaNsx");
            ViewBag.DSLoai = new SelectList(db.LoaiSanPhams.ToList(), "MaLoai", "MaLoai");
            try
            {
                SanPham sp = CSanPham.chuyenDoi(x);
                db.SanPhams.Update(sp);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["MessageError_SuaSP"] = "Có lỗi khi sửa sản phẩm!!!";
                return RedirectToAction("formSuaSP", new {id = x.MaSp});
            }
        }
        public IActionResult chiTietSP(string id)
        {
            SanPham? s = db.SanPhams.Find(id);
            if (s == null) return RedirectToAction("Index");

            CSanPham sp = CSanPham.chuyenDoi(s);
            sp.MaLoaiNavigation = db.LoaiSanPhams.Find(sp.MaLoai);
            sp.MaNsxNavigation = db.NhaSanXuats.Find(sp.MaNsx);

            CapNhat? cn = db.CapNhats
                            .Where(t => t.MaSp == sp.MaSp)
                            .OrderByDescending(t => t.Ngaycapnhat)
                            .FirstOrDefault();
            DateTime? ngayCapNhat = null;
            if (cn != null)
            {
                ngayCapNhat = cn.Ngaycapnhat;
            }
            ViewBag.NgayCapNhat = ngayCapNhat;

            List<Hinh> dsHinh = db.Hinhs.Where(t => t.MaSp == sp.MaSp).ToList();
            ViewBag.DsHinh = dsHinh;
            if (dsHinh != null && dsHinh.Count > 0)
                ViewBag.HinhDaiDien = sp.MaSp.Trim() + "/" + dsHinh[0].Tenhinh;
            else
                ViewBag.HinhDaiDien = "default.png";

            return View(sp);
        }

        public IActionResult chinhSuaKhoHang(string masp, int soluong)
        {
            SanPham? sp = db.SanPhams.Find(masp);
            if (sp == null)
            {
                TempData["MessageError_ChiTiet"] = "Có lỗi khi truy vấn sản phẩm!!!";
                return RedirectToAction("chiTietSP", new { id = masp });
            }
            try
            {
                CapNhat cn = new CapNhat();
                string email = HttpContext.Session.GetString("UserEmail") ?? "";
                foreach (NhanVien n in db.NhanViens.ToList())
                {
                    if (n.Email.Trim() == email.Trim())
                    {
                        cn.MaNv = n.MaNv;
                        break;
                    }
                }
                if (cn.MaNv == null)
                {
                    TempData["MessageError_ChiTiet"] = "Có lỗi khi cập nhật kho (không tìm thấy nhân viên)!!!";
                    return RedirectToAction("chiTietSP", new { id = masp });
                }
                cn.MaSp = sp.MaSp;
                cn.Ngaycapnhat = DateTime.Now;
                db.CapNhats.Add(cn);

                if (soluong < 0)
                    soluong = 0;
                sp.Soluongton += soluong;
                db.SanPhams.Update(sp);
                db.SaveChanges();
                return RedirectToAction("chiTietSP", new { id = masp });
            }
            catch (Exception)
            {
                TempData["MessageError_ChiTiet"] = "Có lỗi khi cập nhật kho hàng!!!";
                return RedirectToAction("chiTietSP", new { id = masp });
            }
        }

        public IActionResult chinhSuaAnh(string id)
        {
            SanPham? sp = db.SanPhams.Find(id);
            if (sp == null)
                return RedirectToAction("chiTietSP", new { id });
            ViewBag.SP = sp;
            List<CHinh> dsHinh = db.Hinhs.Where(t=>t.MaSp==sp.MaSp).Select(x=>CHinh.chuyenDoi(x)).ToList();
            return View(dsHinh);
        }

        public IActionResult xoaAnh(int id)
        {
            Hinh? hinh = db.Hinhs.Find(id);
            if (hinh == null)
            {
                TempData["MessageError_Anh"] = "Hình ảnh không tồn tại!!!";
                return RedirectToAction("chinhSuaAnh", new { id = id });
            }
            
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "anhsp", hinh.MaSp.Trim());
            string filePath = Path.Combine(folderPath, hinh.Tenhinh);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
                db.Hinhs.Remove(hinh);
                db.SaveChanges();
            }
            return RedirectToAction("chiTietSP", new { id = hinh.MaSp });
        }

        public IActionResult themAnh(string maSp, List<IFormFile> filehinh)
        {
            SanPham? sp = db.SanPhams.Find(maSp);
            if (sp == null) return RedirectToAction("Index", new { MaLoai = (string)null, MaNsx = (string)null, MaSp = (string)null });
            
            string thuMucAnh = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "anhsp", sp.MaSp.Trim());
            if (!Directory.Exists(thuMucAnh))
            {
                Directory.CreateDirectory(thuMucAnh);
            }
            if(filehinh == null || filehinh.Count == 0)
            {
                TempData["MessageError_Anh"] = "Vui lòng chọn hình ảnh để tải lên!!!";
                return RedirectToAction("chinhSuaAnh", new { id = maSp });
            }
            if (filehinh.Count + db.Hinhs.Count(t => t.MaSp == sp.MaSp) > 10)
            {
                TempData["MessageError_Anh"] = "Chỉ được tải lên tối đa 10 hình ảnh cho mỗi sản phẩm!!!";
                return RedirectToAction("chinhSuaAnh", new { id = maSp });
            }
            if (filehinh.Any(f => f.Length > 2 * 1024 * 1024))
            {
                TempData["MessageError_Anh"] = "Kích thước mỗi hình ảnh không được vượt quá 2MB!!!";
                return RedirectToAction("chinhSuaAnh", new { id = maSp });
            }
            foreach (IFormFile file in filehinh)
            {
                string chuoiRandom = Guid.NewGuid().ToString();
                string tenfile = sp.MaSp + "_" + chuoiRandom + Path.GetExtension(file.FileName);
                string duongdan = Path.Combine(thuMucAnh, tenfile);
                using (FileStream f = new FileStream(duongdan, FileMode.Create))
                {
                    file.CopyTo(f);
                }
                Hinh hinh = new Hinh();
                hinh.MaSp = sp.MaSp;
                hinh.Tenhinh = tenfile;
                db.Hinhs.Add(hinh);
            }
            db.SaveChanges();
            return RedirectToAction("chiTietSP", new { id = maSp });
        }
    }
}
