using System.ComponentModel.DataAnnotations;
using WebApp_BanNhacCu.Models;

namespace WebApp_BanNhacCu.Areas.Admin.MyModels
{
    public class CDonDatHang
    {
        [Display(Name = "Mã Đơn")]
        public int MaDdh { get; set; }
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
                Ngayxuat = ddh.Ngayxuat,
                Tongtien = ddh.Tongtien,
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
                Ngayxuat = ddh.Ngayxuat,
                Tongtien = ddh.Tongtien,
                TtThanhtoan = ddh.TtThanhtoan,
                MaNdNavigation = ddh.MaKhNavigation
            };
        }
    }
}
