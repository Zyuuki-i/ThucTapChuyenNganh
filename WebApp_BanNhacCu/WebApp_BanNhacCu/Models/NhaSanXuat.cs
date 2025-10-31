using System;
using System.Collections.Generic;

namespace WebApp_BanNhacCu.Models
{
    public partial class NhaSanXuat
    {
        public NhaSanXuat()
        {
            SanPhams = new HashSet<SanPham>();
        }

        public string MaNsx { get; set; } = null!;
        public string Tennsx { get; set; } = null!;
        public string? Diachi { get; set; }
        public string? Sdt { get; set; }
        public string? Email { get; set; }

        public virtual ICollection<SanPham> SanPhams { get; set; }
    }
}
