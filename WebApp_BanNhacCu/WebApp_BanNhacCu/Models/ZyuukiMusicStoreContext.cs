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
        public virtual DbSet<KhoHang> KhoHangs { get; set; } = null!;
        public virtual DbSet<LoaiSanPham> LoaiSanPhams { get; set; } = null!;
        public virtual DbSet<NguoiDung> NguoiDungs { get; set; } = null!;
        public virtual DbSet<NhaSanXuat> NhaSanXuats { get; set; } = null!;
        public virtual DbSet<NhanVien> NhanViens { get; set; } = null!;
        public virtual DbSet<SanPham> SanPhams { get; set; } = null!;
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
                    .HasConstraintName("FK__ChiTietDo__ma_dd__5812160E");

                entity.HasOne(d => d.MaSpNavigation)
                    .WithMany(p => p.ChiTietDonDatHangs)
                    .HasForeignKey(d => d.MaSp)
                    .HasConstraintName("FK__ChiTietDo__ma_sp__59063A47");
            });

            modelBuilder.Entity<DanhGia>(entity =>
            {
                entity.HasKey(e => new { e.MaNd, e.MaSp });

                entity.Property(e => e.MaNd).HasColumnName("ma_nd");

                entity.Property(e => e.MaSp)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ma_sp")
                    .IsFixedLength();

                entity.Property(e => e.Noidung)
                    .HasMaxLength(500)
                    .HasColumnName("noidung");

                entity.Property(e => e.Sosao).HasColumnName("sosao");

                entity.HasOne(d => d.MaNdNavigation)
                    .WithMany(p => p.DanhGia)
                    .HasForeignKey(d => d.MaNd)
                    .HasConstraintName("FK__DanhGia__ma_nd__5CD6CB2B");

                entity.HasOne(d => d.MaSpNavigation)
                    .WithMany(p => p.DanhGia)
                    .HasForeignKey(d => d.MaSp)
                    .HasConstraintName("FK__DanhGia__ma_sp__5DCAEF64");
            });

            modelBuilder.Entity<DonDatHang>(entity =>
            {
                entity.HasKey(e => e.MaDdh)
                    .HasName("PK__DonDatHa__057B0B6B83029F79");

                entity.ToTable("DonDatHang");

                entity.Property(e => e.MaDdh).HasColumnName("ma_ddh");

                entity.Property(e => e.Diachi)
                    .HasMaxLength(255)
                    .HasColumnName("diachi");

                entity.Property(e => e.MaNd).HasColumnName("ma_nd");

                entity.Property(e => e.Ngaydat)
                    .HasColumnType("datetime")
                    .HasColumnName("ngaydat")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Tongtien)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("tongtien");

                entity.Property(e => e.Trangthai)
                    .HasMaxLength(50)
                    .HasColumnName("trangthai");

                entity.Property(e => e.TtThanhtoan)
                    .HasMaxLength(50)
                    .HasColumnName("tt_thanhtoan")
                    .HasDefaultValueSql("(N'Chưa thanh toán')");

                entity.HasOne(d => d.MaNdNavigation)
                    .WithMany(p => p.DonDatHangs)
                    .HasForeignKey(d => d.MaNd)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DonDatHan__ma_nd__5441852A");
            });

            modelBuilder.Entity<Hinh>(entity =>
            {
                entity.HasKey(e => e.MaHinh)
                    .HasName("PK__Hinh__78C576F0DD11FDE1");

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
                    .HasConstraintName("FK__Hinh__ma_sp__4BAC3F29");
            });

            modelBuilder.Entity<KhoHang>(entity =>
            {
                entity.HasKey(e => e.MaSp)
                    .HasName("PK__KhoHang__0FE0F488929FD47F");

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
                    .HasConstraintName("FK__KhoHang__ma_sp__4F7CD00D");
            });

            modelBuilder.Entity<LoaiSanPham>(entity =>
            {
                entity.HasKey(e => e.MaLoai)
                    .HasName("PK__LoaiSanP__D9476E5796134204");

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

            modelBuilder.Entity<NguoiDung>(entity =>
            {
                entity.HasKey(e => e.MaNd)
                    .HasName("PK__NguoiDun__0FE15F4E0121D6A9");

                entity.ToTable("NguoiDung");

                entity.HasIndex(e => e.Email, "UQ__NguoiDun__AB6E61644EC52F66")
                    .IsUnique();

                entity.Property(e => e.MaNd).HasColumnName("ma_nd");

                entity.Property(e => e.Diachi)
                    .HasMaxLength(255)
                    .HasColumnName("diachi");

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

                entity.Property(e => e.Tennd)
                    .HasMaxLength(100)
                    .HasColumnName("tennd");

                entity.HasOne(d => d.MaVtNavigation)
                    .WithMany(p => p.NguoiDungs)
                    .HasForeignKey(d => d.MaVt)
                    .HasConstraintName("FK__NguoiDung__ma_vt__3A81B327");
            });

            modelBuilder.Entity<NhaSanXuat>(entity =>
            {
                entity.HasKey(e => e.MaNsx)
                    .HasName("PK__NhaSanXu__04C16768859B92A2");

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
                    .HasName("PK__NhanVien__0FE15F7C4FD84171");

                entity.ToTable("NhanVien");

                entity.HasIndex(e => e.Cccd, "UQ__NhanVien__37D42BFA1D85D7F2")
                    .IsUnique();

                entity.Property(e => e.MaNv)
                    .ValueGeneratedNever()
                    .HasColumnName("ma_nv");

                entity.Property(e => e.Cccd)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("cccd")
                    .IsFixedLength();

                entity.Property(e => e.Phai)
                    .IsRequired()
                    .HasColumnName("phai")
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.MaNvNavigation)
                    .WithOne(p => p.NhanVien)
                    .HasForeignKey<NhanVien>(d => d.MaNv)
                    .HasConstraintName("FK__NhanVien__ma_nv__403A8C7D");
            });

            modelBuilder.Entity<SanPham>(entity =>
            {
                entity.HasKey(e => e.MaSp)
                    .HasName("PK__SanPham__0FE0F4888A26FF85");

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
                    .HasConstraintName("FK__SanPham__ma_loai__47DBAE45");

                entity.HasOne(d => d.MaNsxNavigation)
                    .WithMany(p => p.SanPhams)
                    .HasForeignKey(d => d.MaNsx)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__SanPham__ma_nsx__48CFD27E");
            });

            modelBuilder.Entity<VaiTro>(entity =>
            {
                entity.HasKey(e => e.MaVt)
                    .HasName("PK__VaiTro__0FE09C6876969C8C");

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
