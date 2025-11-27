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
        [Display(Name = "Mã nhân viên")]
        [Required(ErrorMessage = "Mã nhân viên không được để trống!")]
        public string? MaNv { get; set; }
        [Display(Name = "Người nhận")]
        [Required(ErrorMessage = "Người nhận không được để trống!")]
        public string? Nguoinhan { get; set; }
        [Display(Name = "Số điện thoại")]
        [Required(ErrorMessage = "Số điện thoại không được để trống!")]
        public string? Sdt { get; set; }
        [Display(Name="Địa chỉ nhận")]
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
                MaNv = ddh.MaNv,
                Nguoinhan = ddh.Nguoinhan,
                Sdt = ddh.Sdt,
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
                MaNv = ddh.MaNv,
                Nguoinhan = ddh.Nguoinhan,
                Sdt = ddh.Sdt,
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
