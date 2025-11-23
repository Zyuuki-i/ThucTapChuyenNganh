using System;
using System.Collections.Generic;

namespace WebApp_BanNhacCu.Models
{
    public partial class NhanVien
    {
        public NhanVien()
        {
            CapNhats = new HashSet<CapNhat>();
            DonDatHangs = new HashSet<DonDatHang>();
        }

        public string MaNv { get; set; } = null!;
        public string Tennv { get; set; } = null!;
        public string Matkhau { get; set; } = null!;
        public string? Sdt { get; set; }
        public string Email { get; set; } = null!;
        public string Cccd { get; set; } = null!;
        public string? Diachi { get; set; }
        public string? Hinh { get; set; }
        public string MaVt { get; set; } = null!;

        public virtual VaiTro MaVtNavigation { get; set; } = null!;
        public virtual ICollection<CapNhat> CapNhats { get; set; }
        public virtual ICollection<DonDatHang> DonDatHangs { get; set; }
    }
}
