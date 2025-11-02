using System;
using System.Collections.Generic;

namespace WebApp_BanNhacCu.Models
{
    public partial class KhachHang
    {
        public KhachHang()
        {
            DanhGia = new HashSet<DanhGia>();
            DonDatHangs = new HashSet<DonDatHang>();
        }

        public int MaKh { get; set; }
        public string Tenkh { get; set; } = null!;
        public string? Diachi { get; set; }

        public virtual TaiKhoan MaKhNavigation { get; set; } = null!;
        public virtual ICollection<DanhGia> DanhGia { get; set; }
        public virtual ICollection<DonDatHang> DonDatHangs { get; set; }
    }
}
