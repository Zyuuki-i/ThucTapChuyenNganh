using System;
using System.Collections.Generic;

namespace WebApp_BanNhacCu.Models
{
    public partial class DonDatHang
    {
        public DonDatHang()
        {
            ChiTietDonDatHangs = new HashSet<ChiTietDonDatHang>();
            GiaoHangs = new HashSet<GiaoHang>();
        }

        public int MaDdh { get; set; }
        public int MaNd { get; set; }
        public string? MaNv { get; set; }
        public string? Nguoinhan { get; set; }
        public string? Sdt { get; set; }
        public string Diachi { get; set; } = null!;
        public DateTime? Ngaydat { get; set; }
        public decimal? Tongtien { get; set; }
        public string Trangthai { get; set; } = null!;
        public string? TtThanhtoan { get; set; }
        public string? Phuongthuc { get; set; }

        public virtual NguoiDung MaNdNavigation { get; set; } = null!;
        public virtual NhanVien? MaNvNavigation { get; set; }
        public virtual ICollection<ChiTietDonDatHang> ChiTietDonDatHangs { get; set; }
        public virtual ICollection<GiaoHang> GiaoHangs { get; set; }
    }
}
