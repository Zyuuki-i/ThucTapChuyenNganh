using System;
using System.Collections.Generic;
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

        public virtual DbSet<ChiTietDonDatHang> ChiTietDonDatHangs { get; set; } = null!;
        public virtual DbSet<DanhGia> DanhGia { get; set; } = null!;
        public virtual DbSet<DonDatHang> DonDatHangs { get; set; } = null!;
        public virtual DbSet<Hinh> Hinhs { get; set; } = null!;
        public virtual DbSet<KhachHang> KhachHangs { get; set; } = null!;
        public virtual DbSet<KhoHang> KhoHangs { get; set; } = null!;
        public virtual DbSet<LoaiSanPham> LoaiSanPhams { get; set; } = null!;
        public virtual DbSet<MaGiamGia> MaGiamGia { get; set; } = null!;
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
                optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=ZyuukiMusicStore;Integrated Security=True;Encrypt=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChiTietDonDatHang>(entity =>
            {
                entity.HasKey(e => new { e.MaDdh, e.MaSp })
                    .HasName("PK_ChiTietHoaDon");

                entity.ToTable("ChiTietDonDatHang");

                entity.Property(e => e.MaDdh).HasColumnName("ma_ddh");

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

                entity.HasOne(d => d.MaDdhNavigation)
                    .WithMany(p => p.ChiTietDonDatHangs)
                    .HasForeignKey(d => d.MaDdh)
                    .HasConstraintName("FK__ChiTietDo__ma_dd__60A75C0F");

                entity.HasOne(d => d.MaSpNavigation)
                    .WithMany(p => p.ChiTietDonDatHangs)
                    .HasForeignKey(d => d.MaSp)
                    .HasConstraintName("FK__ChiTietDo__ma_sp__619B8048");
            });

            modelBuilder.Entity<DanhGia>(entity =>
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
                    .HasConstraintName("FK__DanhGia__ma_kh__656C112C");

                entity.HasOne(d => d.MaSpNavigation)
                    .WithMany(p => p.DanhGia)
                    .HasForeignKey(d => d.MaSp)
                    .HasConstraintName("FK__DanhGia__ma_sp__66603565");
            });

            modelBuilder.Entity<DonDatHang>(entity =>
            {
                entity.HasKey(e => e.MaDdh)
                    .HasName("PK__DonDatHa__057B0B6B76D36673");

                entity.ToTable("DonDatHang");

                entity.Property(e => e.MaDdh).HasColumnName("ma_ddh");

                entity.Property(e => e.MaGiamgia)
                    .HasMaxLength(50)
                    .HasColumnName("ma_giamgia");

                entity.Property(e => e.MaKh).HasColumnName("ma_kh");

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
                    .WithMany(p => p.DonDatHangs)
                    .HasForeignKey(d => d.MaGiamgia)
                    .HasConstraintName("FK__DonDatHan__ma_gi__5BE2A6F2");

                entity.HasOne(d => d.MaKhNavigation)
                    .WithMany(p => p.DonDatHangs)
                    .HasForeignKey(d => d.MaKh)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DonDatHan__ma_kh__5CD6CB2B");
            });

            modelBuilder.Entity<Hinh>(entity =>
            {
                entity.HasKey(e => e.MaHinh)
                    .HasName("PK__Hinh__78C576F0B132DEC8");

                entity.ToTable("Hinh");

                entity.Property(e => e.MaHinh).HasColumnName("ma_hinh");

                entity.Property(e => e.MaSp)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ma_sp")
                    .IsFixedLength();

                entity.Property(e => e.Url)
                    .HasMaxLength(255)
                    .HasColumnName("url");

                entity.HasOne(d => d.MaSpNavigation)
                    .WithMany(p => p.Hinhs)
                    .HasForeignKey(d => d.MaSp)
                    .HasConstraintName("FK__Hinh__ma_sp__5165187F");
            });

            modelBuilder.Entity<KhachHang>(entity =>
            {
                entity.HasKey(e => e.MaKh)
                    .HasName("PK__KhachHan__0FE0B7EEC6F0D69E");

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
                    .HasName("PK__KhoHang__0FE0F488E64CEE5B");

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
                    .HasConstraintName("FK__KhoHang__ma_sp__5535A963");
            });

            modelBuilder.Entity<LoaiSanPham>(entity =>
            {
                entity.HasKey(e => e.MaLoai)
                    .HasName("PK__LoaiSanP__D9476E577F9D84F5");

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

            modelBuilder.Entity<MaGiamGia>(entity =>
            {
                entity.HasKey(e => e.MaGiamgia)
                    .HasName("PK__MaGiamGi__5C994423EEA4F286");

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
                    .HasName("PK__NhaSanXu__04C16768F70B87ED");

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
                    .HasName("PK__NhanVien__0FE15F7C91AA843C");

                entity.ToTable("NhanVien");

                entity.HasIndex(e => e.Cccd, "UQ__NhanVien__37D42BFA5AE6A30F")
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
                    .HasName("PK__QuanLy__0FE0A7551DA1107D");

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
                    .HasName("PK__SanPham__0FE0F488DA397F13");

                entity.ToTable("SanPham");

                entity.Property(e => e.MaSp)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ma_sp")
                    .IsFixedLength();

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
                    .HasName("PK__TaiKhoan__0FE0CD1429A5CFBA");

                entity.ToTable("TaiKhoan");

                entity.HasIndex(e => e.Email, "UQ__TaiKhoan__AB6E616470934068")
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
                    .HasName("PK__VaiTro__0FE09C68407113F6");

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
