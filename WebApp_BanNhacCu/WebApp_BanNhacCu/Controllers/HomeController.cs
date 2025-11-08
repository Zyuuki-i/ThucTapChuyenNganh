using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApp_BanNhacCu.Models;

namespace WebApp_BanNhacCu.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        ZyuukiMusicStoreContext db = new ZyuukiMusicStoreContext();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewBag.DsHinh = db.Hinhs
                                        .GroupBy(t => t.MaSp)
                                        .Select(g => g.First())
                                        .ToList();
            List<SanPham> dsSP = new List<SanPham>();
            foreach (var sp in db.SanPhams.ToList())
            {
                sp.MaLoaiNavigation = db.LoaiSanPhams.Find(sp.MaLoai);
                sp.MaNsxNavigation = db.NhaSanXuats.Find(sp.MaNsx);
                dsSP.Add(sp);
                if (dsSP.Count >= 6)
                    break;
            }
            return View(dsSP);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
