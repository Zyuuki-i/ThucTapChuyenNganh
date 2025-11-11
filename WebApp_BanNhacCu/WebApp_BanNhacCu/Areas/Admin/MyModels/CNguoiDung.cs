using System.ComponentModel.DataAnnotations;
using WebApp_BanNhacCu.Models;

namespace WebApp_BanNhacCu.Areas.Admin.MyModels
{
    public class CNguoiDung
    {
        [Display(Name = "Mã tài khoản")]
        [Required(ErrorMessage = "Vui lòng nhập mã tài khoản")]
        public int MaNd { get; set; }
        [Display(Name = "Mã tài khoản")]
        [Required(ErrorMessage = "Vui lòng nhập mã tài khoản")]
        public string Tennd { get; set; } = null!;
        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        public string Matkhau { get; set; } = null!;
        [Display(Name = "Số điện thoại")]
        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        public string? Sdt { get; set; }
        [Display(Name = "Địa chỉ")]
        [Required(ErrorMessage = "Vui lòng nhập địa chỉ")]
        public string? Diachi { get; set; }
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Vui lòng nhập email")]
        public string Email { get; set; } = null!;
        [Display(Name = "Hình ảnh")]
        public string? Hinhanh { get; set; }
        [Display(Name = "Mã vai trò")]
        [Required(ErrorMessage = "Vui lòng nhập mã vai trò")]
        public string MaVt { get; set; } = null!;

        public virtual VaiTro MaVtNavigation { get; set; } = null!;
        public virtual NhanVien? NhanVien { get; set; }

        public static CNguoiDung chuyendoi(NguoiDung nd)
        {
            if (nd == null) return null;
            return new CNguoiDung
            {
                MaNd = nd.MaNd,
                Tennd = nd.Tennd,
                Matkhau = nd.Matkhau,
                Sdt = nd.Sdt,
                Diachi = nd.Diachi,
                Email = nd.Email,
                Hinhanh = nd.Hinhanh,
                MaVt = nd.MaVt,
                MaVtNavigation = nd.MaVtNavigation,
                NhanVien = nd.NhanVien
            };
        }

        public static NguoiDung chuyendoi(CNguoiDung nd)
        {
            if (nd == null) return null;
            return new NguoiDung
            {
                MaNd = nd.MaNd,
                Tennd = nd.Tennd,
                Matkhau = nd.Matkhau,
                Sdt = nd.Sdt,
                Diachi = nd.Diachi,
                Email = nd.Email,
                Hinhanh = nd.Hinhanh,
                MaVt = nd.MaVt,
                MaVtNavigation = nd.MaVtNavigation,
                NhanVien = nd.NhanVien
            };
        }
    }
}
