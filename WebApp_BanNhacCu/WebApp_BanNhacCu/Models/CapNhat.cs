using System;
using System.Collections.Generic;

namespace WebApp_BanNhacCu.Models
{
    public partial class CapNhat
    {
        public int MaCn { get; set; }
        public string MaNv { get; set; } = null!;
        public string MaSp { get; set; } = null!;
        public DateTime? Ngaycapnhat { get; set; }

        public virtual NhanVien MaNvNavigation { get; set; } = null!;
        public virtual SanPham MaSpNavigation { get; set; } = null!;
    }
}
