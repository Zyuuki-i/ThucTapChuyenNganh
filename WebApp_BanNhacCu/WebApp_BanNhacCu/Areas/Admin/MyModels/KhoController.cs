using Microsoft.AspNetCore.Mvc;
using WebApp_BanNhacCu.Areas.Admin.MyModels;
using WebApp_BanNhacCu.Models;

namespace WebApp_BanNhacCu.Areas.Admin.MyModels
{
    [Area("Admin")]
    public class KhoController : Controller
    {
        ZyuukiMusicStoreContext db = new ZyuukiMusicStoreContext();
        public IActionResult Index()
        {
            return View(db.KhoHangs.ToList());
        }

        public IActionResult formSua(string id)
        {
            KhoHang? kho = db.KhoHangs.Find(id);
            if (kho == null)
            {
                return RedirectToAction("Index");
            }
            return View(kho);
        }

        public IActionResult Sua(KhoHang kho)
        {
            KhoHang? k = db.KhoHangs.Find(kho.MaSp);
            if (k != null)
            {
                k.MaSp = kho.MaSp;
                k.Soluongton = kho.Soluongton;
                k.Ngaycapnhat = DateTime.Now; ;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
