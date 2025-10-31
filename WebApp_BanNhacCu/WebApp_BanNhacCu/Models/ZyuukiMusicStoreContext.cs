using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebApp_BanNhacCu.Models
{
    public partial class ZyuukiMusicStoreContext : DbContext
    {
        public ZyuukiMusicStoreContext()
        {
        }

        public ZyuukiMusicStoreContext(DbContextOptions<ZyuukiMusicStoreContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ChiTietGioHang> ChiTietGioHangs { get; set; } = null!;
        public virtual DbSet<ChiTietHoaDon> ChiTietHoaDons { get; set; } = null!;
        public virtual DbSet<DanhGium> DanhGia { get; set; } = null!;
        public virtual DbSet<GioHang> GioHangs { get; set; } = null!;
        public virtual DbSet<HoaDon> HoaDons { get; set; } = null!;
        public virtual DbSet<KhachHang> KhachHangs { get; set; } = null!;
        public virtual DbSet<KhoHang> KhoHangs { get; set; } = null!;
        public virtual DbSet<LoaiSanPham> LoaiSanPhams { get; set; } = null!;
        public virtual DbSet<MaGiamGium> MaGiamGia { get; set; } = null!;
        public virtual DbSet<NhaSanXuat> NhaSanXuats { get; set; } = null!;
        public virtual DbSet<NhanVien> NhanViens { get; set; } = null!;
        public virtual DbSet<QuanLy> QuanLies { get; set; } = null!;
        public virtual DbSet<SanPham> SanPhams { get; set; } = null!;
        public virtual DbSet<TaiKhoan> TaiKhoans { get; set; } = null!;
        public virtual DbSet<VaiTro> VaiTros { get; set; } = null!;
         
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=ZYUUKI\\SQLEXPRESS;Initial Catalog=ZyuukiMusicStore;Integrated Security=True;Encrypt=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChiTietGioHang>(entity =>
            {
                entity.HasKey(e => new { e.MaGh, e.MaSp });

                entity.ToTable("ChiTietGioHang");

                entity.Property(e => e.MaGh).HasColumnName("ma_gh");

                entity.Property(e => e.MaSp)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ma_sp")
                    .IsFixedLength();

                entity.Property(e => e.Gia)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("gia");

                entity.Property(e => e.Soluong).HasColumnName("soluong");

                entity.Property(e => e.Thanhtien)
                    .HasColumnType("decimal(29, 2)")
                    .HasColumnName("thanhtien")
                    .HasComputedColumnSql("([soluong]*[gia])", true);

                entity.HasOne(d => d.MaGhNavigation)
                    .WithMany(p => p.ChiTietGioHangs)
                    .HasForeignKey(d => d.MaGh)
                    .HasConstraintName("FK__ChiTietGi__ma_gh__5AEE82B9");

                entity.HasOne(d => d.MaSpNavigation)
                    .WithMany(p => p.ChiTietGioHangs)
                    .HasForeignKey(d => d.MaSp)
                    .HasConstraintName("FK__ChiTietGi__ma_sp__5BE2A6F2");
            });

            modelBuilder.Entity<ChiTietHoaDon>(entity =>
            {
                entity.HasKey(e => new { e.MaHd, e.MaSp });

                entity.ToTable("ChiTietHoaDon");

                entity.Property(e => e.MaHd).HasColumnName("ma_hd");

                entity.Property(e => e.MaSp)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ma_sp")
                    .IsFixedLength();

                entity.Property(e => e.Chietkhau)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("chietkhau");

                entity.Property(e => e.Gia)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("gia");

                entity.Property(e => e.Soluong).HasColumnName("soluong");

                entity.Property(e => e.Thanhtien)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("thanhtien");

                entity.HasOne(d => d.MaHdNavigation)
                    .WithMany(p => p.ChiTietHoaDons)
                    .HasForeignKey(d => d.MaHd)
                    .HasConstraintName("FK__ChiTietHo__ma_hd__6754599E");

                entity.HasOne(d => d.MaSpNavigation)
                    .WithMany(p => p.ChiTietHoaDons)
                    .HasForeignKey(d => d.MaSp)
                    .HasConstraintName("FK__ChiTietHo__ma_sp__68487DD7");
            });

            modelBuilder.Entity<DanhGium>(entity =>
            {
                entity.HasKey(e => new { e.MaKh, e.MaSp });

                entity.Property(e => e.MaKh).HasColumnName("ma_kh");

                entity.Property(e => e.MaSp)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ma_sp")
                    .IsFixedLength();

                entity.Property(e => e.Noidung)
                    .HasMaxLength(500)
                    .HasColumnName("noidung");

                entity.Property(e => e.Sosao).HasColumnName("sosao");

                entity.HasOne(d => d.MaKhNavigation)
                    .WithMany(p => p.DanhGia)
                    .HasForeignKey(d => d.MaKh)
                    .HasConstraintName("FK__DanhGia__ma_kh__6C190EBB");

                entity.HasOne(d => d.MaSpNavigation)
                    .WithMany(p => p.DanhGia)
                    .HasForeignKey(d => d.MaSp)
                    .HasConstraintName("FK__DanhGia__ma_sp__6D0D32F4");
            });

            modelBuilder.Entity<GioHang>(entity =>
            {
                entity.HasKey(e => e.MaGh)
                    .HasName("PK__GioHang__0FE116612D394018");

                entity.ToTable("GioHang");

                entity.Property(e => e.MaGh).HasColumnName("ma_gh");

                entity.Property(e => e.MaKh).HasColumnName("ma_kh");

                entity.Property(e => e.Ngaytao)
                    .HasColumnType("datetime")
                    .HasColumnName("ngaytao")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Tongtien)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("tongtien")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Trangthai)
                    .HasMaxLength(50)
                    .HasColumnName("trangthai")
                    .HasDefaultValueSql("(N'Đang hoạt động')");

                entity.HasOne(d => d.MaKhNavigation)
                    .WithMany(p => p.GioHangs)
                    .HasForeignKey(d => d.MaKh)
                    .HasConstraintName("FK__GioHang__ma_kh__5812160E");
            });

            modelBuilder.Entity<HoaDon>(entity =>
            {
                entity.HasKey(e => e.MaHd)
                    .HasName("PK__HoaDon__0FE16E8672201D0D");

                entity.ToTable("HoaDon");

                entity.Property(e => e.MaHd).HasColumnName("ma_hd");

                entity.Property(e => e.HanThanhtoan)
                    .HasColumnType("date")
                    .HasColumnName("han_thanhtoan")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.MaGiamgia)
                    .HasMaxLength(50)
                    .HasColumnName("ma_giamgia");

                entity.Property(e => e.Ngayxuat)
                    .HasColumnType("datetime")
                    .HasColumnName("ngayxuat")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Tongtien)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("tongtien");

                entity.Property(e => e.TtThanhtoan)
                    .HasMaxLength(50)
                    .HasColumnName("tt_thanhtoan")
                    .HasDefaultValueSql("(N'Chưa thanh toán')");

                entity.HasOne(d => d.MaGiamgiaNavigation)
                    .WithMany(p => p.HoaDons)
                    .HasForeignKey(d => d.MaGiamgia)
                    .HasConstraintName("FK__HoaDon__ma_giamg__6383C8BA");
            });

            modelBuilder.Entity<KhachHang>(entity =>
            {
                entity.HasKey(e => e.MaKh)
                    .HasName("PK__KhachHan__0FE0B7EE53E8EF6F");

                entity.ToTable("KhachHang");

                entity.Property(e => e.MaKh)
                    .ValueGeneratedNever()
                    .HasColumnName("ma_kh");

                entity.Property(e => e.Diachi)
                    .HasMaxLength(255)
                    .HasColumnName("diachi");

                entity.Property(e => e.Tenkh)
                    .HasMaxLength(100)
                    .HasColumnName("tenkh");

                entity.HasOne(d => d.MaKhNavigation)
                    .WithOne(p => p.KhachHang)
                    .HasForeignKey<KhachHang>(d => d.MaKh)
                    .HasConstraintName("FK__KhachHang__ma_kh__3D5E1FD2");
            });

            modelBuilder.Entity<KhoHang>(entity =>
            {
                entity.HasKey(e => e.MaSp)
                    .HasName("PK__KhoHang__0FE0F488DD45802C");

                entity.ToTable("KhoHang");

                entity.Property(e => e.MaSp)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ma_sp")
                    .IsFixedLength();

                entity.Property(e => e.Ngaycapnhat)
                    .HasColumnType("datetime")
                    .HasColumnName("ngaycapnhat")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Soluongton).HasColumnName("soluongton");

                entity.HasOne(d => d.MaSpNavigation)
                    .WithOne(p => p.KhoHang)
                    .HasForeignKey<KhoHang>(d => d.MaSp)
                    .HasConstraintName("FK__KhoHang__ma_sp__52593CB8");
            });

            modelBuilder.Entity<LoaiSanPham>(entity =>
            {
                entity.HasKey(e => e.MaLoai)
                    .HasName("PK__LoaiSanP__D9476E578EC561AE");

                entity.ToTable("LoaiSanPham");

                entity.Property(e => e.MaLoai)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ma_loai")
                    .IsFixedLength();

                entity.Property(e => e.Mota)
                    .HasMaxLength(100)
                    .HasColumnName("mota");

                entity.Property(e => e.Tenloai)
                    .HasMaxLength(50)
                    .HasColumnName("tenloai");
            });

            modelBuilder.Entity<MaGiamGium>(entity =>
            {
                entity.HasKey(e => e.MaGiamgia)
                    .HasName("PK__MaGiamGi__5C99442385EAF87F");

                entity.Property(e => e.MaGiamgia)
                    .HasMaxLength(50)
                    .HasColumnName("ma_giamgia");

                entity.Property(e => e.Giatri)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("giatri");

                entity.Property(e => e.Ngaybatdau)
                    .HasColumnType("date")
                    .HasColumnName("ngaybatdau");

                entity.Property(e => e.Ngayketthuc)
                    .HasColumnType("date")
                    .HasColumnName("ngayketthuc");

                entity.Property(e => e.Trangthai)
                    .HasColumnName("trangthai")
                    .HasComputedColumnSql("(case when getdate()<[ngaybatdau] then (-1) when getdate()>[ngayketthuc] then (0) else (1) end)", false);
            });

            modelBuilder.Entity<NhaSanXuat>(entity =>
            {
                entity.HasKey(e => e.MaNsx)
                    .HasName("PK__NhaSanXu__04C167683154F5E8");

                entity.ToTable("NhaSanXuat");

                entity.Property(e => e.MaNsx)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ma_nsx")
                    .IsFixedLength();

                entity.Property(e => e.Diachi)
                    .HasMaxLength(200)
                    .HasColumnName("diachi");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .HasColumnName("email");

                entity.Property(e => e.Sdt)
                    .HasMaxLength(15)
                    .HasColumnName("sdt");

                entity.Property(e => e.Tennsx)
                    .HasMaxLength(50)
                    .HasColumnName("tennsx");
            });

            modelBuilder.Entity<NhanVien>(entity =>
            {
                entity.HasKey(e => e.MaNv)
                    .HasName("PK__NhanVien__0FE15F7C3E69B914");

                entity.ToTable("NhanVien");

                entity.HasIndex(e => e.Cccd, "UQ__NhanVien__37D42BFA9928269D")
                    .IsUnique();

                entity.Property(e => e.MaNv)
                    .ValueGeneratedNever()
                    .HasColumnName("ma_nv");

                entity.Property(e => e.Cccd)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("cccd")
                    .IsFixedLength();

                entity.Property(e => e.Ngaycap)
                    .HasColumnType("date")
                    .HasColumnName("ngaycap");

                entity.Property(e => e.Noicap)
                    .HasMaxLength(100)
                    .HasColumnName("noicap");

                entity.Property(e => e.Phai)
                    .IsRequired()
                    .HasColumnName("phai")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Tennv)
                    .HasMaxLength(100)
                    .HasColumnName("tennv");

                entity.HasOne(d => d.MaNvNavigation)
                    .WithOne(p => p.NhanVien)
                    .HasForeignKey<NhanVien>(d => d.MaNv)
                    .HasConstraintName("FK__NhanVien__ma_nv__4316F928");
            });

            modelBuilder.Entity<QuanLy>(entity =>
            {
                entity.HasKey(e => e.MaQl)
                    .HasName("PK__QuanLy__0FE0A75583CFD72B");

                entity.ToTable("QuanLy");

                entity.Property(e => e.MaQl)
                    .ValueGeneratedNever()
                    .HasColumnName("ma_ql");

                entity.Property(e => e.Tenql)
                    .HasMaxLength(100)
                    .HasColumnName("tenql");

                entity.HasOne(d => d.MaQlNavigation)
                    .WithOne(p => p.QuanLy)
                    .HasForeignKey<QuanLy>(d => d.MaQl)
                    .HasConstraintName("FK__QuanLy__ma_ql__45F365D3");
            });

            modelBuilder.Entity<SanPham>(entity =>
            {
                entity.HasKey(e => e.MaSp)
                    .HasName("PK__SanPham__0FE0F488198E2C04");

                entity.ToTable("SanPham");

                entity.Property(e => e.MaSp)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ma_sp")
                    .IsFixedLength();

                entity.Property(e => e.Anhsp)
                    .HasMaxLength(255)
                    .HasColumnName("anhsp");

                entity.Property(e => e.Giasp)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("giasp");

                entity.Property(e => e.MaLoai)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ma_loai")
                    .IsFixedLength();

                entity.Property(e => e.MaNsx)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ma_nsx")
                    .IsFixedLength();

                entity.Property(e => e.Mota).HasColumnName("mota");

                entity.Property(e => e.Tensp)
                    .HasMaxLength(100)
                    .HasColumnName("tensp");

                entity.HasOne(d => d.MaLoaiNavigation)
                    .WithMany(p => p.SanPhams)
                    .HasForeignKey(d => d.MaLoai)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__SanPham__ma_loai__4D94879B");

                entity.HasOne(d => d.MaNsxNavigation)
                    .WithMany(p => p.SanPhams)
                    .HasForeignKey(d => d.MaNsx)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__SanPham__ma_nsx__4E88ABD4");
            });

            modelBuilder.Entity<TaiKhoan>(entity =>
            {
                entity.HasKey(e => e.MaTk)
                    .HasName("PK__TaiKhoan__0FE0CD14E992262E");

                entity.ToTable("TaiKhoan");

                entity.HasIndex(e => e.Email, "UQ__TaiKhoan__AB6E6164B02ADEB6")
                    .IsUnique();

                entity.Property(e => e.MaTk).HasColumnName("ma_tk");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .HasColumnName("email");

                entity.Property(e => e.Hinhanh)
                    .HasMaxLength(255)
                    .HasColumnName("hinhanh");

                entity.Property(e => e.MaVt)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ma_vt")
                    .IsFixedLength();

                entity.Property(e => e.Matkhau)
                    .HasMaxLength(255)
                    .HasColumnName("matkhau");

                entity.Property(e => e.Sdt)
                    .HasMaxLength(20)
                    .HasColumnName("sdt");

                entity.HasOne(d => d.MaVtNavigation)
                    .WithMany(p => p.TaiKhoans)
                    .HasForeignKey(d => d.MaVt)
                    .HasConstraintName("FK__TaiKhoan__ma_vt__3A81B327");
            });

            modelBuilder.Entity<VaiTro>(entity =>
            {
                entity.HasKey(e => e.MaVt)
                    .HasName("PK__VaiTro__0FE09C68BE16D09B");

                entity.ToTable("VaiTro");

                entity.Property(e => e.MaVt)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ma_vt")
                    .IsFixedLength();

                entity.Property(e => e.Mota)
                    .HasMaxLength(100)
                    .HasColumnName("mota");

                entity.Property(e => e.Tenvt)
                    .HasMaxLength(50)
                    .HasColumnName("tenvt");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
