using System.ComponentModel.DataAnnotations;
using WebApp_BanNhacCu.Models;

namespace WebApp_BanNhacCu.Areas.Admin.MyModels
{
    public class CNguoiDung
    {
        [Display(Name = "Mã người dùng")]
        [Required(ErrorMessage = "Vui lòng nhập mã tài khoản")]
        public int MaNd { get; set; }
        [Display(Name = "Tên người dùng")]
        [Required(ErrorMessage = "Vui lòng nhập tên tài khoản")]
        public string Tennd { get; set; } = null!;
        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        public string Matkhau { get; set; } = null!;
        [Display(Name = "Số điện thoại")]
        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        public string? Sdt { get; set; }
        [Display(Name = "Địa chỉ")]
        [Required(ErrorMessage = "Vui lòng nhập địa chỉ")]
        public string? Phuongxa { get; set; }
        public string? Tinhthanh { get; set; }
        public string? Diachi { get; set; }
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Vui lòng nhập email")]
        public string Email { get; set; } = null!;
        [Display(Name = "Avatar")]
        public string? Hinh { get; set; }
        [Display(Name = "Trạng thái")]
        public bool? Trangthai { get; set; }


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
                Phuongxa = nd.Phuongxa,
                Tinhthanh = nd.Tinhthanh,
                Email = nd.Email,
                Hinh = nd.Hinh,
                Trangthai = nd.Trangthai,
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
                Phuongxa = nd.Phuongxa,
                Tinhthanh = nd.Tinhthanh,
                Email = nd.Email,
                Hinh = nd.Hinh,
                Trangthai = nd.Trangthai,
            };
        }
    }
}
