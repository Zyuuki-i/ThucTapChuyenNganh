using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using WebApp_BanNhacCu.Areas.Admin.MyModels;
using WebApp_BanNhacCu.Models;

namespace WebApp_BanNhacCu.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TaiKhoanController : Controller
    {
        ZyuukiMusicStoreContext db = new ZyuukiMusicStoreContext();
        public IActionResult Index()
        {
            List<TaiKhoan> taiKhoans = new List<TaiKhoan>();
            foreach(TaiKhoan tk in db.TaiKhoans.ToList())
            {
                tk.MaVtNavigation = db.VaiTros.Find(tk.MaVt);
                taiKhoans.Add(tk);
            }
            List<CTaiKhoan> ds=taiKhoans.Select(t => CTaiKhoan.chuyendoi(t)).ToList();
            return View(ds);
        }
        public IActionResult formXoaTK(int id)
        {
            TaiKhoan tk = db.TaiKhoans.Find(id);
            if (tk == null)
            {
                return RedirectToAction("Index");
            }
            return View(CTaiKhoan.chuyendoi(tk));
        }

        public IActionResult xoaTK(int id)
        {
            TaiKhoan tk = db.TaiKhoans.Find(id);
            try { 
                db.TaiKhoans.Remove(tk);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Có lỗi khi xóa sản phẩm!!!");
                return View("formXoaTK", CTaiKhoan.chuyendoi(tk));
            }
           
        }

        public IActionResult formThemTK()
        {
            ViewBag.DSVaitro = new SelectList(db.VaiTros.ToList(), "MaVt", "MaVt");
            return View();
        }
        public IActionResult themTK(CTaiKhoan x)
        {
            ViewBag.DSVaitro = new SelectList(db.VaiTros.ToList(), "MaVt", "MaVt");
            if (ModelState.IsValid)
            {
                try
                {
                    TaiKhoan tk = CTaiKhoan.chuyendoi(x);
                    db.TaiKhoans.Add(tk);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Bi loi khi them tai khoan");
                }

            }
            return View("formThemTK");
        }

        public IActionResult formSuaTK(int id)
        {
            TaiKhoan? tk = db.TaiKhoans.Find(id);
            if (tk == null)
            {
                return RedirectToAction("Index");
            }
            CTaiKhoan ds = CTaiKhoan.chuyendoi(tk);
            ViewBag.DSVaitro = new SelectList(db.VaiTros.ToList(), "MaVt", "MaVt");
            return View(ds);
        }

        public IActionResult suaTK(CTaiKhoan x)
        {
            ViewBag.DSVaitro = new SelectList(db.VaiTros.ToList(), "MaVt", "MaVt");
            if (ModelState.IsValid)
            {
                try
                {
                    TaiKhoan tk = CTaiKhoan.chuyendoi(x);
                    db.TaiKhoans.Update(tk);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Có lỗi khi sửa sản phẩm!!!");
                    return View("formSuaTK", x);
                }
            }
            return View("formSuaTK", x);
        }
    }
}
