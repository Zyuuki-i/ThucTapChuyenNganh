using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApp_BanNhacCu.Models;

namespace WebApp_BanNhacCu.Models
{
    public partial class GiaoHang
    {
        [Display(Name = "Mã giao hàng")]
        public int MaGh { get; set; }
        [Display(Name = "Mã đơn đặt hàng")]
        public int MaDdh { get; set; }
        [Display(Name = "Mã nhân viên")]
        public string MaNv { get; set; } = null!;
        [Display(Name = "Ngày bắt đầu")]
        public DateTime? Ngaybd { get; set; }
        [Display(Name = "Ngày kết thúc")]
        public DateTime? Ngaykt { get; set; }
        [Display(Name = "Tổng thu")]
        public decimal? Tongthu { get; set; }
        [Display(Name = "Trạng thái")]
        public string? Trangthai { get; set; }

        public virtual DonDatHang MaDdhNavigation { get; set; } = null!;
        public virtual NhanVien MaNvNavigation { get; set; } = null!;
    }
}
