using System;
using System.Collections.Generic;

namespace WebApp_BanNhacCu.Models
{
    public partial class MaGiamGia
    {
        public MaGiamGia()
        {
            DonDatHangs = new HashSet<DonDatHang>();
        }

        public string MaGiamgia { get; set; } = null!;
        public decimal Giatri { get; set; }
        public DateTime? Ngaybatdau { get; set; }
        public DateTime? Ngayketthuc { get; set; }
        public int Trangthai { get; set; }

        public virtual ICollection<DonDatHang> DonDatHangs { get; set; }
    }
}
