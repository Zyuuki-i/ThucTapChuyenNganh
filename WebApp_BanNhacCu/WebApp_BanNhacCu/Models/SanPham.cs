using System;
using System.Collections.Generic;

namespace WebApp_BanNhacCu.Models
{
    public partial class SanPham
    {
        public SanPham()
        {
            ChiTietGioHangs = new HashSet<ChiTietGioHang>();
            ChiTietHoaDons = new HashSet<ChiTietHoaDon>();
            DanhGia = new HashSet<DanhGium>();
        }

        public string MaSp { get; set; } = null!;
        public string Tensp { get; set; } = null!;
        public string? MaNsx { get; set; }
        public string? MaLoai { get; set; }
        public decimal Giasp { get; set; }
        public string? Anhsp { get; set; }
        public string? Mota { get; set; }

        public virtual LoaiSanPham? MaLoaiNavigation { get; set; }
        public virtual NhaSanXuat? MaNsxNavigation { get; set; }
        public virtual KhoHang? KhoHang { get; set; }
        public virtual ICollection<ChiTietGioHang> ChiTietGioHangs { get; set; }
        public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }
        public virtual ICollection<DanhGium> DanhGia { get; set; }
    }
}
