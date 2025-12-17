using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp_BanNhacCu.Areas.Admin.MyModels;
using WebApp_BanNhacCu.Models;

namespace WebApp_BanNhacCu.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NhanVienController : Controller
    {
        ZyuukiMusicStoreContext db = new ZyuukiMusicStoreContext();
        public IActionResult Index()
        {
            List<NhanVien> dsNV = db.NhanViens.Where(t=>t.Trangthai == true).ToList();
            return View(dsNV);
        }

        public IActionResult loadNVVoHieu()
        {
            List<NhanVien> dsNV = db.NhanViens.Where(t => t.Trangthai == false).ToList();
            return PartialView(dsNV);
        }

        public IActionResult formThem()
        {
            ViewBag.dsVT = new SelectList(db.VaiTros.ToList(), "MaVt", "Tenvt");
            return View();
        }

        public IActionResult them(NhanVien x)
        {
            ViewBag.dsVT = new SelectList(db.VaiTros.ToList(), "MaVt", "Tenvt");

            if (string.IsNullOrWhiteSpace(x.Cccd) || x.Cccd.Length != 12)
            {
                ModelState.AddModelError("Cccd", "CCCD phải đúng 12 số.");
                return View("formThem", x);
            }
            var checkCCCD = db.NhanViens.FirstOrDefault(n => n.Cccd == x.Cccd);
            if (checkCCCD != null)
            {
                ModelState.AddModelError("Cccd", "CCCD đã tồn tại trong hệ thống.");
                return View("formThem", x);
            }

            try
            {
                x.Trangthai = true;
                db.NhanViens.Add(x);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Lỗi khi lưu dữ liệu." + ex.Message);
                return View("formThem", x);
            }

            return RedirectToAction("Index");
        }

        public IActionResult dongTaiKhoan(string id)
        {
            var nv = db.NhanViens.Find(id);
            if (nv == null)
            {
                return NotFound();
            }
            nv.Trangthai = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult moTaiKhoan(string id)
        {
            var nv = db.NhanViens.Find(id);
            if (nv == null)
            {
                return NotFound();
            }
            nv.Trangthai = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult formSua(string id)
        {
            return View(db.NhanViens.Find(id));
        }

        public IActionResult Sua(NhanVien nv)
        {
            NhanVien x = db.NhanViens.Find(nv.MaNv);
            if (x != null)
            {
                x.Tennv = nv.Tennv;
                x.Matkhau = nv.Matkhau;
                x.Phai = nv.Phai;
                x.Sdt = nv.Sdt;
                x.Email = nv.Email;
                x.Cccd = nv.Cccd;
                x.Diachi = nv.Diachi;
                x.MaVt = nv.MaVt;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    }
}
