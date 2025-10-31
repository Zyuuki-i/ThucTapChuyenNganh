using System;
using System.Collections.Generic;

namespace WebApp_BanNhacCu.Models
{
    public partial class KhoHang
    {
        public string MaSp { get; set; } = null!;
        public int Soluongton { get; set; }
        public DateTime? Ngaycapnhat { get; set; }

        public virtual SanPham MaSpNavigation { get; set; } = null!;
    }
}
