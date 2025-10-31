using System;
using System.Collections.Generic;

namespace WebApp_BanNhacCu.Models
{
    public partial class LoaiSanPham
    {
        public LoaiSanPham()
        {
            SanPhams = new HashSet<SanPham>();
        }

        public string MaLoai { get; set; } = null!;
        public string Tenloai { get; set; } = null!;
        public string? Mota { get; set; }

        public virtual ICollection<SanPham> SanPhams { get; set; }
    }
}
