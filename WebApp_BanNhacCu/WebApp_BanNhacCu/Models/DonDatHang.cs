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
        public int MaNd { get; set; }
        public DateTime? Ngayxuat { get; set; }
        public decimal? Tongtien { get; set; }
        public string? TtThanhtoan { get; set; }

        public virtual NguoiDung MaNdNavigation { get; set; } = null!;
        public virtual ICollection<ChiTietDonDatHang> ChiTietDonDatHangs { get; set; }
    }
}
