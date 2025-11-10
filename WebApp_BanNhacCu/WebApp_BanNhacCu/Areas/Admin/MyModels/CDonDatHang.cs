using System.ComponentModel.DataAnnotations;
using WebApp_BanNhacCu.Models;

namespace WebApp_BanNhacCu.Areas.Admin.MyModels
{
    public class CDonDatHang
    {
        [Display(Name = "Mã Đơn")]
        public int MaDdh { get; set; }
        [Display(Name = "Mã giảm giá")]
        public string? MaGiamgia { get; set; }
        [Display(Name = "Mã khách hàng")]
        [Required(ErrorMessage = "Mã khách hàng không được để trống!")]
        public int MaKh { get; set; }
        [Display(Name = "Ngày xuất")]
        [Required(ErrorMessage = "Ngày xuất không được để trống!")]
        public DateTime? Ngayxuat { get; set; }
        [Display(Name = "Tổng tiền")]
        public decimal? Tongtien { get; set; }
        [Display(Name = "Trạng thái")]
        public string? TtThanhtoan { get; set; }

        public virtual MaGiamGia? MaGiamgiaNavigation { get; set; }
        public virtual KhachHang MaKhNavigation { get; set; } = null!;

        public static CDonDatHang chuyendoi(DonDatHang ddh)
        {
            if (ddh == null) return null;
            return new CDonDatHang
            {
                MaDdh = ddh.MaDdh,
                MaGiamgia = ddh.MaGiamgia,
                MaKh = ddh.MaKh,
                Ngayxuat = ddh.Ngayxuat,
                Tongtien = ddh.Tongtien,
                TtThanhtoan = ddh.TtThanhtoan,
                MaGiamgiaNavigation = ddh.MaGiamgiaNavigation,
                MaKhNavigation = ddh.MaKhNavigation
            };
        }

        public static DonDatHang chuyendoi(CDonDatHang ddh)
        {
            if (ddh == null) return null;
            return new DonDatHang
            {
                MaDdh = ddh.MaDdh,
                MaGiamgia = ddh.MaGiamgia,
                MaKh = ddh.MaKh,
                Ngayxuat = ddh.Ngayxuat,
                Tongtien = ddh.Tongtien,
                TtThanhtoan = ddh.TtThanhtoan,
                MaGiamgiaNavigation = ddh.MaGiamgiaNavigation,
                MaKhNavigation = ddh.MaKhNavigation
            };
        }

    }
}
