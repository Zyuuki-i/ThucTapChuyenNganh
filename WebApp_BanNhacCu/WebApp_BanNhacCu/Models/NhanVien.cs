using System;
using System.Collections.Generic;

namespace WebApp_BanNhacCu.Models
{
    public partial class NhanVien
    {
        public int MaNv { get; set; }
        public bool? Phai { get; set; }
        public string Cccd { get; set; } = null!;

        public virtual NguoiDung MaNvNavigation { get; set; } = null!;
    }
}
