using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using WebApp_BanNhacCu.Models;

namespace WebApp_BanNhacCu.Controllers
{
    public class TaiKhoanController : Controller
    {
        ZyuukiMusicStoreContext db = new ZyuukiMusicStoreContext();
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public IActionResult DangNhap(string email, string matkhau)
        {
            @ViewBag.Email = email;
            @ViewBag.Matkhau = matkhau;
            var tk = db.NguoiDungs.FirstOrDefault(t => t.Email == email && t.Matkhau == matkhau);
            if (tk == null)
            {
                TempData["ErrorLogin"] = "Sai email hoặc mật khẩu!";
                return View();
            }
            TempData["SuccessLogin"] = "Đăng nhập thành công!";
            HttpContext.Session.SetString("UserEmail", email); 
            return RedirectToAction("Index", "Home");
        }

        public IActionResult DangKy()
        {
            ViewBag.Hoten = TempData["Hoten"] ?? "";
            ViewBag.Email = TempData["Email"] ?? "";
            ViewBag.Sdt = TempData["Sdt"] ?? "";
            ViewBag.Matkhau = TempData["Matkhau"] ?? "";
            ViewBag.XacnhanMatkhau = TempData["XacnhanMatkhau"] ?? "";
            return View();
        }

        [HttpPost]
        public IActionResult DangKy(string Hoten, string Email, string sdt, string Matkhau, string XacnhanMatkhau)
        {
            ViewBag.Hoten = Hoten;
            ViewBag.Email = Email;
            ViewBag.Sdt = sdt;
            ViewBag.Matkhau = Matkhau;
            ViewBag.XacnhanMatkhau = XacnhanMatkhau;
            if (Matkhau != XacnhanMatkhau)
            {
                TempData["ErrorRegister"] = "Mật khẩu nhập lại không khớp!";
                return View();
            }
            if(Matkhau.Length < 8)
            {
                TempData["ErrorRegister"] = "Mật khẩu phải có ít nhất 8 ký tự!";
                return View();
            }
            NguoiDung tk = new NguoiDung
            {
                Email = Email,
                Matkhau = Matkhau,
                Sdt = sdt,
                MaVt = "VT01",
            };
            db.NguoiDungs.Add(tk);
            db.SaveChanges();
            TempData["SuccessRegister"] = "Đăng ký thành công, hãy đăng nhập!";
            HttpContext.Session.Remove("EmailVerified");
            return RedirectToAction("DangNhap");
        }

        public IActionResult QuenMatKhau()
        {
            ViewBag.Email = TempData["Email"] ?? "";
            return View();
        }
        [HttpPost]
        public IActionResult QuenMatKhau(string email,string Matkhau, string XacnhanMatkhau)
        {
            ViewBag.Email = email;
            NguoiDung tk = db.NguoiDungs.FirstOrDefault(t => t.Email == email);
            if (tk == null)
            {
                TempData["ErrorForgot"] = "Email không tồn tại!";
                return View();
            }
            if (Matkhau != XacnhanMatkhau)
            {
                TempData["ErrorForgot"] = "Mật khẩu nhập lại không khớp!";
                return View();
            }
            if (Matkhau.Length < 8)
            {
                TempData["ErrorForgot"] = "Mật khẩu phải có ít nhất 8 ký tự!";
                return View();
            }
            tk.Matkhau = Matkhau;
            db.NguoiDungs.Update(tk);
            db.SaveChanges();
            HttpContext.Session.Remove("EmailVerified");
            return RedirectToAction("DangNhap");
        }
        public IActionResult GuiXacMinhEmailDK(string Email, string Hoten, string sdt, string Matkhau, string XacnhanMatkhau)
        {
            TempData["Hoten"] = Hoten;
            TempData["Email"] = Email;
            TempData["Sdt"] = sdt;
            TempData["Matkhau"] = Matkhau;
            TempData["XacnhanMatkhau"] = XacnhanMatkhau;
            var existing = db.NguoiDungs.FirstOrDefault(x => x.Email == Email);
            if (existing != null)
            {
                TempData["ErrorRegister"] = "Email đã tồn tại!";
                return RedirectToAction("DangKy");
            }
            if (string.IsNullOrEmpty(Email))
            {
                TempData["ErrorRegister"] = "Email không hợp lệ!";
                return RedirectToAction("DangKy");
            }

            // Sinh mã OTP 6 chữ số
            var random = new Random();
            string otp = random.Next(100000, 999999).ToString();

            // Lưu OTP vào session, 5 phút
            HttpContext.Session.SetString("EmailOtp", otp);
            HttpContext.Session.SetString("EmailToVerify", Email);
            HttpContext.Session.SetString("OtpExpiration", DateTime.Now.AddMinutes(5).ToString());

            // Gửi email
            try
            {
                var smtpConfig = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build()
                    .GetSection("Smtp");

                using (var client = new SmtpClient(smtpConfig["Host"], int.Parse(smtpConfig["Port"])))
                {
                    client.EnableSsl = true;
                    client.Credentials = new NetworkCredential(smtpConfig["User"], smtpConfig["Pass"]);

                    var mail = new MailMessage();
                    mail.From = new MailAddress(smtpConfig["From"], smtpConfig["FromName"]);
                    mail.To.Add(Email);
                    mail.Subject = "Mã OTP xác minh email";
                    mail.Body = $"Mã OTP của bạn là: {otp}. Mã có hiệu lực trong 5 phút.";
                    mail.IsBodyHtml = false;

                    client.Send(mail);
                }

                TempData["SuccessRegister"] = "Mã OTP đã được gửi đến email!";
            }
            catch (Exception ex)
            {
                TempData["ErrorRegister"] = "Gửi email thất bại: " + ex.Message;
            }

            return RedirectToAction("DangKy");
        }

        [HttpPost]
        public IActionResult XacMinhEmailDK(string OtpInput, string Hoten, string Email, string sdt, string Matkhau, string XacnhanMatkhau)
        {
            TempData["Hoten"] = Hoten;
            TempData["Email"] = Email;
            TempData["Sdt"] = sdt;
            TempData["Matkhau"] = Matkhau;
            TempData["XacnhanMatkhau"] = XacnhanMatkhau;
            var savedOtp = HttpContext.Session.GetString("EmailOtp");
            var email = HttpContext.Session.GetString("EmailToVerify");
            var expiration = HttpContext.Session.GetString("OtpExpiration");

            if (savedOtp == null || email == null || expiration == null)
            {
                TempData["ErrorRegister"] = "OTP không tồn tại hoặc đã hết hạn!";
                return RedirectToAction("DangKy");
            }

            if (DateTime.Now > DateTime.Parse(expiration))
            {
                TempData["ErrorRegister"] = "OTP đã hết hạn!";
                return RedirectToAction("DangKy");
            }

            if (OtpInput == savedOtp)
            {
                HttpContext.Session.SetString("EmailVerified", "true");
                TempData["SuccessRegister"] = "Xác minh email thành công!";
            }
            else
            {
                TempData["ErrorRegister"] = "OTP không đúng!";
            }

            return RedirectToAction("DangKy");
        }
        public IActionResult GuiXacMinhEmailMK(string Email, string Hoten, string sdt, string Matkhau, string XacnhanMatkhau)
        {
            TempData["Hoten"] = Hoten;
            TempData["Email"] = Email;
            TempData["Sdt"] = sdt;
            TempData["Matkhau"] = Matkhau;
            TempData["XacnhanMatkhau"] = XacnhanMatkhau;
            var existing = db.NguoiDungs.FirstOrDefault(x => x.Email == Email);
            if (existing == null)
            {
                TempData["ErrorForgot"] = "Email không tồn tại!";
                return RedirectToAction("QuenMatKhau");
            }
            if (string.IsNullOrEmpty(Email))
            {
                TempData["ErrorForgot"] = "Email không hợp lệ!";
                return RedirectToAction("QuenMatKhau");
            }

            // Sinh mã OTP 6 chữ số
            var random = new Random();
            string otp = random.Next(100000, 999999).ToString();

            // Lưu OTP vào session, 5 phút
            HttpContext.Session.SetString("EmailOtp", otp);
            HttpContext.Session.SetString("EmailToVerify", Email);
            HttpContext.Session.SetString("OtpExpiration", DateTime.Now.AddMinutes(5).ToString());

            // Gửi email
            try
            {
                var smtpConfig = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build()
                    .GetSection("Smtp");

                using (var client = new SmtpClient(smtpConfig["Host"], int.Parse(smtpConfig["Port"])))
                {
                    client.EnableSsl = true;
                    client.Credentials = new NetworkCredential(smtpConfig["User"], smtpConfig["Pass"]);

                    var mail = new MailMessage();
                    mail.From = new MailAddress(smtpConfig["From"], smtpConfig["FromName"]);
                    mail.To.Add(Email);
                    mail.Subject = "Mã OTP xác minh email";
                    mail.Body = $"Mã OTP của bạn là: {otp}. Mã có hiệu lực trong 5 phút.";
                    mail.IsBodyHtml = false;

                    client.Send(mail);
                }

                TempData["SuccessForgot"] = "Mã OTP đã được gửi đến email!";
            }
            catch (Exception ex)
            {
                TempData["ErrorForgot"] = "Gửi email thất bại: " + ex.Message;
            }

            return RedirectToAction("QuenMatKhau");
        }
        [HttpPost]
        public IActionResult XacMinhEmailMK(string OtpInput, string Hoten, string Email, string sdt, string Matkhau, string XacnhanMatkhau)
        {
            TempData["Hoten"] = Hoten;
            TempData["Email"] = Email;
            TempData["Sdt"] = sdt;
            TempData["Matkhau"] = Matkhau;
            TempData["XacnhanMatkhau"] = XacnhanMatkhau;
            var savedOtp = HttpContext.Session.GetString("EmailOtp");
            var email = HttpContext.Session.GetString("EmailToVerify");
            var expiration = HttpContext.Session.GetString("OtpExpiration");

            if (savedOtp == null || email == null || expiration == null)
            {
                TempData["ErrorForgot"] = "OTP không tồn tại hoặc đã hết hạn!";
                return RedirectToAction("QuenMatKhau");
            }

            if (DateTime.Now > DateTime.Parse(expiration))
            {
                TempData["ErrorForgot"] = "OTP đã hết hạn!";
                return RedirectToAction("QuenMatKhau");
            }

            if (OtpInput == savedOtp)
            {
                HttpContext.Session.SetString("EmailVerified", "true");
                TempData["SuccessForgot"] = "Xác minh email thành công!";
            }
            else
            {
                TempData["ErrorForgot"] = "OTP không đúng!";
            }

            return RedirectToAction("QuenMatKhau");
        }
    }
}
