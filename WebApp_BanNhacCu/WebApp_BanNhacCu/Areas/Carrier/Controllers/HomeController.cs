using Microsoft.AspNetCore.Mvc;
using WebApp_BanNhacCu.Areas.Carrier.MyModels;
using WebApp_BanNhacCu.Models;

namespace WebApp_BanNhacCu.Areas.Carier.Controllers
{
    [Area("Carrier")]
    public class HomeController : Controller
    {
        ZyuukiMusicStoreContext db = new ZyuukiMusicStoreContext();
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult donHangCOD()
        {
            string? email = HttpContext.Session.GetString("UserEmail");
            if(email == null)
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            NhanVien? nv = db.NhanViens.FirstOrDefault(n => n.Email == email);
            List<CDonDatHang> dsDon = new List<CDonDatHang>();
            if (nv != null)
            {
                foreach (GiaoHang gh in db.GiaoHangs.ToList())
                {
                    if (gh.MaNv == nv.MaNv)
                    {
                        DonDatHang? ddh = db.DonDatHangs.Where(t => t.MaDdh == gh.MaDdh && t.Phuongthuc == "COD" && gh.Trangthai != "Giao thất bại").FirstOrDefault();
                        if(ddh != null) dsDon.Add(CDonDatHang.chuyenDoi(ddh));
                    }
                }
            }
            ViewBag.dsDonDangGiao = dsDon.ToList().Join(
                db.GiaoHangs.Where(t=>t.Trangthai=="Đang giao hàng"),
                dh => dh.MaDdh,
                gh => gh.MaDdh,
                (dh,gh) => dh
            ).ToList();
            dsDon = dsDon.ToList().Join(
                db.GiaoHangs.Where(t => t.Trangthai == "Chờ giao hàng"),
                dh => dh.MaDdh,
                gh => gh.MaDdh,
                (dh, gh) => dh
            ).ToList();
            return View(dsDon);
        }

        public IActionResult donHangVNPay()
        {
            string? email = HttpContext.Session.GetString("UserEmail");
            if (email == null)
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            NhanVien? nv = db.NhanViens.FirstOrDefault(n => n.Email == email);
            List<CDonDatHang> dsDon = new List<CDonDatHang>();
            if (nv != null)
            {
                foreach (GiaoHang gh in db.GiaoHangs.ToList())
                {
                    if (gh.MaNv == nv.MaNv)
                    {
                        DonDatHang? ddh = db.DonDatHangs.Where(t => t.MaDdh == gh.MaDdh && t.Phuongthuc == "VNPay" && gh.Trangthai != "Giao thất bại").FirstOrDefault();
                        if (ddh != null) dsDon.Add(CDonDatHang.chuyenDoi(ddh));
                    }
                }
            }
            ViewBag.dsDonDangGiao = dsDon.ToList().Join(
                db.GiaoHangs.Where(t => t.Trangthai == "Đang giao hàng"),
                dh => dh.MaDdh,
                gh => gh.MaDdh,
                (dh, gh) => dh
            ).ToList();
            dsDon = dsDon.ToList().Join(
                db.GiaoHangs.Where(t => t.Trangthai == "Chờ giao hàng"),
                dh => dh.MaDdh,
                gh => gh.MaDdh,
                (dh, gh) => dh
            ).ToList();
            return View(dsDon);
        }

        public IActionResult donDaGiaoCOD()
        {
            string? email = HttpContext.Session.GetString("UserEmail");
            if (email == null)
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            NhanVien? nv = db.NhanViens.FirstOrDefault(n => n.Email == email);
            List<CDonDatHang> dsDon = new List<CDonDatHang>();
            if (nv != null)
            {
                foreach (GiaoHang gh in db.GiaoHangs.ToList())
                {
                    if (gh.MaNv == nv.MaNv)
                    {
                        DonDatHang? ddh = db.DonDatHangs.Where(t => t.MaDdh == gh.MaDdh && t.Phuongthuc == "COD" && gh.Trangthai == "Đã giao hàng").FirstOrDefault();
                        if (ddh != null) dsDon.Add(CDonDatHang.chuyenDoi(ddh));
                    }
                }
            }

            return View(dsDon);
        }

        public IActionResult donDaGiaoVNPay()
        {
            string? email = HttpContext.Session.GetString("UserEmail");
            if (email == null)
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            NhanVien? nv = db.NhanViens.FirstOrDefault(n => n.Email == email);
            List<CDonDatHang> dsDon = new List<CDonDatHang>();
            if (nv != null)
            {
                foreach (GiaoHang gh in db.GiaoHangs.ToList())
                {
                    if (gh.MaNv == nv.MaNv)
                    {
                        DonDatHang? ddh = db.DonDatHangs.Where(t => t.MaDdh == gh.MaDdh && t.Phuongthuc == "VNPay" && t.Trangthai == "Đã giao hàng").FirstOrDefault();
                        if (ddh != null) dsDon.Add(CDonDatHang.chuyenDoi(ddh));
                    }
                }
            }

            return View(dsDon);
        }

        public IActionResult giaoHang(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            GiaoHang? gh = new GiaoHang();
            foreach(GiaoHang g in db.GiaoHangs.ToList())
            {
                if (g.MaDdh == id) gh = g;
            }
            if (gh == null)
            {
                return RedirectToAction("Index");
            }
            gh.Trangthai = "Đang giao hàng";
            db.Update(gh);
            db.SaveChanges();
            if(gh.Tongthu == 0)
            {
                return RedirectToAction("donHangVNPay");
            }
            return RedirectToAction("donHangCOD");
        }

        public IActionResult daGiao(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            DonDatHang? ddh = db.DonDatHangs.FirstOrDefault(d => d.MaDdh == id);
            GiaoHang? gh = new GiaoHang();
            foreach (GiaoHang g in db.GiaoHangs.ToList())
            {
                if (g.MaDdh == id) gh = g;
            }
            if (gh == null || ddh == null)
            {
                return RedirectToAction("Index");
            }
            gh.Trangthai = "Đã giao hàng";
            ddh.Trangthai = "Chờ xác nhận";
            db.Update(gh);
            db.Update(ddh);
            db.SaveChanges();
            if (gh.Tongthu == 0)
            {
                return RedirectToAction("donHangVNPay");
            }
            return RedirectToAction("donHangCOD");
        }

        public IActionResult giaoThatBai(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            DonDatHang? ddh = db.DonDatHangs.FirstOrDefault(d => d.MaDdh == id);
            GiaoHang? gh = new GiaoHang();
            foreach (GiaoHang g in db.GiaoHangs.ToList())
            {
                if (g.MaDdh == id) gh = g;
            }
            if (gh == null || ddh == null)
            {
                return RedirectToAction("Index");
            }
            gh.Trangthai = "Giao thất bại";
            ddh.Trangthai = "Đang xử lý";
            db.Update(gh);
            db.Update(ddh);
            db.SaveChanges();
            if (gh.Tongthu == 0)
            {
                return RedirectToAction("donHangVNPay");
            }
            return RedirectToAction("donHangCOD");
        }

    }
}
