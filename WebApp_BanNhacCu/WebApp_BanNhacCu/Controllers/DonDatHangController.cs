using Microsoft.AspNetCore.Mvc;
using WebApp_BanNhacCu.Models;

namespace WebApp_BanNhacCu.Controllers
{
    public class DonDatHangController : Controller
    {
        ZyuukiMusicStoreContext db = new ZyuukiMusicStoreContext();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult themVaoGio(string id)
        {
            SanPham? sp = db.SanPhams.Find(id);
            if(sp == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

    }
}
