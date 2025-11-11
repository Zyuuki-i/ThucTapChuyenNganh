using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
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
            List<SanPham> dsSP = db.SanPhams
                                            .Include(sp => sp.MaLoaiNavigation)
                                            .Include(sp => sp.MaNsxNavigation)
                                            .Take(6).ToList();
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
