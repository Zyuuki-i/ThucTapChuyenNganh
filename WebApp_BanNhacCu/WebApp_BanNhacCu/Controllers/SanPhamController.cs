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
            ViewBag.KhachHangDanhGia = dg.Select(dg => db.KhachHangs.FirstOrDefault(kh => kh.MaKh == dg.MaKh)).ToList();
            ViewBag.Khohang = db.KhoHangs.FirstOrDefault(l => l.MaSp == sp.MaSp);

            List<SanPham> dsSP = db.SanPhams
                                        .Where(s => s.MaLoai == sp.MaLoai || s.MaNsx == sp.MaNsx && s.MaSp != sp.MaSp)
                                        .Take(4)
                                        .ToList();
            int vt = dsSP.FindIndex(x => x.MaSp == sp.MaSp);
            dsSP.RemoveAt(vt);
            dsSP.Insert(0, sp);
            
            List<Hinh> dsHinh = new List<Hinh>();
            foreach (SanPham sanPham in dsSP)
            {
                Hinh hinh = db.Hinhs.FirstOrDefault(h => h.MaSp == sanPham.MaSp);
                if (hinh != null)
                {
                    dsHinh.Add(hinh);
                }
            }
            ViewBag.DsHinh = dsHinh
                                    .GroupBy(t => t.MaSp)
                                    .Select(g => g.First())
                                    .ToList();
            return View(dsSP);
        }
    }
}
