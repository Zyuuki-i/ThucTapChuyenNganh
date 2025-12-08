using Microsoft.AspNetCore.Mvc;
using WebApp_BanNhacCu.Models;


namespace WebApp_BanNhacCu.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoaiController : Controller
    {
        ZyuukiMusicStoreContext db = new ZyuukiMusicStoreContext();
        public IActionResult Index()
        {
            return View(db.LoaiSanPhams.ToList());
        }

        public IActionResult formXoaLoai(string id)
        {
            LoaiSanPham? l = db.LoaiSanPhams.Find(id);
            if (l == null)
            {
                return RedirectToAction("Index");
            }
            return View(l);
        }

        public IActionResult xoaLoai(string id)
        {
            LoaiSanPham? l = db.LoaiSanPhams.Find(id);
            if (l != null)
            {
                db.LoaiSanPhams.Remove(l);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult formThemLoai()
        {
            return View();
        }

        public IActionResult themLoai(LoaiSanPham l)
        {
            db.LoaiSanPhams.Add(l);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult formSuaLoai(string id)
        {
            LoaiSanPham? l = db.LoaiSanPhams.Find(id);
            return View(l);
        }

        public IActionResult suaLoai(LoaiSanPham l)
        {
            LoaiSanPham? x = db.LoaiSanPhams.Find(l.MaLoai);
            if (x != null)
            {
                x.MaLoai = l.MaLoai;
                x.Tenloai = l.Tenloai;
                x.Mota = l.Mota;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
