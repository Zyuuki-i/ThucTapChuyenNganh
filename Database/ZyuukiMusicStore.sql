USE master
GO
IF EXISTS (SELECT NAME FROM sysdatabases WHERE NAME='ZyuukiMusicStore')
    DROP DATABASE [ZyuukiMusicStore];
GO
CREATE DATABASE ZyuukiMusicStore;
GO
USE ZyuukiMusicStore;
GO

CREATE TABLE [VaiTro] (
    ma_vt CHAR(10) PRIMARY KEY,
    tenvt NVARCHAR(50) NOT NULL,
    mota NVARCHAR(100)
);
GO

CREATE TABLE [TaiKhoan] (
    ma_tk INT IDENTITY(1,1) PRIMARY KEY,
	sdt NVARCHAR(20),
    matkhau NVARCHAR(255) NOT NULL,
    ma_vt CHAR(10) NOT NULL,
    email NVARCHAR(100) UNIQUE NOT NULL,
	hinhanh NVARCHAR (255),
    FOREIGN KEY (ma_vt) REFERENCES VaiTro(ma_vt) ON DELETE CASCADE ON UPDATE CASCADE
);
GO

CREATE TABLE [KhachHang] (
    ma_kh INT PRIMARY KEY,
    tenkh NVARCHAR(100) NOT NULL,
    diachi NVARCHAR(255),
    FOREIGN KEY (ma_kh) REFERENCES TaiKhoan(ma_tk) ON DELETE CASCADE ON UPDATE CASCADE
);
GO

CREATE TABLE [NhanVien] (
    ma_nv INT PRIMARY KEY,
    tennv NVARCHAR(100) NOT NULL,
	phai BIT NOT NULL DEFAULT 1,
    cccd CHAR(12) NOT NULL UNIQUE,
	ngaycap Date NOT NULL,
	noicap NVARCHAR(100) NOT NULL,
	CONSTRAINT CK_NguoiDung_CCCD_Format CHECK (LEN(cccd) = 12 AND cccd NOT LIKE '%[^0-9]%'),
    FOREIGN KEY (ma_nv) REFERENCES TaiKhoan(ma_tk) ON DELETE CASCADE ON UPDATE CASCADE
);
GO

CREATE TABLE [QuanLy] (
    ma_ql INT PRIMARY KEY,
    tenql NVARCHAR(100) NOT NULL,
    FOREIGN KEY (ma_ql) REFERENCES TaiKhoan(ma_tk) ON DELETE CASCADE ON UPDATE CASCADE
);
GO

CREATE TABLE [LoaiSanPham] (
    ma_loai CHAR(10) PRIMARY KEY,
    tenloai NVARCHAR(50) NOT NULL,
    mota NVARCHAR(100)
);
GO

CREATE TABLE [NhaSanXuat] (
    ma_nsx CHAR(10) PRIMARY KEY,
    tennsx NVARCHAR(50) NOT NULL,                   
    diachi NVARCHAR(200),                    
    sdt NVARCHAR(15),                
    email NVARCHAR(100),                     
);
GO

CREATE TABLE [SanPham] (
    ma_sp CHAR(10) PRIMARY KEY,
    tensp NVARCHAR(100) NOT NULL,
    ma_nsx CHAR(10),
    ma_loai CHAR(10),
    giasp DECIMAL(18,2) CHECK (giasp >= 0) NOT NULL,
    anhsp NVARCHAR(255),
    mota NVARCHAR(MAX),
    FOREIGN KEY (ma_loai) REFERENCES LoaiSanPham(ma_loai)
        ON DELETE CASCADE ON UPDATE CASCADE,
	FOREIGN KEY (ma_nsx) REFERENCES NhaSanXuat(ma_nsx)
        ON DELETE CASCADE ON UPDATE CASCADE,
);
GO

CREATE TABLE [KhoHang] (
    ma_sp CHAR(10) PRIMARY KEY,
    soluongton INT NOT NULL,
    ngaycapnhat DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (ma_sp) REFERENCES SanPham(ma_sp) ON DELETE CASCADE ON UPDATE CASCADE
);
GO

CREATE TABLE [GioHang] (
    ma_gh INT IDENTITY(1,1) PRIMARY KEY,
    ma_kh INT NOT NULL,
    ngaytao DATETIME DEFAULT GETDATE(),
    tongtien DECIMAL(18,2) DEFAULT 0,
    trangthai NVARCHAR(50) DEFAULT N'Đang hoạt động',
    FOREIGN KEY (ma_kh) REFERENCES [KhachHang](ma_kh) ON DELETE CASCADE ON UPDATE CASCADE
);
GO

CREATE TABLE [ChiTietGioHang] (
    ma_gh INT NOT NULL,
    ma_sp CHAR(10) NOT NULL,
    soluong INT NOT NULL,
	gia DECIMAL(18,2) NOT NULL,
    thanhtien AS (soluong * gia) PERSISTED,
	CONSTRAINT PK_ChiTietGioHang PRIMARY KEY (ma_gh, ma_sp),
    FOREIGN KEY (ma_gh) REFERENCES GioHang(ma_gh) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (ma_sp) REFERENCES SanPham(ma_sp) ON DELETE CASCADE ON UPDATE CASCADE
);
GO

CREATE TABLE [MaGiamGia] (
    ma_giamgia NVARCHAR(50) NOT NULL PRIMARY KEY,
    giatri DECIMAL(10,2) NOT NULL,
    ngaybatdau DATE,
    ngayketthuc DATE,
	trangthai AS 
    CASE 
        WHEN GETDATE() < ngaybatdau THEN -1 --Chưa hiệu lực
        WHEN GETDATE() > ngayketthuc THEN 0 -- Hết hạn
        ELSE 1 -- Đang hoạt động
    END
);
GO

CREATE TABLE [HoaDon] (
    ma_hd INT IDENTITY(1,1) PRIMARY KEY,
	ma_giamgia NVARCHAR(50),
    ngayxuat DATETIME DEFAULT GETDATE(),
    tongtien DECIMAL(18,2),
    tt_thanhtoan NVARCHAR(50) DEFAULT N'Chưa thanh toán',
	han_thanhtoan DATE DEFAULT GETDATE(),
	FOREIGN KEY (ma_giamgia) REFERENCES [MaGiamGia] (ma_giamgia) ON UPDATE CASCADE
);
GO

CREATE TABLE [ChiTietHoaDon] (
    ma_hd INT NOT NULL,              
    ma_sp CHAR(10) NOT NULL,         
    soluong INT NOT NULL CHECK (soluong > 0),
    gia DECIMAL(18,2) NOT NULL,   
	chietkhau DECIMAL(10,2),
    thanhtien DECIMAL(18,2), 
	CONSTRAINT PK_ChiTietHoaDon PRIMARY KEY (ma_hd, ma_sp),
    FOREIGN KEY (ma_hd) REFERENCES [HoaDon](ma_hd)
        ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (ma_sp) REFERENCES [SanPham](ma_sp)
        ON DELETE CASCADE ON UPDATE CASCADE
);
GO	

CREATE TABLE [DanhGia] (
    ma_kh INT NOT NULL,
    ma_sp CHAR(10) NOT NULL,
    noidung NVARCHAR(500),
    sosao INT CHECK (sosao BETWEEN 1 AND 5),
	CONSTRAINT PK_DanhGia PRIMARY KEY (ma_kh, ma_sp),
    FOREIGN KEY (ma_kh) REFERENCES [KhachHang](ma_kh) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (ma_sp) REFERENCES [SanPham](ma_sp) ON DELETE CASCADE ON UPDATE CASCADE,
);
GO

