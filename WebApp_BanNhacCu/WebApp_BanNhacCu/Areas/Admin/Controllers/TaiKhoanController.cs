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
                taiKhoans.Add(tk);
            }
            List<CNguoiDung> ds = taiKhoans.Select(t => CNguoiDung.chuyendoi(t)).ToList();
            return View(ds);
        }

        public IActionResult formThemTK()
        {
            ViewBag.DSVaitro = new SelectList(db.VaiTros.ToList(), "MaVt", "MaVt");
            return View();
        }
        public IActionResult themTK(CNhanVien x)
        {
            ViewBag.DSVaitro = new SelectList(db.VaiTros.ToList(), "MaVt", "MaVt");
            try
            {
                NhanVien tk = CNhanVien.chuyendoi(x);
                db.NhanViens.Add(tk);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Có lỗi khi thêm tài khoản!");
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

        public IActionResult formXoaNV(string id)
        {
            return RedirectToAction("Index"); 
        }

        public IActionResult xoaNV(string id)
        {
            return RedirectToAction("Index");//Chỉ đóng nhân viên không xóa
        }

        public IActionResult formSuaNV(string id) //Chưa tạo view
        {
            NhanVien? nv = db.NhanViens.Find(id);
            if (nv == null)
            {
                return RedirectToAction("Index");
            }
            CNhanVien ds = CNhanVien.chuyendoi(nv);
            ViewBag.DSVaitro = new SelectList(db.VaiTros.ToList(), "MaVt", "MaVt");
            return View(ds);
        }

        public IActionResult suaNV(CNhanVien x)
        {
            ViewBag.DSVaitro = new SelectList(db.VaiTros.ToList(), "MaVt", "MaVt");
            try
            {
                NhanVien nv = CNhanVien.chuyendoi(x);
                db.NhanViens.Update(nv);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Có lỗi khi sửa nhân viên!!!");
                return View("formSuaNV", x);
            }
        }
    }
}
