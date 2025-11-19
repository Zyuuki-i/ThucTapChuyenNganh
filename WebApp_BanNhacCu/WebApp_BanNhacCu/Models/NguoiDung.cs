using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApp_BanNhacCu.Models
{
    public partial class NguoiDung
    {
        public NguoiDung()
        {
            DanhGia = new HashSet<DanhGia>();
            DonDatHangs = new HashSet<DonDatHang>();
        }
        [Display(Name = "Mã người dùng")]
        public int MaNd { get; set; }
        [Display(Name = "Tên người dùng")]
        public string Tennd { get; set; } = null!;
        [Display(Name = "Mật khẩu")]
        public string Matkhau { get; set; } = null!;
        [Display(Name = "Số điện thoại")]
        public string? Sdt { get; set; }
        [Display(Name = "Địa chỉ")]
        public string? Diachi { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; } = null!;
        public string? Hinhanh { get; set; }
        public string MaVt { get; set; } = null!;

        public virtual VaiTro MaVtNavigation { get; set; } = null!;
        public virtual NhanVien? NhanVien { get; set; }
        public virtual ICollection<DanhGia> DanhGia { get; set; }
        public virtual ICollection<DonDatHang> DonDatHangs { get; set; }
    }
}
