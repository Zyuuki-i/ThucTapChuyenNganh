using System;
using System.Collections.Generic;

namespace WebApp_BanNhacCu.Models
{
    public partial class MaGiamGium
    {
        public MaGiamGium()
        {
            HoaDons = new HashSet<HoaDon>();
        }

        public string MaGiamgia { get; set; } = null!;
        public decimal Giatri { get; set; }
        public DateTime? Ngaybatdau { get; set; }
        public DateTime? Ngayketthuc { get; set; }
        public int Trangthai { get; set; }

        public virtual ICollection<HoaDon> HoaDons { get; set; }
    }
}
