using System;
using System.Collections.Generic;

namespace WebApp_BanNhacCu.Models
{
    public partial class ChiTietHoaDon
    {
        public int MaHd { get; set; }
        public string MaSp { get; set; } = null!;
        public int Soluong { get; set; }
        public decimal Gia { get; set; }
        public decimal? Chietkhau { get; set; }
        public decimal? Thanhtien { get; set; }

        public virtual HoaDon MaHdNavigation { get; set; } = null!;
        public virtual SanPham MaSpNavigation { get; set; } = null!;
    }
}
