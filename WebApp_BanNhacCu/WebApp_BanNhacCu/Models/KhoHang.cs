using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApp_BanNhacCu.Models
{
    public partial class KhoHang
    {
        [Display(Name = "Mã sản phẩm")]
        public string MaSp { get; set; } = null!;
        [Display(Name = "Số lượng tồn")]
        public int Soluongton { get; set; }
        [Display(Name = "Ngày cập nhật")]
        public DateTime? Ngaycapnhat { get; set; }

        public virtual SanPham MaSpNavigation { get; set; } = null!;
    }
}
