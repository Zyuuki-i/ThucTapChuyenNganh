using System;
using System.Collections.Generic;

namespace WebApp_BanNhacCu.Models
{
    public partial class ChiTietGioHang
    {
        public int MaGh { get; set; }
        public string MaSp { get; set; } = null!;
        public int Soluong { get; set; }
        public decimal Gia { get; set; }
        public decimal? Thanhtien { get; set; }

        public virtual GioHang MaGhNavigation { get; set; } = null!;
        public virtual SanPham MaSpNavigation { get; set; } = null!;
    }
}
