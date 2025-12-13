using System;
using System.Collections.Generic;

namespace WebApp_BanNhacCu.Models
{
    public partial class DanhGia
    {
        public int MaNd { get; set; }
        public string MaSp { get; set; } = null!;
        public string? Noidung { get; set; }
        public int? Sosao { get; set; }

        public virtual NguoiDung MaNdNavigation { get; set; } = null!;
        public virtual SanPham MaSpNavigation { get; set; } = null!;
    }
}
