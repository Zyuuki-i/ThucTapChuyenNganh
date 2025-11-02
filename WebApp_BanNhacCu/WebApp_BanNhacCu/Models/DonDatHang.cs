using System;
using System.Collections.Generic;

namespace WebApp_BanNhacCu.Models
{
    public partial class DonDatHang
    {
        public DonDatHang()
        {
            ChiTietDonDatHangs = new HashSet<ChiTietDonDatHang>();
        }

        public int MaDdh { get; set; }
        public string? MaGiamgia { get; set; }
        public int MaKh { get; set; }
        public DateTime? Ngayxuat { get; set; }
        public decimal? Tongtien { get; set; }
        public string? TtThanhtoan { get; set; }

        public virtual MaGiamGia? MaGiamgiaNavigation { get; set; }
        public virtual KhachHang MaKhNavigation { get; set; } = null!;
        public virtual ICollection<ChiTietDonDatHang> ChiTietDonDatHangs { get; set; }
    }
}
