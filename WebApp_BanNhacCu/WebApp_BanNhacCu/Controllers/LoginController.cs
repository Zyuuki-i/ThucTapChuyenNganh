using Microsoft.AspNetCore.Mvc;
using System.Text;
using WebApp_BanNhacCu.Models;

namespace WebApp_BanNhacCu.Controllers
{
    public class LoginController : Controller
    {
        ZyuukiMusicStoreContext db = new ZyuukiMusicStoreContext();
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult DangNhap(string email, string matkhau)
        {
            var tk = db.TaiKhoans.FirstOrDefault(t => t.Email == email && t.Matkhau == matkhau);
            if (tk == null)
            {
                TempData["ErrorLogin"] = "Sai email hoặc mật khẩu!";
                return RedirectToAction("Index");
            }

            TempData["SuccessLogin"] = "Đăng nhập thành công!";
            HttpContext.Session.SetString("UserEmail", email);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult DangKy(string Hoten, string Email,string sdt, string Matkhau, string XacnhanMatkhau)
        {
            if (Matkhau != XacnhanMatkhau)
            {
                TempData["ErrorRegister"] = "Mật khẩu nhập lại không khớp!";
                return RedirectToAction("Index");
            }

            var existing = db.TaiKhoans.FirstOrDefault(x => x.Email == Email);
            if (existing != null)
            {
                TempData["ErrorRegister"] = "Email đã tồn tại!";
                return RedirectToAction("Index");
            }

            TaiKhoan tk = new TaiKhoan
            {
                Email = Email,
                Matkhau = Matkhau,
                Sdt = sdt,
                MaVt = "VT01",
            };

            db.TaiKhoans.Add(tk);
            db.SaveChanges();

            TempData["SuccessRegister"] = "Đăng ký thành công, hãy đăng nhập!";
            return RedirectToAction("Index");
        }
    }
}
