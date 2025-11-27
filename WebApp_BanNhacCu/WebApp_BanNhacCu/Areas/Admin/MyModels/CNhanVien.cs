using System.ComponentModel.DataAnnotations;
using WebApp_BanNhacCu.Models;

namespace WebApp_BanNhacCu.Areas.Admin.MyModels
{
    public class CNhanVien
    {
        [Display(Name = "Mã nhân viên")]
        [Required(ErrorMessage = "Mã nhân viên không được để trống!")]
        public string MaNv { get; set; } = null!;
        [Display(Name = "Tên nhân viên")]
        [Required(ErrorMessage = "Tên nhân viên không được để trống!")]
        public string Tennv { get; set; } = null!;
        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Mật khẩu không được để trống!")]
        public string Matkhau { get; set; } = null!;
        [Display(Name = "Phái")]
        [Required(ErrorMessage = "Phái không được để trống!")]
        public bool Phai { get; set; }
        [Display(Name = "Số điện thoại")]
        [Required(ErrorMessage = "Số điện thoại không được để trống!")]
        public string? Sdt { get; set; }
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email không được để trống!")]
        public string Email { get; set; } = null!;
        [Display(Name = "CCCD")]
        [Required(ErrorMessage = "CCCD không được để trống!")]
        public string Cccd { get; set; } = null!;
        [Display(Name = "Địa chỉ")]
        [Required(ErrorMessage = "Địa chỉ không được để trống!")]
        public string? Diachi { get; set; }
        [Display(Name = "Hình ảnh")]
        public string? Hinh { get; set; }
        [Display(Name = "Mã vai trò")]
        [Required(ErrorMessage = "Mã vai trò không được để trống!")]
        public string MaVt { get; set; } = null!;
        [Display(Name = "Trạng thái")]
        public bool? Trangthai { get; set; }

        public static CNhanVien chuyendoi(NhanVien nv)
        {
            if (nv == null)
            {
                return null;
            }
            return new CNhanVien
            {
                MaNv = nv.MaNv,
                Tennv = nv.Tennv,
                Matkhau = nv.Matkhau,
                Phai = nv.Phai,
                Sdt = nv.Sdt,
                Email = nv.Email,
                Cccd = nv.Cccd,
                Diachi = nv.Diachi,
                Hinh = nv.Hinh,
                MaVt = nv.MaVt,
                Trangthai = nv.Trangthai,
            };
        }

        public static NhanVien chuyendoi(CNhanVien nv)
        {
            if (nv == null)
            {
                return null;
            }
            return new NhanVien
            {
                MaNv = nv.MaNv,
                Tennv = nv.Tennv,
                Matkhau = nv.Matkhau,
                Phai = nv.Phai,
                Sdt = nv.Sdt,
                Email = nv.Email,
                Cccd = nv.Cccd,
                Diachi = nv.Diachi,
                Hinh = nv.Hinh,
                MaVt = nv.MaVt,
                Trangthai = nv.Trangthai
            };
        }
    }
}
