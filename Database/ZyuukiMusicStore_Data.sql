
USE ZyuukiMusicStore;
GO

-- Bảng VaiTro
INSERT INTO VaiTro (ma_vt, tenvt, mota) VALUES
('VT01', N'Quản lý', N'Quyền cao nhất, quản trị toàn hệ thống'),
('VT02', N'Nhân viên', N'Quản lý sản phẩm và đơn hàng'),
('VT03', N'Khách hàng', N'Mua hàng, đánh giá sản phẩm');
GO

-- Bảng TaiKhoan
INSERT INTO TaiKhoan (sdt, matkhau, ma_vt, email, hinhanh) VALUES
('0912345678', '123456', 'VT01', 'admin@zyuuki.vn', NULL),
('0912000111', '123456', 'VT02', 'staff1@zyuuki.vn', NULL),
('0912000222', '123456', 'VT02', 'staff2@zyuuki.vn', NULL),
('0988000333', '123456', 'VT03', 'customer1@zyuuki.vn', NULL),
('0988000444', '123456', 'VT03', 'customer2@zyuuki.vn', NULL);
GO

-- Bảng KhachHang
INSERT INTO KhachHang (ma_kh, tenkh, diachi) VALUES
(4, N'Lê Minh Hoàng', N'Hà Nội'),
(5, N'Nguyễn Thu Trang', N'TP. Hồ Chí Minh');
GO

-- Bảng NhanVien
INSERT INTO NhanVien (ma_nv, tennv, phai, cccd, ngaycap, noicap) VALUES
(2, N'Trần Văn Nam', 1, '012345678901', '2020-05-12', N'Hà Nội'),
(3, N'Phạm Thị Linh', 0, '098765432109', '2021-03-25', N'Hồ Chí Minh');
GO

-- Bảng QuanLy
INSERT INTO QuanLy (ma_ql, tenql) VALUES
(1, N'Nguyễn Quang Huy');
GO

-- Bảng LoaiSanPham
INSERT INTO LoaiSanPham (ma_loai, tenloai, mota) VALUES
('L01', N'Guitar', N'Các loại đàn guitar'),
('L02', N'Piano', N'Đàn piano điện và cơ'),
('L03', N'Sáo', N'Sáo trúc, sáo mèo và các loại sáo khác'),
('L04', N'Trống', N'Trống điện tử, trống jazz'),
('L05', N'Phụ kiện', N'Dây đàn, bao đàn, chân đàn...');
GO

-- Bảng NhaSanXuat
INSERT INTO NhaSanXuat (ma_nsx, tennsx, diachi, sdt, email) VALUES
('NSX01', N'Yamaha', N'Nhật Bản', '0845123456', 'contact@yamaha.jp'),
('NSX02', N'Casio', N'Nhật Bản', '0811122233', 'support@casio.jp'),
('NSX03', N'Fender', N'Mỹ', '0800345678', 'info@fender.com'),
('NSX04', N'MeiLan', N'Trung Quốc', '0869988776', 'info@meilan.cn'),
('NSX05', N'Vic Firth', N'Mỹ', '0899123456', 'contact@vicfirth.com');
GO

-- Bảng SanPham
INSERT INTO SanPham (ma_sp, tensp, ma_nsx, ma_loai, giasp, mota) VALUES
('SP01', N'Guitar Classic C40', 'NSX01', 'L01', 2500000,N'Guitar gỗ phù hợp cho người mới học'),
('SP02', N'Piano Điện PX-S1000', 'NSX02', 'L02', 18000000, N'Dòng piano điện cao cấp của Casio'),
('SP03', N'Sáo trúc Việt', 'NSX04', 'L03', 300000, N'Sáo trúc truyền thống âm thanh ấm áp'),
('SP04', N'Trống Jazz Set', 'NSX03', 'L04', 12500000,N'Bộ trống dành cho biểu diễn sân khấu'),
('SP05', N'Dây đàn DAddario', 'NSX03', 'L05', 120000, N'Dây đàn thay thế chất lượng cao'),
('SP06', N'Guitar Điện Strat', 'NSX03', 'L01', 15000000, N'Guitar điện Fender nổi tiếng'), -- NSX03 (Fender) làm cả L01 (Guitar) và L04, L05
('SP07', N'Piano Cơ U1', 'NSX01', 'L02', 120000000, N'Piano cơ Yamaha cao cấp'), -- NSX01 (Yamaha) làm cả L01 (Guitar) và L02 (Piano)
('SP08', N'Bao Đàn Guitar Dày', 'NSX01', 'L05', 450000, N'Bao đàn chất lượng cao, chống sốc'), -- NSX01 (Yamaha) làm cả L05 (Phụ kiện)
('SP09', N'Trống Điện DTX', 'NSX01', 'L04', 19000000, N'Bộ trống điện tử Yamaha'), -- NSX01 (Yamaha) làm cả L04 (Trống)
('SP10', N'Dùi Trống 5A', 'NSX05', 'L05', 250000, N'Dùi trống Vic Firth phổ thông'); -- NSX05 (Vic Firth) chỉ làm L05 (Phụ kiện)
GO

-- Bảng Hinh
INSERT INTO Hinh (ma_sp, url) VALUES
('SP01','GuitarClassicC40.png'),
('SP02','PianoDienPX-S1000.png'),
('SP03','SaotrucViet.png'),
('SP04','TrongJazzSet.png'),
('SP05','DaydanDAddario.png'),
('SP06','GuitarDienStrat.png'),
('SP07','PianoCoU1.png'),
('SP08','BaoDanGuitarDay.png'),
('SP09','TrongDienDTX.png'),
('SP10','DuiTrong5A.png');
GO

-- Bảng KhoHang
INSERT INTO KhoHang (ma_sp, soluongton) VALUES
('SP01', 20),
('SP02', 5),
('SP03', 50),
('SP04', 3),
('SP05', 100),
('SP06', 15),
('SP07', 2),
('SP08', 40),
('SP09', 7),
('SP10', 80);
GO

-- Bảng MaGiamGia
INSERT INTO MaGiamGia (ma_giamgia, giatri, ngaybatdau, ngayketthuc) VALUES
('SALE10', 10, '2025-01-01', '2025-12-31'),
('NEWUSER', 5, '2025-06-01', '2025-12-31');
GO

-- Bảng DonDatHang
INSERT INTO DonDatHang (ma_giamgia, ma_kh, tongtien, tt_thanhtoan) VALUES
('SALE10',4, 4500000, N'Đã thanh toán'),
('NEWUSER',5, 17100000, N'Chưa thanh toán');
GO

-- Bảng ChiTietDonDatHang
INSERT INTO ChiTietDonDatHang (ma_ddh, ma_sp, soluong, gia, chietkhau, thanhtien) VALUES
(1, 'SP01', 2, 2500000, 10, 4500000),
(2, 'SP02', 1, 18000000, 5, 17100000);
GO

-- Bảng DanhGia
INSERT INTO DanhGia (ma_kh, ma_sp, noidung, sosao) VALUES
(4, 'SP01', N'Âm thanh rất hay, dễ chơi', 5),
(5, 'SP02', N'Đàn tốt, chất lượng cao nhưng hơi nặng', 4),
(4, 'SP03', N'Giá rẻ, dễ thổi cho người mới học', 5);
GO
