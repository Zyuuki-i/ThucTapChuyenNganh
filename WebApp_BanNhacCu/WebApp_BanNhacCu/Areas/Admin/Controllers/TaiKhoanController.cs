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
            List<NguoiDung> taiKhoans = new List<NguoiDung>();
            foreach(NguoiDung tk in db.NguoiDungs.ToList())
            {
                tk.MaVtNavigation = db.VaiTros.Find(tk.MaVt);
                taiKhoans.Add(tk);
            }
            List<CNguoiDung> ds=taiKhoans.Select(t => CNguoiDung.chuyendoi(t)).ToList();
            return View(ds);
        }
        public IActionResult formXoaTK(int id)
        {
            NguoiDung tk = db.NguoiDungs.Find(id);
            if (tk == null)
            {
                return RedirectToAction("Index");
            }
            return View(CNguoiDung.chuyendoi(tk));
        }

        public IActionResult xoaTK(int id)
        {
            NguoiDung tk = db.NguoiDungs.Find(id);
            try { 
                db.NguoiDungs.Remove(tk);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Có lỗi khi xóa sản phẩm!!!");
                return View("formXoaTK", CNguoiDung.chuyendoi(tk));
            }
           
        }

        public IActionResult formThemTK()
        {
            ViewBag.DSVaitro = new SelectList(db.VaiTros.ToList(), "MaVt", "MaVt");
            return View();
        }
        public IActionResult themTK(CNguoiDung x)
        {
            ViewBag.DSVaitro = new SelectList(db.VaiTros.ToList(), "MaVt", "MaVt");
            try
            {
                NguoiDung tk = CNguoiDung.chuyendoi(x);
                db.NguoiDungs.Add(tk);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Bi loi khi them tai khoan");
                return View("formThemTK");
            }
        }

        public IActionResult formSuaTK(int id)
        {
            NguoiDung? tk = db.NguoiDungs.Find(id);
            if (tk == null)
            {
                return RedirectToAction("Index");
            }
            CNguoiDung ds = CNguoiDung.chuyendoi(tk);
            ViewBag.DSVaitro = new SelectList(db.VaiTros.ToList(), "MaVt", "MaVt");
            return View(ds);
        }

        public IActionResult suaTK(CNguoiDung x)
        {
            ViewBag.DSVaitro = new SelectList(db.VaiTros.ToList(), "MaVt", "MaVt");
            try
            {
                NguoiDung tk = CNguoiDung.chuyendoi(x);
                db.NguoiDungs.Update(tk);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Có lỗi khi sửa sản phẩm!!!");
                return View("formSuaTK", x);
            }
        }
    }
}
