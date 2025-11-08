using System.ComponentModel.DataAnnotations;
using WebApp_BanNhacCu.Models;

namespace WebApp_BanNhacCu.Areas.Admin.MyModels
{
    public class CTaiKhoan
    {
        [Display(Name = "Mã tài khoản")]
        public int MaTk { get; set; }

        [Display(Name = "Số điện thoại")]
        [Required(ErrorMessage = "Bạn chưa nhập sdt")]
        public string? Sdt { get; set; }

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Bạn chưa nhập mật khẩu")]
        public string Matkhau { get; set; } = null!;

        [Display(Name = "Mã vai trò")]
        [Required(ErrorMessage = "Bạn chưa nhập vai trò")]
        public string MaVt { get; set; } = null!;

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Bạn chưa nhập email")]
        public string Email { get; set; } = null!;

        [Display(Name = "Hình")]
        public string? Hinhanh { get; set; }

        public virtual VaiTro MaVtNavigation { get; set; } = null!;

        public static CTaiKhoan chuyendoi (TaiKhoan tk)
        {
            CTaiKhoan ctk = new CTaiKhoan();
            ctk.MaTk = tk.MaTk;
            ctk.Sdt = tk.Sdt;
            ctk.Matkhau = tk.Matkhau;
            ctk.MaVt = tk.MaVt;
            ctk.Email = tk.Email;
            ctk.Hinhanh = tk.Hinhanh;
            ctk.MaVtNavigation = tk.MaVtNavigation;
            return ctk;
        }

        public static TaiKhoan chuyendoi(CTaiKhoan ctk)
        {
            TaiKhoan tk = new TaiKhoan();
            tk.MaTk = ctk.MaTk;
            tk.Sdt = ctk.Sdt;
            tk.Matkhau = ctk.Matkhau;
            tk.MaVt = ctk.MaVt;
            tk.Email = ctk.Email;
            tk.Hinhanh = ctk.Hinhanh;
            return tk;
        }
    }
}
