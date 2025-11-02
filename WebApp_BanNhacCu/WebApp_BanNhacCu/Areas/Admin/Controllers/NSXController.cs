using Microsoft.AspNetCore.Mvc;
using WebApp_BanNhacCu.Models;

namespace WebApp_BanNhacCu.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NSXController : Controller
    {
        ZyuukiMusicStoreContext db = new ZyuukiMusicStoreContext();
        public IActionResult Index()
        {
            return View(db.NhaSanXuats.ToList());
        }

        public IActionResult xoaNSX(string id)
        {
            NhaSanXuat nsx = db.NhaSanXuats.Find(id);
            if (nsx != null)
            {
                db.NhaSanXuats.Remove(nsx);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult formThemNSX()
        {
            return View();
        }

        public IActionResult themNSX(NhaSanXuat nsx)
        {
            db.NhaSanXuats.Add(nsx);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult formSuaNSX(string id)
        {
            NhaSanXuat nsx = db.NhaSanXuats.Find(id);
            return View(nsx);
        }

        public IActionResult suaNSX(NhaSanXuat nsx)
        {
            NhaSanXuat x = db.NhaSanXuats.Find(nsx.MaNsx);
            if (x != null)
            {
                x.MaNsx = nsx.MaNsx;
                x.Tennsx = nsx.Tennsx;
                x.Diachi = nsx.Diachi;
                x.Sdt = nsx.Sdt;
                x.Email = nsx.Email;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
