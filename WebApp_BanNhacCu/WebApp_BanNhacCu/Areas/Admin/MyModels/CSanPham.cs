using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using WebApp_BanNhacCu.Models;

namespace WebApp_BanNhacCu.Areas.Admin.MyModels
{
    public class CSanPham
    {
        [Display(Name = "Mã sản phẩm")]
        [Required(ErrorMessage = "Mã sản phẩm không được để trống!")]
        public string MaSp { get; set; } = null!;
        [Display(Name = "Tên sản phẩm")]
        [Required(ErrorMessage = "Tên sản phẩm không được để trống!")]
        public string Tensp { get; set; } = null!;
        [Display(Name = "Mã nhà sản xuất")]
        [Required(ErrorMessage = "Chọn nhà sản xuất phù hợp!")]
        public string? MaNsx { get; set; }
        [Display(Name = "Mã loại")]
        [Required(ErrorMessage = "Chọn loại cho sản phẩm!")]
        public string? MaLoai { get; set; }
        [Display(Name = "Giá")]
        [Range(0, double.MaxValue, ErrorMessage = "Giá sản phẩm không hợp lệ!")]
        public decimal Giasp { get; set; }
        [Display(Name = "Hình ảnh")]
        public string? Anhsp { get; set; }
        [Display(Name = "Mô tả")]
        public string? Mota { get; set; }

        public virtual LoaiSanPham? MaLoaiNavigation { get; set; }
        public virtual NhaSanXuat? MaNsxNavigation { get; set; }
        public virtual KhoHang? KhoHang { get; set; }

        public static CSanPham chuyenDoi(SanPham sp)
        {
            return new CSanPham
            {
                MaSp = sp.MaSp,
                Tensp = sp.Tensp,
                MaNsx = sp.MaNsx,
                MaLoai = sp.MaLoai,
                Giasp = sp.Giasp,
                Anhsp = sp.Anhsp,
                Mota = sp.Mota,
                MaLoaiNavigation = sp.MaLoaiNavigation,
                MaNsxNavigation = sp.MaNsxNavigation,
                KhoHang = sp.KhoHang
            };
        }

        public static SanPham chuyenDoi(CSanPham sp)
        {
            return new SanPham
            {
                MaSp = sp.MaSp,
                Tensp = sp.Tensp,
                MaNsx = sp.MaNsx,
                MaLoai = sp.MaLoai,
                Giasp = sp.Giasp,
                Anhsp = sp.Anhsp,
                Mota = sp.Mota,
                MaLoaiNavigation = sp.MaLoaiNavigation,
                MaNsxNavigation = sp.MaNsxNavigation,
                KhoHang = sp.KhoHang
            };
        }
    }
}
