using System;
using System.Collections.Generic;

namespace WebApp_BanNhacCu.Models
{
    public partial class KhachHang
    {
        public KhachHang()
        {
            DanhGia = new HashSet<DanhGium>();
            GioHangs = new HashSet<GioHang>();
        }

        public int MaKh { get; set; }
        public string Tenkh { get; set; } = null!;
        public string? Diachi { get; set; }

        public virtual TaiKhoan MaKhNavigation { get; set; } = null!;
        public virtual ICollection<DanhGium> DanhGia { get; set; }
        public virtual ICollection<GioHang> GioHangs { get; set; }
    }
}
