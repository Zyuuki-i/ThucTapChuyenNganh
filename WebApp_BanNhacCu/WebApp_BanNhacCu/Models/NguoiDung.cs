using System;
using System.Collections.Generic;

namespace WebApp_BanNhacCu.Models
{
    public partial class NguoiDung
    {
        public NguoiDung()
        {
            DanhGia = new HashSet<DanhGia>();
            DonDatHangs = new HashSet<DonDatHang>();
        }

        public int MaNd { get; set; }
        public string Tennd { get; set; } = null!;
        public string Matkhau { get; set; } = null!;
        public string? Sdt { get; set; }
        public string? Diachi { get; set; }
        public string Email { get; set; } = null!;
        public string? Hinhanh { get; set; }
        public string MaVt { get; set; } = null!;

        public virtual VaiTro MaVtNavigation { get; set; } = null!;
        public virtual NhanVien? NhanVien { get; set; }
        public virtual ICollection<DanhGia> DanhGia { get; set; }
        public virtual ICollection<DonDatHang> DonDatHangs { get; set; }
    }
}
