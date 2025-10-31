using System;
using System.Collections.Generic;

namespace WebApp_BanNhacCu.Models
{
    public partial class GioHang
    {
        public GioHang()
        {
            ChiTietGioHangs = new HashSet<ChiTietGioHang>();
        }

        public int MaGh { get; set; }
        public int MaKh { get; set; }
        public DateTime? Ngaytao { get; set; }
        public decimal? Tongtien { get; set; }
        public string? Trangthai { get; set; }

        public virtual KhachHang MaKhNavigation { get; set; } = null!;
        public virtual ICollection<ChiTietGioHang> ChiTietGioHangs { get; set; }
    }
}
