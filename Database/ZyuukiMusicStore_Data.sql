USE ZyuukiMusicStore; -- Sử dụng đúng tên DB đã tạo ở bước trước
GO

-- =============================================
-- 1. Bảng VaiTro (Giữ nguyên)
-- =============================================
INSERT INTO VaiTro (ma_vt, tenvt, mota) VALUES
('VT01', N'Quản lý', N'Quyền cao nhất, quản trị toàn hệ thống'),
('VT02', N'Nhân viên', N'Quản lý sản phẩm và đơn hàng');
-- VT03 (Khách hàng) không còn cần thiết trong bảng này theo ERD mới, 
-- vì bảng NguoiDung (khách hàng) không còn liên kết với VaiTro.
-- Tuy nhiên, nếu bạn muốn giữ lại để tham khảo thì cứ để. 
-- Ở đây mình xóa VT03 để đúng chuẩn ERD.
-- ('VT03', N'Khách hàng', N'Mua hàng, đánh giá sản phẩm'); 
GO

-- =============================================
-- 2. Bảng NhanVien (Dữ liệu mới được tách ra)
-- =============================================
-- Lưu ý: Đã bổ sung CCCD giả định cho Admin vì cột này bắt buộc (NOT NULL) trong schema mới.
INSERT INTO NhanVien (ma_nv,tennv, matkhau, sdt, email, cccd, diachi, hinh, ma_vt) VALUES
('Admin',N'Võ Chung Khánh Đăng', '123456', '0912345678', 'admin@zyuuki.vn', '000000000001', N'Cần Thơ', NULL, 'VT01'),
('NV_01',N'Trần Văn Nam', '123456', '0912000111', 'staff1@zyuuki.vn', '012345678901', N'Hà Nội', NULL, 'VT02'),
('NV_02',N'Phạm Thị Linh', '123456', '0912000222', 'staff2@zyuuki.vn', '098765432109', N'TP. Hồ Chí Minh', NULL, 'VT02');
-- Kết quả IDENTITY dự kiến: 
-- ID 1: Võ Chung Khánh Đăng
-- ID 2: Trần Văn Nam
-- ID 3: Phạm Thị Linh
GO

-- =============================================
-- 3. Bảng NguoiDung (Khách hàng - Dữ liệu mới được tách ra)
-- =============================================
INSERT INTO NguoiDung (tennd, matkhau, sdt, diachi, email, hinh) VALUES
(N'Lê Minh Hoàng', '123456', '0988000333', N'Hà Nội', 'customer1@zyuuki.vn', NULL),
(N'Nguyễn Thu Trang', '123456', '0988000444', N'TP. Hồ Chí Minh', 'customer2@zyuuki.vn', NULL);
-- Kết quả IDENTITY dự kiến: 
-- ID 1: Lê Minh Hoàng (ID cũ là 4)
-- ID 2: Nguyễn Thu Trang (ID cũ là 5)
GO

-- =============================================
-- 4. Bảng LoaiSanPham (Giữ nguyên)
-- =============================================
INSERT INTO LoaiSanPham (ma_loai, tenloai, mota) VALUES
('L01', N'Guitar', N'Các loại đàn guitar'),
('L02', N'Piano', N'Đàn piano điện và cơ'),
('L03', N'Sáo', N'Sáo trúc, sáo mèo và các loại sáo khác'),
('L04', N'Trống', N'Trống điện tử, trống jazz'),
('L05', N'Phụ kiện', N'Dây đàn, bao đàn, chân đàn...');
GO

-- =============================================
-- 5. Bảng NhaSanXuat (Giữ nguyên)
-- =============================================
INSERT INTO NhaSanXuat (ma_nsx, tennsx, diachi, sdt, email) VALUES
('NSX01', N'Yamaha', N'Nhật Bản', '0845123456', 'contact@yamaha.jp'),
('NSX02', N'Casio', N'Nhật Bản', '0811122233', 'support@casio.jp'),
('NSX03', N'Fender', N'Mỹ', '0800345678', 'info@fender.com'),
('NSX04', N'MeiLan', N'Trung Quốc', '0869988776', 'info@meilan.cn'),
('NSX05', N'Vic Firth', N'Mỹ', '0899123456', 'contact@vicfirth.com');
GO

-- =============================================
-- 6. Bảng SanPham (Gộp dữ liệu từ KhoHang cũ vào cột soluongton)
-- =============================================
INSERT INTO SanPham (ma_sp, tensp, ma_nsx, ma_loai, giasp, soluongton, mota) VALUES
('SP01', N'Guitar Classic C40', 'NSX01', 'L01', 2500000, 20, N'Guitar gỗ phù hợp cho người mới học'),
('SP02', N'Piano Điện PX-S1000', 'NSX02', 'L02', 18000000, 5, N'Dòng piano điện cao cấp của Casio'),
('SP03', N'Sáo trúc Việt', 'NSX04', 'L03', 300000, 50, N'Sáo trúc truyền thống âm thanh ấm áp'),
('SP04', N'Trống Jazz Set', 'NSX03', 'L04', 12500000, 3, N'Bộ trống dành cho biểu diễn sân khấu'),
('SP05', N'Dây đàn DAddario', 'NSX03', 'L05', 120000, 100, N'Dây đàn thay thế chất lượng cao'),
('SP06', N'Guitar Điện Strat', 'NSX03', 'L01', 15000000, 15, N'Guitar điện Fender nổi tiếng'),
('SP07', N'Piano Cơ U1', 'NSX01', 'L02', 120000000, 2, N'Piano cơ Yamaha cao cấp'),
('SP08', N'Bao Đàn Guitar Dày', 'NSX01', 'L05', 450000, 40, N'Bao đàn chất lượng cao, chống sốc'),
('SP09', N'Trống Điện DTX', 'NSX01', 'L04', 19000000, 7, N'Bộ trống điện tử Yamaha'),
('SP10', N'Dùi Trống 5A', 'NSX05', 'L05', 250000, 80, N'Dùi trống Vic Firth phổ thông'),
('SP11', 'Guitar Acoustic FG800M', 'NSX01', 'L01', 5800000, 0, 'Guitar Acoustic tầm trung của Yamaha, âm thanh cân bằng, mặt gỗ Mahogany.'),
('SP12', 'Đàn Ukulele Soprano', 'NSX04', 'L01', 650000, 0, 'Ukulele gỗ tự nhiên, size Soprano, âm thanh vui tươi, dễ học.'),
('SP13', 'Keyboard CT-S300', 'NSX02', 'L02', 4200000, 0, 'Keyboard điện tử Casio, 61 phím cảm ứng lực, phù hợp cho người mới.'),
('SP14', 'Metronome cơ học', 'NSX05', 'L05', 750000, 0, 'Máy đập nhịp cơ học cổ điển, hỗ trợ luyện tập tiết tấu chính xác.'),
('SP15', 'Harmonica Diatonic', 'NSX01', 'L03', 400000, 0, 'Kèn Harmonica 10 lỗ Diatonic, tone C, dễ sử dụng.'),
('SP16', 'Amplifier Guitar 10W', 'NSX03', 'L05', 2800000, 0, 'Amply nhỏ gọn Fender cho guitar điện, công suất 10W, có hiệu ứng Distortion.'),
('SP17', 'Piano Điện CDP-S150', 'NSX02', 'L02', 14500000, 0, 'Dòng piano điện mỏng nhẹ của Casio, 88 phím có độ nặng.'),
('SP18', 'Sáo Recorder Baroque', 'NSX04', 'L03', 180000, 0, 'Sáo nhựa Recorder hệ thống Baroque, thích hợp cho giáo dục âm nhạc.'),
('SP19', 'Trống Cajun box', 'NSX03', 'L04', 3500000, 0, 'Trống Cajon làm bằng gỗ bạch dương, âm trầm và âm snare rõ ràng.'),
('SP20', 'Dây Micro Canon', 'NSX05', 'L05', 300000, 0, 'Dây cáp micro XLR dài 3m, chất lượng truyền tín hiệu tốt.'),
('SP21', 'Guitar Acoustic F310', 'NSX01', 'L01', 3200000, 0, 'Mẫu đàn Acoustic phổ biến, âm thanh vang, rất được ưa chuộng.'),
('SP22', 'Piano Điện P-125', 'NSX01', 'L02', 19500000, 0, 'Piano điện Yamaha P-Series, âm thanh Pure CF, gọn và mạnh mẽ.'),
('SP23', 'Trống Lắc Tambourine', 'NSX04', 'L05', 150000, 0, 'Nhạc cụ gõ Tambourine, vỏ nhựa, chuông kim loại, âm thanh sáng.'),
('SP24', 'Bộ Dây Đàn Piano', 'NSX01', 'L05', 1200000, 0, 'Bộ dây đàn piano cơ thay thế, chất liệu thép cao cấp.'),
('SP25', 'Sáo Flute Bạc', 'NSX01', 'L03', 8500000, 0, 'Sáo Flute tiêu chuẩn, thân mạ bạc, âm thanh trong trẻo, chuyên nghiệp.'),
('SP26', 'Giá Đỡ Nhạc Đa Năng', 'NSX05', 'L05', 550000, 0, 'Chân đỡ nhạc bằng thép, có thể điều chỉnh độ cao, gấp gọn.'),
('SP27', 'Guitar Điện Telecaster', 'NSX03', 'L01', 17000000, 0, 'Guitar điện Fender Telecaster, âm thanh twang đặc trưng, thiết kế cổ điển.'),
('SP28', 'Piano Cơ B1', 'NSX01', 'L02', 80000000, 0, 'Piano cơ Yamaha B-series, nhỏ gọn, phù hợp cho căn hộ.'),
('SP29', 'Pad Luyện Tập Trống', 'NSX05', 'L05', 600000, 0, 'Bề mặt cao su, giúp luyện tập trống yên lặng và tăng cường độ nảy.'),
('SP30', 'Trống Đồng Latin', 'NSX04', 'L04', 4800000, 0, 'Bộ Trống Conga/Bongo kiểu Latin, âm thanh vang, phù hợp cho nhạc Latin Jazz.');
GO

-- =============================================
-- 7. Bảng Hinh (Đổi tên cột url thành tenhinh)
-- =============================================
INSERT INTO Hinh (ma_sp, tenhinh) VALUES
('SP01','SP1_12112025.jpg'),
('SP01','GuitarClassicC40.png'),
('SP01','SP1_13112025.jpg'),
('SP02','PianoDienPX-S1000.png'),
('SP03','SaotrucViet.png'),
('SP04','TrongJazzSet.png'),
('SP05','DaydanDAddario.png'),
('SP06','GuitarDienStrat.png');
GO

-- =============================================
-- 8. Bảng DonDatHang (Cập nhật ma_nd mới và thêm ma_nv)
-- =============================================
-- Mapping ID cũ -> mới: ID 4 cũ -> ID 1 mới; ID 5 cũ -> ID 2 mới.
-- Giả định nhân viên có ID 2 (Trần Văn Nam) xử lý các đơn hàng đã hoàn thành.
INSERT INTO DonDatHang (ma_nd, ma_nv, diachi, ngaydat, tongtien, trangthai, tt_thanhtoan) VALUES
(1, 'NV_02', N'Hà Nội','2025-11-12', 4500000, N'Hoàn thành', N'Đã thanh toán'), -- Cũ là nd=4
(2, 'NV_01', N'TP. Hồ Chí Minh','2025-11-6', 17100000, N'Đang xử lý', N'Chưa thanh toán'), -- Cũ là nd=5
(1, 'NV_02', N'Hà Nội', '2025-09-05', 5250000, N'Hoàn thành', N'Đã thanh toán'), -- Cũ là nd=4
(2, 'NV_02', N'TP. Hồ Chí Minh', '2025-09-20', 120000000, N'Đã hủy', N'Chưa thanh toán'), -- Cũ là nd=5
(1, 'NV_02', N'Đà Nẵng', '2025-10-10', 12500000, N'Hoàn thành', N'Đã thanh toán'), -- Cũ là nd=4
(2, 'NV_01', N'Hà Nội', '2025-10-28', 15000000, N'Đang xử lý', N'Chưa thanh toán'), -- Cũ là nd=5
(1, 'NV_02', N'Cần Thơ', '2025-11-01', 19000000, N'Hoàn thành', N'Đã thanh toán'), -- Cũ là nd=4
(2, 'NV_01', N'Hải Phòng', '2025-11-25', 18000000, N'Đang xử lý', N'Đã thanh toán'); -- Cũ là nd=5
GO

-- =============================================
-- 9. Bảng ChiTietDonDatHang (Giữ nguyên, dựa trên giả định ma_ddh không đổi)
-- =============================================
INSERT INTO ChiTietDonDatHang (ma_ddh, ma_sp, soluong, gia, thanhtien) VALUES
(1, 'SP01', 2, 2500000, 4500000),
(2, 'SP02', 1, 18000000, 17100000),
(3, 'SP01', 1, 2500000, 2500000),
(3, 'SP03', 9, 300000, 2700000), 
(3, 'SP05', 4, 120000, 480000),
(3, 'SP08', 3, 450000, 1350000),
(4, 'SP07', 1, 120000000, 120000000),
(5, 'SP04', 1, 12500000, 12500000),
(6, 'SP06', 1, 15000000, 15000000),
(7, 'SP09', 1, 19000000, 19000000),
(8, 'SP02', 1, 18000000, 18000000);
GO

-- =============================================
-- 10. Bảng DanhGia (Cập nhật ma_nd mới)
-- =============================================
-- Mapping ID cũ -> mới: ID 4 cũ -> ID 1 mới; ID 5 cũ -> ID 2 mới.
INSERT INTO DanhGia (ma_nd, ma_sp, noidung, sosao) VALUES
(1, 'SP01', N'Âm thanh rất hay, dễ chơi', 5), -- Cũ là nd=4
(1, 'SP02', N'Cảm giác chất âm không hợp, khó chơi', 3), -- Cũ là nd=4
(2, 'SP02', N'Đàn tốt, chất lượng cao nhưng hơi nặng', 4), -- Cũ là nd=5
(1, 'SP03', N'Giá rẻ, dễ thổi cho người mới học', 5); -- Cũ là nd=4
GO

-- =============================================
-- 11. Bảng CapNhat (Bảng mới - Thêm dữ liệu mẫu)
-- =============================================
-- Giả định nhân viên ID 1 và 2 thực hiện cập nhật sản phẩm
INSERT INTO CapNhat (ma_nv, ma_sp, ngaycapnhat) VALUES
('NV_01', 'SP01', '2025-11-01 09:00:00'),
('NV_01', 'SP01', '2025-11-05 14:30:00'),
('NV_02', 'SP03', '2025-11-02 10:15:00'),
('NV_02', 'SP05', '2025-11-10 16:45:00');
GO