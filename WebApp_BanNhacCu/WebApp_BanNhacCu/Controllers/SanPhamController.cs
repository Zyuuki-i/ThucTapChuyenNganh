using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp_BanNhacCu.Models;

namespace WebApp_BanNhacCu.Controllers
{
    public class SanPhamController : Controller
    {
        ZyuukiMusicStoreContext db = new ZyuukiMusicStoreContext();

        public IActionResult Index(string keyword = "", string maloai="", string mansx="", int trang = 1)
        {
            List<SanPham> dsSP = new List<SanPham>();
            if (keyword != "")
            {
                dsSP = db.SanPhams.Where(sp => sp.Tensp.Contains(keyword)).ToList();
            } 
            else if (maloai != "")
            {
                dsSP = db.SanPhams.Where(sp => sp.MaLoai == maloai).ToList();
            }
            else if (mansx != "")
            {
                dsSP = db.SanPhams.Where(sp => sp.MaNsx == mansx).ToList();
            }
            else
            {
                dsSP = db.SanPhams.ToList();
            }

            int soSP = 6;

            int tongSP = dsSP.Count;
            int soTrang = (int)Math.Ceiling((double)tongSP / soSP);

            List<SanPham> sanPhams = dsSP.Skip((trang - 1) * soSP).Take(soSP).ToList();

            ViewBag.trangHienTai = trang;
            ViewBag.tongTrang = soTrang;
            ViewBag.DsHinh = db.Hinhs
                                        .GroupBy(t => t.MaSp)
                                        .Select(g => g.First())
                                        .ToList();
            ViewBag.DsLoai = db.LoaiSanPhams.ToList();
            ViewBag.DsNSX = db.NhaSanXuats.ToList();
            TempData["maloai"] = maloai;
            TempData["mansx"] = mansx;
            TempData["keyword"] = keyword;
            return View(sanPhams);
        }

        public IActionResult locLoai(string maloai)
        {
            return RedirectToAction("Index", new { maloai = maloai });
        }

        public IActionResult locNSX(string mansx)
        {
            return RedirectToAction("Index", new { mansx = mansx });
        }

        public IActionResult timKiem(string keyword)
        {
            return RedirectToAction("Index", new { keyword = keyword });
        }

        public IActionResult chiTiet(string id)
        {
            SanPham? sp = db.SanPhams.Find(id);
            if (sp == null)
            {
                return View("Index", null);
            }
            List<Hinh> dshinhChinh = db.Hinhs.Where(h => h.MaSp == id).ToList();
            List<DanhGia> dg = db.DanhGia.Where(dg => dg.MaSp == sp.MaSp).ToList();

            ViewBag.HinhChinh = dshinhChinh;
            ViewBag.DsLoai = db.LoaiSanPhams.ToList();
            ViewBag.DsNSX = db.NhaSanXuats.ToList();
            ViewBag.DanhGia = dg;
            ViewBag.KhachHangDanhGia = dg.Select(dg => db.NguoiDungs.FirstOrDefault(kh => kh.MaNd == dg.MaNd)).ToList();

            List<SanPham> dsSP = db.SanPhams
                                        .Where(s => s.MaLoai == sp.MaLoai || s.MaNsx == sp.MaNsx && s.MaSp != sp.MaSp)
                                        .Take(4)
                                        .ToList();
            List<Hinh> dsHinh = new List<Hinh>();
            if(dsSP != null && dsSP.Count > 0)
            {
                int vt = dsSP.FindIndex(x => x.MaSp == sp.MaSp);
                if (vt >= 0 && vt < dsSP.Count)
                    dsSP.RemoveAt(vt);
                dsSP.Insert(0, sp);

            foreach (SanPham sanPham in dsSP)
            {
                Hinh? hinh = db.Hinhs.FirstOrDefault(h => h.MaSp == sanPham.MaSp);
                if (hinh != null)
                {
                    dsHinh.Add(hinh);
                }
            }
            ViewBag.DsHinh = dsHinh
                                    .GroupBy(t => t.MaSp)
                                    .Select(g => g.First())
                                    .ToList();
            }
            return View(dsSP);
        }
    }
}
