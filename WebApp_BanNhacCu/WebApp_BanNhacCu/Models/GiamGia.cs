using System;
using System.Collections.Generic;

namespace WebApp_BanNhacCu.Models
{
    public partial class GiamGia
    {
        public GiamGia()
        {
            ChiTietGiamGia = new HashSet<ChiTietGiamGia>();
        }

        public string MaGg { get; set; } = null!;
        public string Tenma { get; set; } = null!;
        public string? Loaima { get; set; }
        public decimal? Dieukien { get; set; }
        public int? Phantramgiam { get; set; }
        public DateTime? Ngaybd { get; set; }
        public DateTime? Ngaykt { get; set; }

        public virtual ICollection<ChiTietGiamGia> ChiTietGiamGia { get; set; }
    }
}
