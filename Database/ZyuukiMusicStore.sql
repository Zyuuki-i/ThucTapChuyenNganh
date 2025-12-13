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

CREATE TABLE [NhanVien] (
    ma_nv CHAR(10) PRIMARY KEY,
    tennv NVARCHAR(100) NOT NULL,
    matkhau NVARCHAR(255) NOT NULL,
	phai BIT NOT NULL,
    sdt NVARCHAR(20),
    email NVARCHAR(100) UNIQUE NOT NULL,
    cccd CHAR(12) NOT NULL UNIQUE,
	diachi NVARCHAR(255),
	hinh NVARCHAR(255),
    ma_vt CHAR(10) NOT NULL, 
	trangthai BIT,
	CONSTRAINT CK_NhanVien_CCCD_Format CHECK (LEN(cccd) = 12 AND cccd NOT LIKE '%[^0-9]%'),
    FOREIGN KEY (ma_vt) REFERENCES VaiTro(ma_vt) ON DELETE CASCADE ON UPDATE CASCADE
);
GO

CREATE TABLE [NguoiDung] (
    ma_nd INT IDENTITY(1,1) PRIMARY KEY,
	tennd NVARCHAR(100) NOT NULL,
    matkhau NVARCHAR(255) NOT NULL,
	sdt NVARCHAR(20),
	diachi NVARCHAR(255),
	phuongxa NVARCHAR(100),
	tinhthanh NVARCHAR(100),
    email NVARCHAR(100) UNIQUE NOT NULL,
	hinh NVARCHAR (255),
	trangthai BIT
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
    sdt NVARCHAR(20),                
    email NVARCHAR(100),                     
);
GO

CREATE TABLE [SanPham] (
    ma_sp CHAR(10) PRIMARY KEY,
    tensp NVARCHAR(100) NOT NULL,
    ma_nsx CHAR(10),
    ma_loai CHAR(10),
    giasp DECIMAL(18,2) CHECK (giasp >= 0) NOT NULL,
    soluongton INT DEFAULT 0 CHECK (soluongton >= 0), -- Thêm cột này từ bảng KhoHang cũ
    mota NVARCHAR(MAX),
    FOREIGN KEY (ma_loai) REFERENCES LoaiSanPham(ma_loai)
        ON DELETE CASCADE ON UPDATE CASCADE,
	FOREIGN KEY (ma_nsx) REFERENCES NhaSanXuat(ma_nsx)
        ON DELETE CASCADE ON UPDATE CASCADE,
);
GO

CREATE TABLE [Hinh] (
    ma_hinh INT IDENTITY(1,1) PRIMARY KEY,
    ma_sp CHAR(10) NOT NULL,
    tenhinh NVARCHAR(255), 
	FOREIGN KEY (ma_sp) REFERENCES SanPham(ma_sp)
	ON DELETE CASCADE ON UPDATE CASCADE,
);
GO

CREATE TABLE [DonDatHang] (
    ma_ddh INT IDENTITY(1,1) PRIMARY KEY,
	ma_nd INT NOT NULL, 
    ma_nv CHAR(10),
	nguoinhan NVARCHAR(100),
	sdt NVARCHAR(15),
	diachi NVARCHAR(255) NOT NULL,
	phuongxa NVARCHAR(100),
	tinhthanh NVARCHAR(100),
    ngaydat DATETIME DEFAULT GETDATE(),
    tongtien DECIMAL(18,2),
	trangthai NVARCHAR(50) NOT NULL,
    tt_thanhtoan NVARCHAR(50) DEFAULT N'Chưa thanh toán',
	phuongthuc NVARCHAR(50),
	FOREIGN KEY (ma_nd) REFERENCES [NguoiDung] (ma_nd) ON UPDATE CASCADE,
    FOREIGN KEY (ma_nv) REFERENCES [NhanVien] (ma_nv) ON UPDATE CASCADE 
);
GO

CREATE TABLE [GiaoHang] (
	ma_gh INT IDENTITY PRIMARY KEY,
    ma_ddh INT NOT NULL,              
    ma_nv CHAR(10) NOT NULL,    
	ngaybd DATE,
	ngaykt DATE,
    tongthu DECIMAL(18,2),
	trangthai NVARCHAR(50),
    FOREIGN KEY (ma_ddh) REFERENCES [DonDatHang](ma_ddh),
    FOREIGN KEY (ma_nv) REFERENCES [NhanVien](ma_nv)
);
GO	

CREATE TABLE [ChiTietDonDatHang] (
    ma_ddh INT NOT NULL,              
    ma_sp CHAR(10) NOT NULL,         
    soluong INT NOT NULL CHECK (soluong > 0),
    gia DECIMAL(18,2) NOT NULL,   
    thanhtien DECIMAL(18,2), 
	CONSTRAINT PK_ChiTietHoaDon PRIMARY KEY (ma_ddh, ma_sp),
    FOREIGN KEY (ma_ddh) REFERENCES [DonDatHang](ma_ddh)
        ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (ma_sp) REFERENCES [SanPham](ma_sp)
        ON DELETE CASCADE ON UPDATE CASCADE
);
GO	

CREATE TABLE [DanhGia] (
    ma_nd INT NOT NULL,
    ma_sp CHAR(10) NOT NULL,
    noidung NVARCHAR(500),
    sosao INT CHECK (sosao BETWEEN 1 AND 5),
	CONSTRAINT PK_DanhGia PRIMARY KEY (ma_nd, ma_sp),
    FOREIGN KEY (ma_nd) REFERENCES [NguoiDung](ma_nd) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (ma_sp) REFERENCES [SanPham](ma_sp) ON DELETE CASCADE ON UPDATE CASCADE,
);
GO

CREATE TABLE [CapNhat] (
	ma_cn INT IDENTITY(1,1) NOT NULL,
    ma_nv CHAR(10) NOT NULL,
    ma_sp CHAR(10) NOT NULL,
    ngaycapnhat DATETIME DEFAULT GETDATE(),
    CONSTRAINT PK_CapNhat PRIMARY KEY (ma_cn, ma_nv, ma_sp),
    FOREIGN KEY (ma_nv) REFERENCES NhanVien(ma_nv) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (ma_sp) REFERENCES SanPham(ma_sp) ON DELETE CASCADE ON UPDATE CASCADE
);
GO

CREATE TABLE [GiamGia] (
    ma_gg CHAR(10) NOT NULL PRIMARY KEY,
    tenma NVARCHAR(50) NOT NULL,
    loaima NVARCHAR(50),
	dieukien DECIMAL(18,2),
	phantramgiam INT CHECK (phantramgiam > 0 AND phantramgiam <= 100),
	ngaybd DATE,
	ngaykt DATE
);
GO

CREATE TABLE [ChiTietGiamGia] (
	ma_nd INT NOT NULL,
    ma_gg CHAR(10) NOT NULL,
    soluong INT DEFAULT 0,
    CONSTRAINT PK_CTGiamGia PRIMARY KEY (ma_nd, ma_gg),
    FOREIGN KEY (ma_nd) REFERENCES NguoiDung(ma_nd) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (ma_gg) REFERENCES GiamGia(ma_gg) ON DELETE CASCADE ON UPDATE CASCADE
);
GO
