using System;
using System.Collections.Generic;

namespace WebApp_BanNhacCu.Models
{
    public partial class VaiTro
    {
        public VaiTro()
        {
            NguoiDungs = new HashSet<NguoiDung>();
        }

        public string MaVt { get; set; } = null!;
        public string Tenvt { get; set; } = null!;
        public string? Mota { get; set; }

        public virtual ICollection<NguoiDung> NguoiDungs { get; set; }
    }
}
