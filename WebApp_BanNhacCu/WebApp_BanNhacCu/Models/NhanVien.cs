using System;
using System.Collections.Generic;

namespace WebApp_BanNhacCu.Models
{
    public partial class NhanVien
    {
        public int MaNv { get; set; }
        public string Tennv { get; set; } = null!;
        public bool? Phai { get; set; }
        public string Cccd { get; set; } = null!;
        public DateTime Ngaycap { get; set; }
        public string Noicap { get; set; } = null!;

        public virtual TaiKhoan MaNvNavigation { get; set; } = null!;
    }
}
