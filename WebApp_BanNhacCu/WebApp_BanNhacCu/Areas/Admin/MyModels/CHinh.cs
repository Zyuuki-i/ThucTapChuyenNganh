using System.ComponentModel.DataAnnotations;
using WebApp_BanNhacCu.Models;

namespace WebApp_BanNhacCu.Areas.Admin.MyModels
{
    public class CHinh
    {
        [Display(Name = "Mã hình")]
        public int MaHinh { get; set; }
        [Display(Name = "Mã sản phẩm")]
        [Required(ErrorMessage = "Chọn sản phẩm có hình này!")]
        public string MaSp { get; set; } = null!;
        [Display(Name = "Tên hình")]
        [Required(ErrorMessage = "Tên file hình là bắt buộc!")]
        public string? Tenhinh { get; set; }

        public virtual SanPham MaSpNavigation { get; set; } = null!;

        public static CHinh chuyenDoi(Hinh hinh)
        {
            if (hinh == null) return null;
            return new CHinh
            {
                MaHinh = hinh.MaHinh,
                Tenhinh = hinh.Tenhinh,
                MaSp = hinh.MaSp,
            };
        }

        public static Hinh chuyenDoi(CHinh hinh)
        {
            if (hinh == null) return null;
            return new Hinh
            {
                MaHinh = hinh.MaHinh,
                Tenhinh = hinh.Tenhinh,
                MaSp = hinh.MaSp,
            };
        }
    }
}
