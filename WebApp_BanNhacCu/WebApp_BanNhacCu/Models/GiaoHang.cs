using System;
using System.Collections.Generic;

namespace WebApp_BanNhacCu.Models
{
    public partial class GiaoHang
    {
        public int MaGh { get; set; }
        public int MaDdh { get; set; }
        public string MaNv { get; set; } = null!;
        public DateTime? Ngaybd { get; set; }
        public DateTime? Ngaykt { get; set; }
        public decimal? Tongthu { get; set; }

        public virtual DonDatHang MaDdhNavigation { get; set; } = null!;
        public virtual NhanVien MaNvNavigation { get; set; } = null!;
    }
}
