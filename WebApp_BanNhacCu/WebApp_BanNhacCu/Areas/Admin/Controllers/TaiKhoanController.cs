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
            foreach (NguoiDung tk in db.NguoiDungs.ToList())
            {
                if(tk.Trangthai == true)
                    taiKhoans.Add(tk);
            }
            List<CNguoiDung> ds = taiKhoans.Select(t => CNguoiDung.chuyendoi(t)).ToList();
            return View(ds);
        }

        public IActionResult loadTKVoHieu()
        {
            List<NguoiDung> dsTK = db.NguoiDungs.Where(t => t.Trangthai == false).ToList();
            List<CNguoiDung> ds = dsTK.Select(t => CNguoiDung.chuyendoi(t)).ToList();
            return PartialView(ds);
        }

        public IActionResult formSuaTK(int id)
        {
            NguoiDung? tk = db.NguoiDungs.Find(id);
            if (tk == null)
            {
                return RedirectToAction("Index");
            }
            CNguoiDung ds = CNguoiDung.chuyendoi(tk);
            return View(ds);
        }

        public IActionResult suaTK(CNguoiDung x)
        {
            try
            {
                NguoiDung tk = CNguoiDung.chuyendoi(x);
                db.NguoiDungs.Update(tk);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Có lỗi khi sửa tài khoản!!!");
                return View("formSuaTK", x);
            }
        }

        public IActionResult dongTaiKhoan(int id)
        {
            var nv = db.NguoiDungs.Find(id);
            if (nv == null)
            {
                return NotFound();
            }
            nv.Trangthai = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult moTaiKhoan(int id)
        {
            var nv = db.NguoiDungs.Find(id);
            if (nv == null)
            {
                return NotFound();
            }
            nv.Trangthai = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
