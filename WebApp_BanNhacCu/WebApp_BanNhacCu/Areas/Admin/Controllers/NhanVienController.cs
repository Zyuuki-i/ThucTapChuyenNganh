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
            return View(db.NhanViens.ToList());
        }

        public IActionResult formThem()
        {
            ViewBag.dsVT = new SelectList(db.VaiTros.ToList(), "MaVt", "Tenvt");
            return View();
        }

        public IActionResult them(NhanVien x)
        {
            ViewBag.dsVT = new SelectList(db.VaiTros.ToList(), "MaVt", "Tenvt");
            var checkCCCD = db.NhanViens.FirstOrDefault(n => n.Cccd == x.Cccd);
            if (checkCCCD != null)
            {
                ModelState.AddModelError("Cccd", "CCCD đã tồn tại trong hệ thống hoặc chưa đủ 12 ký tự");
                return View("formThem", x);
            }
            try
            {
                db.NhanViens.Add(x);
                db.SaveChanges();
            }
            catch
            {
                ModelState.AddModelError("", "Lỗi");
                return View("formThem", x);
            }

            return RedirectToAction("Index");
        }
    }
}
