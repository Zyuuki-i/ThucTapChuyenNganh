using System;
using System.Collections.Generic;

namespace WebApp_BanNhacCu.Models
{
    public partial class VaiTro
    {
        public VaiTro()
        {
            NhanViens = new HashSet<NhanVien>();
        }

        public string MaVt { get; set; } = null!;
        public string Tenvt { get; set; } = null!;
        public string? Mota { get; set; }

        public virtual ICollection<NhanVien> NhanViens { get; set; }
    }
}
