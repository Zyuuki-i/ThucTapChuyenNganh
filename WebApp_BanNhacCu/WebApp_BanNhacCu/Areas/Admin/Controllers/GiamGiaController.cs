using Microsoft.AspNetCore.Mvc;
using WebApp_BanNhacCu.Models;
using WebApp_BanNhacCu.Areas.Admin.MyModels;

namespace WebApp_BanNhacCu.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GiamGiaController : Controller
    {
        private ZyuukiMusicStoreContext db = new ZyuukiMusicStoreContext();
        public IActionResult Index()
        {
            return View(db.GiamGia.ToList());
        }

        public IActionResult formThemMa()
        {
            return View();
        }

        public IActionResult themMa(GiamGia gg)
        {
            db.GiamGia.Add(gg);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult formApDung(string id)
        {
            GiamGia gg = db.GiamGia.Find(id);
            List<NguoiDung> nd = db.NguoiDungs.ToList();
            ViewBag.DSNguoiDung = nd;
            return View(gg);
        }

        public IActionResult apDungMa(IFormCollection fc)
        {
            string magg = fc["MaGg"];
            foreach(NguoiDung item in db.NguoiDungs.ToList())
            {
                string ma=item.MaNd.ToString();
                ChiTietGiamGia ctgg = db.ChiTietGiamGia.FirstOrDefault(x => x.MaGg == magg && x.MaNd == item.MaNd);
                if (ctgg != null)
                    ctgg.Soluong += 1;
                else
                {
                    ctgg = new ChiTietGiamGia
                    {
                        MaGg = magg,
                        MaNd = item.MaNd,
                        Soluong = 1
                    };
                    db.ChiTietGiamGia.Add(ctgg);
                }
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
