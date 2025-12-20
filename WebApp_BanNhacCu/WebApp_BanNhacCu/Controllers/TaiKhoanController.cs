using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using WebApp_BanNhacCu.Models;
using WebApp_BanNhacCu.Areas.Admin.MyModels;

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
                var nv = db.NhanViens.FirstOrDefault(t => t.Email == email && t.Matkhau == matkhau);
                if (nv != null)
                {
                    if (nv.Trangthai == true)
                    {
                        HttpContext.Session.SetString("UserEmail", email);
                        HttpContext.Session.SetString("UserRole", nv.MaVt.Trim());
                        string areas = nv.MaVt.Trim() == "Carrier" ? "Carrier" : "Admin";
                        return RedirectToAction("Index", "Home", new { area = areas });
                    }
                    TempData["ErrorLogin"] = "Tài khoản nhân viên đã bị khóa!";
                }
                else
                {
                    TempData["ErrorLogin"] = "Email hoặc mật khẩu không đúng!";
                }
            }
            else
            {
                if (tk.Trangthai == true)
                {
                    HttpContext.Session.SetString("UserRole", "");
                    HttpContext.Session.SetString("UserEmail", email);
                    HttpContext.Session.SetInt32("UserId", tk.MaNd);
                    HttpContext.Session.SetString("UserName", tk.Tennd);
                    return RedirectToAction("Index", "Home");
                }
                TempData["ErrorLogin"] = "Tài khoản người dùng đã bị khóa!";
            }
            return View();
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
            if (Matkhau.Length < 8)
            {
                TempData["ErrorRegister"] = "Mật khẩu phải có ít nhất 8 ký tự!";
                return View();
            }
            NguoiDung tk = new NguoiDung
            {
                Email = Email,
                Matkhau = Matkhau,
                Sdt = sdt,
                Tennd = Hoten,
                Diachi = "",
                Hinh = ""
            };
            db.NguoiDungs.Add(tk);
            db.SaveChanges();
            TempData["SuccessRegister"] = "Đăng ký thành công, hãy đăng nhập!";
            HttpContext.Session.Remove("EmailVerified");
            return RedirectToAction("DangNhap");
        }

        public IActionResult DangXuat()
        {
            HttpContext.Session.Remove("UserRole");
            HttpContext.Session.Remove("UserEmail");
            HttpContext.Session.Remove("UserId");
            HttpContext.Session.Remove("UserName");
            return RedirectToAction("DangNhap");
        }

        public IActionResult QuenMatKhau()
        {
            ViewBag.Email = TempData["Email"] ?? "";
            return View();
        }
        [HttpPost]
        public IActionResult QuenMatKhau(string email, string Matkhau, string XacnhanMatkhau)
        {
            ViewBag.Email = email;
            NguoiDung? tk = db.NguoiDungs.FirstOrDefault(t => t.Email == email);
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

        public IActionResult XemTaiKhoan(string email)
        {
            NguoiDung? tk = db.NguoiDungs.FirstOrDefault(t => t.Email == email);
            if (tk != null)
                return View(tk);
            else
            {
                NhanVien? nv = db.NhanViens.FirstOrDefault(t => t.Email == email);
                if (nv != null)
                {
                    nv.MaVtNavigation = db.VaiTros.Find(nv.MaVt) ?? new VaiTro();
                    return View("XemTaiKhoanNV", nv);
                }
            }
            return RedirectToAction("DangNhap");
        }

        private void GuiOtpEmail(string email, string tempDataSuccessKey, string tempDataErrorKey, string subject, string bodyTemplate)
        {
            try
            {
                var random = new Random();
                string otp = random.Next(100000, 999999).ToString();

                // Lưu OTP vào session
                HttpContext.Session.SetString("EmailOtp", otp);
                HttpContext.Session.SetString("EmailToVerify", email);
                HttpContext.Session.SetString("OtpExpiration", DateTime.Now.AddMinutes(5).ToString());

                // Lấy cấu hình SMTP
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
                    mail.To.Add(email);
                    mail.Subject = subject;
                    mail.Body = string.Format(bodyTemplate, otp);
                    mail.IsBodyHtml = false;

                    client.Send(mail);
                }

                TempData[tempDataSuccessKey] = "Mã OTP đã được gửi đến email!";
            }
            catch (Exception ex)
            {
                TempData[tempDataErrorKey] = "Gửi email thất bại: " + ex.Message;
            }
        }
        private bool XacMinhOtp(string otpInput, string tempDataSuccessKey, string tempDataErrorKey)
        {
            var savedOtp = HttpContext.Session.GetString("EmailOtp");
            var expiration = HttpContext.Session.GetString("OtpExpiration");

            if (savedOtp == null || expiration == null)
            {
                TempData[tempDataErrorKey] = "OTP không tồn tại!";
                return false;
            }

            if (DateTime.Now > DateTime.Parse(expiration))
            {
                TempData[tempDataErrorKey] = "OTP đã hết hạn!";
                return false;
            }

            if (otpInput == savedOtp)
            {
                HttpContext.Session.SetString("EmailVerified", "true");
                TempData[tempDataSuccessKey] = "Xác minh email thành công!";
                return true;
            }
            else
            {
                TempData[tempDataErrorKey] = "OTP không đúng!";
                return false;
            }
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

            GuiOtpEmail(Email, "SuccessRegister", "ErrorRegister", "Mã OTP xác minh email", "Mã OTP của bạn là: {0}. Mã có hiệu lực trong 5 phút.");

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

            XacMinhOtp(OtpInput, "SuccessRegister", "ErrorRegister");

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


            GuiOtpEmail(Email, "SuccessForgot", "ErrorForgot", "Mã OTP xác minh email", "Mã OTP của bạn là: {0}. Mã có hiệu lực trong 5 phút.");

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
            XacMinhOtp(OtpInput, "SuccessForgot", "ErrorForgot");
            return RedirectToAction("QuenMatKhau");
        }

        public IActionResult lichSuDDH(int id)
        {
            List<CDonDatHang> ds = new List<CDonDatHang>();
            foreach (DonDatHang ddh in db.DonDatHangs.ToList())
            {
                if (ddh.MaNd == id)
                    ds.Add(CDonDatHang.chuyenDoi(ddh));
            }
            return View(ds);
        }

        public IActionResult formCapNhatTK(int id)
        {
            NguoiDung? tk = db.NguoiDungs.Find(id);
            return View(tk);
        }
        [HttpPost]
        public IActionResult CapNhatTK(NguoiDung nd, IFormFile hinh)
        {
            var tk = db.NguoiDungs.Find(nd.MaNd);
            if (tk == null) return NotFound();

            tk.Tennd = nd.Tennd;
            tk.Matkhau = nd.Matkhau;
            tk.Sdt = nd.Sdt;
            tk.Diachi = nd.Diachi;
            tk.Phuongxa = nd.Phuongxa;
            tk.Tinhthanh = nd.Tinhthanh;

            if (hinh != null)
            {
                if (hinh.Length > 1024 * 1024)
                {
                    TempData["MessageError_KhachHang"] = "Dung lượng file không được vượt quá 1MB.";
                    return RedirectToAction("formCapNhatTK", new { id = nd.MaNd });
                }

                var cacDinhDangChoPhep = new[] { ".jpg", ".jpeg", ".png" };
                var duoiFile = Path.GetExtension(hinh.FileName).ToLowerInvariant();

                if (!cacDinhDangChoPhep.Contains(duoiFile))
                {
                    TempData["MessageError_KhachHang"] = "Chỉ chấp nhận định dạng .JPEG hoặc .PNG.";
                    return RedirectToAction("formCapNhatTK", new { id = nd.MaNd });
                }

                string thuMucAnh = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "avatar");
                if (!Directory.Exists(thuMucAnh))
                {
                    Directory.CreateDirectory(thuMucAnh);
                }
                string chuoiRandom = Guid.NewGuid().ToString();
                string tenfile = tk.MaNd + "_" + chuoiRandom + Path.GetExtension(hinh.FileName);
                string duongdan = Path.Combine(thuMucAnh, tenfile);
                using (FileStream f = new FileStream(duongdan, FileMode.Create))
                {
                    hinh.CopyTo(f);
                }
                tk.Hinh = tenfile;
            }

            db.Update(tk);
            db.SaveChanges();
            return RedirectToAction("XemTaiKhoan", new { email = nd.Email });
        }

        public IActionResult xemSanPham(int id)
        {
            List<DonDatHang> dsddh = new List<DonDatHang>();
            foreach (DonDatHang ddh in db.DonDatHangs.ToList())
            {
                if (ddh.MaNd == id)
                    dsddh.Add(ddh);
            }
            List<SanPham> sp = new List<SanPham>();
            foreach (ChiTietDonDatHang x in db.ChiTietDonDatHangs.ToList())
            {
                foreach (DonDatHang ddh in dsddh)
                {
                    if (x.MaDdh == ddh.MaDdh)
                    {
                        var sanpham = db.SanPhams.Find(x.MaSp);
                        if (sanpham != null && !sp.Contains(sanpham))
                        {
                            sp.Add(sanpham);
                        }
                    }
                }
            }
            ViewBag.DsHinh = db.Hinhs.ToList();
            ViewBag.DsNSX = db.NhaSanXuats.ToList();
            ViewBag.DsLoai = db.LoaiSanPhams.ToList();
            return View(sp);
        }

        public IActionResult formDanhGia(string id)
        {
            ViewBag.MaSp = id;
            return View();
        }

        [HttpPost]
        public IActionResult danhGia(string maSP, string noidung, int sosao)
        {
            var maNd = HttpContext.Session.GetInt32("UserId");

            var kt = db.DanhGia.FirstOrDefault(d => d.MaSp == maSP && d.MaNd == maNd);
            if (kt != null)
            {
                TempData["ErrorDanhGia"] = "Bạn đã đánh giá sản phẩm này rồi!";
                return RedirectToAction("xemSanPham", new { id = maNd });
            }
            var dg = new DanhGia
            {
                MaSp = maSP,
                MaNd = maNd != null ? maNd.Value : -1,
                Noidung = noidung,
                Sosao = sosao
            };

            try
            {
                db.DanhGia.Add(dg);
                db.SaveChanges();
                TempData["SuccessDanhGia"] = "Đánh giá thành công!";
            }
            catch (Exception ex)
            {
                return Content("Lỗi khi lưu: " + ex.InnerException?.Message ?? ex.Message);
            }

            return RedirectToAction("xemSanPham", new { id = maNd });
        }

        public IActionResult CapNhatHoSo_Form(string id)
        {
            NhanVien? nhanVien = db.NhanViens.Find(id);
            if (nhanVien == null)
            {
                string email = HttpContext.Session.GetString("UserEmail") ?? "";
                nhanVien = db.NhanViens.FirstOrDefault(nv => nv.Email == email);
                if (nhanVien == null)
                    return RedirectToAction("DangNhap");
            }
            return View(nhanVien);
        }

        public IActionResult CapNhatHoSo(NhanVien nv, IFormFile hinh)
        {
            var tk = db.NhanViens.Find(nv.MaNv);
            if (tk == null) return NotFound();

            tk.MaNv = nv.MaNv;
            tk.Matkhau = nv.Matkhau;
            tk.MaVt = nv.MaVt;
            tk.Sdt = nv.Sdt;
            tk.Cccd = nv.Cccd;
            tk.Diachi = nv.Diachi;
            tk.Email = nv.Email;
            tk.Tennv = nv.Tennv;
            tk.Phai = nv.Phai;
            tk.Trangthai = nv.Trangthai;

            if (hinh != null)
            {
                if (hinh.Length > 1024 * 1024)
                {
                    TempData["MessageError_NhanVien"] = "Dung lượng file không được vượt quá 1MB.";
                    return RedirectToAction("CapNhatHoSo_Form", new { id = nv.MaNv });
                }

                var cacDinhDangChoPhep = new[] { ".jpg", ".jpeg", ".png" };
                var duoiFile = Path.GetExtension(hinh.FileName).ToLowerInvariant();

                if (!cacDinhDangChoPhep.Contains(duoiFile))
                {
                    TempData["MessageError_NhanVien"] = "Chỉ chấp nhận định dạng .JPEG hoặc .PNG.";
                    return RedirectToAction("CapNhatHoSo_Form", new { id = nv.MaNv });
                }

                string thuMucAnh = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "avatar");
                if (!Directory.Exists(thuMucAnh))
                {
                    Directory.CreateDirectory(thuMucAnh);
                }
                string chuoiRandom = Guid.NewGuid().ToString();
                string tenfile = nv.MaNv.Trim() + "_" + chuoiRandom + Path.GetExtension(hinh.FileName);
                string duongdan = Path.Combine(thuMucAnh, tenfile);
                using (FileStream f = new FileStream(duongdan, FileMode.Create))
                {
                    hinh.CopyTo(f);
                }
                tk.Hinh = tenfile;
            }
            db.Update(tk);
            db.SaveChanges();
            return RedirectToAction("XemTaiKhoan", new { email = nv.Email });
        }
    }
}
