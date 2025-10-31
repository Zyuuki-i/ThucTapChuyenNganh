using System;
using System.Collections.Generic;

namespace WebApp_BanNhacCu.Models
{
    public partial class TaiKhoan
    {
        public int MaTk { get; set; }
        public string? Sdt { get; set; }
        public string Matkhau { get; set; } = null!;
        public string MaVt { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Hinhanh { get; set; }

        public virtual VaiTro MaVtNavigation { get; set; } = null!;
        public virtual KhachHang? KhachHang { get; set; }
        public virtual NhanVien? NhanVien { get; set; }
        public virtual QuanLy? QuanLy { get; set; }
    }
}
