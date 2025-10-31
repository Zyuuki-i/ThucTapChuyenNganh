using System;
using System.Collections.Generic;

namespace WebApp_BanNhacCu.Models
{
    public partial class HoaDon
    {
        public HoaDon()
        {
            ChiTietHoaDons = new HashSet<ChiTietHoaDon>();
        }

        public int MaHd { get; set; }
        public string? MaGiamgia { get; set; }
        public DateTime? Ngayxuat { get; set; }
        public decimal? Tongtien { get; set; }
        public string? TtThanhtoan { get; set; }
        public DateTime? HanThanhtoan { get; set; }

        public virtual MaGiamGium? MaGiamgiaNavigation { get; set; }
        public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }
    }
}
