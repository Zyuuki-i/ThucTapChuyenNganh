using System;
using System.Collections.Generic;

namespace WebApp_BanNhacCu.Models
{
    public partial class Hinh
    {
        public int MaHinh { get; set; }
        public string MaSp { get; set; } = null!;
        public string? Url { get; set; }

        public virtual SanPham MaSpNavigation { get; set; } = null!;
    }
}
