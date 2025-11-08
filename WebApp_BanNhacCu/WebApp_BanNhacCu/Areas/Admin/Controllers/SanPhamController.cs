using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Classification;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebApp_BanNhacCu.Areas.Admin.MyModels;
using WebApp_BanNhacCu.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WebApp_BanNhacCu.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SanPhamController : Controller
    {
        ZyuukiMusicStoreContext db = new ZyuukiMusicStoreContext();
        public IActionResult Index(string MaLoai, string MaNsx, string MaSp)
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
                CSanPham sp = CSanPham.chuyenDoi(db.SanPhams.Find(MaSp));
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
            List<Hinh> dshinh = db.Hinhs
                                        .GroupBy(t => t.MaSp)
                                        .Select(g => g.First())
                                        .ToList();
            ViewBag.DsHinh = dshinh;
            return View(ds); 
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
            ViewBag.DSNsx = new SelectList(db.NhaSanXuats.ToList(), "MaNsx", "MaNsx");
            ViewBag.DSLoai = new SelectList(db.LoaiSanPhams.ToList(), "MaLoai", "MaLoai");
            return View();
        }

        public IActionResult themSanPham(CSanPham x, List<IFormFile> filehinh)
        {
            ViewBag.DSNsx = new SelectList(db.NhaSanXuats.ToList(), "MaNsx", "MaNsx");
            ViewBag.DSLoai = new SelectList(db.LoaiSanPhams.ToList(), "MaLoai", "MaLoai");
            if (ModelState.IsValid)
            {
                if (db.SanPhams.Find(x.MaSp) != null)
                {
                    ModelState.AddModelError("", "Sản phẩm đã tồn tại!!!");
                    return View("formThemSP");
                }
                try
                {
                    SanPham sp = CSanPham.chuyenDoi(x);
                    db.SanPhams.Add(sp);
                    if (filehinh != null && filehinh.Count > 0)
                    {
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
                            hinh.Url = tenfile;
                            db.Hinhs.Add(hinh);
                        }
                    }
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Có lỗi khi thêm sản phẩm!!!");
                    return View("formThemsp");
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
            if (db.KhoHangs.Any(t => t.MaSp == id) || db.KhoHangs.Any(t => t.MaSp == id) || db.ChiTietDonDatHangs.Any(t => t.MaSp == id))
            {
                ModelState.AddModelError("", "Không thể xóa sản phẩm này!!!");
                return View("formXoaSP", CSanPham.chuyenDoi(sp));
            }
            try
            {
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
                ModelState.AddModelError("", "Có lỗi khi xóa sản phẩm!!!");
                return View("formXoaSP", CSanPham.chuyenDoi(sp));
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
            if (ModelState.IsValid)
            {
                try
                {
                    SanPham sp = CSanPham.chuyenDoi(x);
                    db.SanPhams.Update(sp);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Có lỗi khi sửa sản phẩm!!!");
                    return View("formSuaSP", x);
                }
            }
            return View("formSuaSP", x);
        }
        public IActionResult chiTietSP(string id)
        {
            SanPham? s = db.SanPhams.Find(id);
            if (s == null) return RedirectToAction("Index");

            CSanPham sp = CSanPham.chuyenDoi(s);
            sp.MaLoaiNavigation = db.LoaiSanPhams.Find(sp.MaLoai);
            sp.MaNsxNavigation = db.NhaSanXuats.Find(sp.MaNsx);
            sp.KhoHang = db.KhoHangs.Find(sp.MaSp);

            List<Hinh> dsHinh = db.Hinhs.Where(t => t.MaSp == sp.MaSp).ToList();
            ViewBag.DsHinh = dsHinh;
            if (dsHinh != null)
                ViewBag.HinhDaiDien = sp.MaSp.Trim() + "/" + dsHinh[0].Url;
            else
                ViewBag.HinhDaiDien = "default.png";

            return View(sp);
        }
    }
}
