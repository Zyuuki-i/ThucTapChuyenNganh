using System;
using System.Collections.Generic;

namespace WebApp_BanNhacCu.Models
{
    public partial class SanPham
    {
        public SanPham()
        {
            CapNhats = new HashSet<CapNhat>();
            ChiTietDonDatHangs = new HashSet<ChiTietDonDatHang>();
            DanhGia = new HashSet<DanhGia>();
            Hinhs = new HashSet<Hinh>();
        }

        public string MaSp { get; set; } = null!;
        public string Tensp { get; set; } = null!;
        public string? MaNsx { get; set; }
        public string? MaLoai { get; set; }
        public decimal Giasp { get; set; }
        public int? Soluongton { get; set; }
        public string? Mota { get; set; }

        public virtual LoaiSanPham? MaLoaiNavigation { get; set; }
        public virtual NhaSanXuat? MaNsxNavigation { get; set; }
        public virtual ICollection<CapNhat> CapNhats { get; set; }
        public virtual ICollection<ChiTietDonDatHang> ChiTietDonDatHangs { get; set; }
        public virtual ICollection<DanhGia> DanhGia { get; set; }
        public virtual ICollection<Hinh> Hinhs { get; set; }
    }
}
