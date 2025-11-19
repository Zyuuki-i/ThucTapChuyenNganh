using System.ComponentModel.DataAnnotations;

namespace WebApp_BanNhacCu.Areas.Admin.MyModels
{
    public class CThongKecs
    {
        [Display(Name = "Tháng")]
        public int Thang { get; set; }
        [Display(Name = "Số lượng đơn")]
        public int SoLuongDon { get; set; }
        [Display(Name = "Tổng tiền")]
        public decimal TongTien { get; set; }
    }
}
