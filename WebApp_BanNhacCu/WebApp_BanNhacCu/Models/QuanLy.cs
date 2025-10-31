using System;
using System.Collections.Generic;

namespace WebApp_BanNhacCu.Models
{
    public partial class QuanLy
    {
        public int MaQl { get; set; }
        public string Tenql { get; set; } = null!;

        public virtual TaiKhoan MaQlNavigation { get; set; } = null!;
    }
}
