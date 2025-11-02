using System;
using System.Collections.Generic;

namespace WebApp_BanNhacCu.Models
{
    public partial class DanhGia
    {
        public int MaKh { get; set; }
        public string MaSp { get; set; } = null!;
        public string? Noidung { get; set; }
        public int? Sosao { get; set; }

        public virtual KhachHang MaKhNavigation { get; set; } = null!;
        public virtual SanPham MaSpNavigation { get; set; } = null!;
    }
}
