using System.ComponentModel.DataAnnotations;
using WebApp_BanNhacCu.Models;

namespace WebApp_BanNhacCu.Areas.Admin.MyModels
{
    public class CDonDatHang
    {
        [Display(Name = "Mã Đơn hàng")]
        public int MaDdh { get; set; }
        [Display(Name = "Mã khách hàng")]
        [Required(ErrorMessage = "Mã khách hàng không được để trống!")]
        public int MaKh { get; set; }
        [Display(Name="Địa chỉ")]
        [Required(ErrorMessage = "Địa chỉ nhận không được để trống!")]
        public string Diachi { get; set; } = null!;
        [Display(Name = "Ngày xuất")]
        [Required(ErrorMessage = "Ngày xuất không được để trống!")]
        public DateTime? Ngaydat { get; set; }
        [Display(Name = "Tổng tiền")]
        public decimal? Tongtien { get; set; }
        [Display(Name = "Trạng thái")]
        public string Trangthai { get; set; } = null!;
        [Display(Name = "Thanh toán")]
        public string? TtThanhtoan { get; set; }

        public virtual NguoiDung MaKhNavigation { get; set; } = null!;

        public static CDonDatHang chuyenDoi(DonDatHang ddh)
        {
            if(ddh == null)
            {
                return null;
            }
            return new CDonDatHang
            {
                MaDdh = ddh.MaDdh,
                MaKh = ddh.MaNd,
                Diachi = ddh.Diachi,
                Ngaydat = ddh.Ngaydat,
                Tongtien = ddh.Tongtien,
                Trangthai = ddh.Trangthai,
                TtThanhtoan = ddh.TtThanhtoan,
                MaKhNavigation = ddh.MaNdNavigation
            };
        }

        public static DonDatHang chuyenDoi(CDonDatHang ddh)
        {
            if (ddh == null)
            {
                return null;
            }
            return new DonDatHang
            {
                MaDdh = ddh.MaDdh,
                MaNd = ddh.MaKh,
                Diachi = ddh.Diachi,
                Ngaydat = ddh.Ngaydat,
                Tongtien = ddh.Tongtien,
                Trangthai = ddh.Trangthai,
                TtThanhtoan = ddh.TtThanhtoan,
                MaNdNavigation = ddh.MaKhNavigation
            };
        }
    }
}
