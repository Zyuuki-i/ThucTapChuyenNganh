using Microsoft.AspNetCore.Mvc;
using WebApp_BanNhacCu.Models;

namespace WebApp_BanNhacCu.Controllers
{
    public class SanPhamController : Controller
    {
        ZyuukiMusicStoreContext db = new ZyuukiMusicStoreContext();

        public IActionResult Index(List<SanPham> dsSP)
        {
            if(dsSP == null || dsSP.Count <= 0)
            {
                dsSP = db.SanPhams.ToList();
            }
            ViewBag.DsHinh = db.Hinhs
                                        .GroupBy(t => t.MaSp)
                                        .Select(g => g.First())
                                        .ToList();
            ViewBag.DsLoai = db.LoaiSanPhams.ToList();
            ViewBag.DsNSX = db.NhaSanXuats.ToList();
            return View(dsSP);
        }

        public IActionResult locLoai(string id)
        {
            List<SanPham> dsSP = db.SanPhams.Where(sp => sp.MaLoai == id).ToList();
            List<Hinh> dsHinh = new List<Hinh>();
            foreach (SanPham sp in dsSP)
            {
                Hinh hinh = db.Hinhs.FirstOrDefault(h => h.MaSp == sp.MaSp);
                if (hinh != null)
                {
                    dsHinh.Add(hinh);
                }
            }
            ViewBag.DsHinh = dsHinh
                                    .GroupBy(t => t.MaSp)
                                    .Select(g => g.First())
                                    .ToList();
            ViewBag.DsLoai = db.LoaiSanPhams.ToList();
            ViewBag.DsNSX = db.NhaSanXuats.ToList();
            return View("Index",dsSP);
        }

        public IActionResult locNSX(string id)
        {
            List<SanPham> dsSP = db.SanPhams.Where(sp => sp.MaNsx == id).ToList();
            List<Hinh> dsHinh = new List<Hinh>();
            foreach (SanPham sp in dsSP)
            {
                Hinh hinh = db.Hinhs.FirstOrDefault(h => h.MaSp == sp.MaSp);
                if (hinh != null)
                {
                    dsHinh.Add(hinh);
                }
            }
            ViewBag.DsHinh = dsHinh
                                    .GroupBy(t => t.MaSp)
                                    .Select(g => g.First())
                                    .ToList();
            ViewBag.DsLoai = db.LoaiSanPhams.ToList();
            ViewBag.DsNSX = db.NhaSanXuats.ToList();
            return View("Index", dsSP);
        }

        public IActionResult timKiem(string keyword)
        {
            List<SanPham> dsSP = db.SanPhams
                                    .Where(sp => sp.Tensp.Contains(keyword))
                                    .ToList();
            List<Hinh> dsHinh = new List<Hinh>();
            foreach (SanPham sp in dsSP)
            {
                Hinh hinh = db.Hinhs.FirstOrDefault(h => h.MaSp == sp.MaSp);
                if (hinh != null)
                {
                    dsHinh.Add(hinh);
                }
            }
            ViewBag.DsHinh = dsHinh
                                    .GroupBy(t => t.MaSp)
                                    .Select(g => g.First())
                                    .ToList();
            ViewBag.DsLoai = db.LoaiSanPhams.ToList();
            ViewBag.DsNSX = db.NhaSanXuats.ToList();
            return View("Index", dsSP);
        }
    }
}
