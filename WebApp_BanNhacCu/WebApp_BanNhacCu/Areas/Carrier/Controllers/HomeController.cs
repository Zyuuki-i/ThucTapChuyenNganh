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
                dsDon = db.DonDatHangs
                    .Where(ddh => ddh.MaNv == nv.MaNv && ddh.Phuongthuc == "COD")
                    .Select(ddh => CDonDatHang.chuyenDoi(ddh))
                    .ToList();
            }
            ViewBag.dsDonDangGiao = dsDon.Where(d => d.Trangthai == "Đang giao hàng").ToList();
            ViewBag.dsDonMoiNhan = dsDon.Where(d => d.Trangthai == "Đã xác nhận").ToList();
            List<CDonDatHang> dsDonHoanThanh = dsDon
                .Where(d => d.Trangthai == "Hoàn thành" || d.Trangthai == "Đã giao hàng")
                .ToList();
            return View(dsDonHoanThanh);
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
                dsDon = db.DonDatHangs
                    .Where(ddh => ddh.MaNv == nv.MaNv && ddh.Phuongthuc == "VNPay")
                    .Select(ddh => CDonDatHang.chuyenDoi(ddh))
                    .ToList();
            }
            ViewBag.dsDonDangGiao = dsDon.Where(d => d.Trangthai == "Đang giao hàng").ToList();
            ViewBag.dsDonMoiNhan = dsDon.Where(d => d.Trangthai == "Đã xác nhận").ToList();
            List<CDonDatHang> dsDonHoanThanh = dsDon
                .Where(d => d.Trangthai == "Hoàn thành" || d.Trangthai == "Đã hủy")
                .ToList();
            return View(dsDonHoanThanh);
        }

        public IActionResult xacNhan(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            DonDatHang? ddh = db.DonDatHangs.FirstOrDefault(d => d.MaDdh == id);
            if (ddh == null)
            {
                return NotFound();
            }
            ddh.Trangthai = "Đang giao hàng";
            db.SaveChanges();
            if(ddh.Phuongthuc == "VNPay")
            {
                return RedirectToAction("donHangVNPay");
            }
            return RedirectToAction("donHangCOD");
        }

        public IActionResult hoanThanhVNPay(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            DonDatHang? ddh = db.DonDatHangs.FirstOrDefault(d => d.MaDdh == id);
            if (ddh == null)
            {
                return NotFound();
            }
            ddh.Trangthai = "Hoàn thành";
            db.SaveChanges();
            return RedirectToAction("donHangVNPay");
        }

        public IActionResult daGiaoCOD(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            DonDatHang? ddh = db.DonDatHangs.FirstOrDefault(d => d.MaDdh == id);
            if (ddh == null)
            {
                return NotFound();
            }
            ddh.Trangthai = "Đã giao hàng";
            db.SaveChanges();
            return RedirectToAction("donHangCOD");
        }

        public IActionResult hoanThanhCOD(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            DonDatHang? ddh = db.DonDatHangs.FirstOrDefault(d => d.MaDdh == id);
            if (ddh == null)
            {
                return NotFound();
            }
            ddh.Trangthai = "Hoàn thành";
            db.SaveChanges();
            return RedirectToAction("donHangCOD");
        }
    }
}
