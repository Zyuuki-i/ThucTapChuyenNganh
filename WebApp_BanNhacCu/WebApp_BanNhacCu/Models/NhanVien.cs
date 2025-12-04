using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApp_BanNhacCu.Models
{
    public partial class NhanVien
    {
        public NhanVien()
        {
            CapNhats = new HashSet<CapNhat>();
            DonDatHangs = new HashSet<DonDatHang>();
            GiaoHangs = new HashSet<GiaoHang>();
        }
        [Display(Name = "Mã nhân viên")]
        public string MaNv { get; set; } = null!;
        [Display(Name = "Tên nhân viên")]
        public string Tennv { get; set; } = null!;
        [Display(Name = "Mật khẩu")]
        public string Matkhau { get; set; } = null!;
        [Display(Name = "Giới tính")]
        public bool Phai { get; set; }
        [Display(Name = "Số điện thoại")]
        public string? Sdt { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; } = null!;
        [Display(Name = "CCCD")]
        public string Cccd { get; set; } = null!;
        [Display(Name = "Địa chỉ")]
        public string? Diachi { get; set; }
        [Display(Name = "Hình")]
        public string? Hinh { get; set; }
        [Display(Name = "Mã vai trò")]
        public string MaVt { get; set; } = null!;
        [Display(Name = "Trạng thái")]
        public bool? Trangthai { get; set; }

        public virtual VaiTro MaVtNavigation { get; set; } = null!;
        public virtual ICollection<CapNhat> CapNhats { get; set; }
        public virtual ICollection<DonDatHang> DonDatHangs { get; set; }
        public virtual ICollection<GiaoHang> GiaoHangs { get; set; }
    }
}
