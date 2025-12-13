USE ZyuukiMusicStore; 
GO


INSERT INTO VaiTro (ma_vt, tenvt, mota) VALUES
('Admin', N'Quản lý', N'Quyền cao nhất, quản trị toàn hệ thống'),
('Staff', N'Nhân viên', N'Quản lý sản phẩm và đơn hàng'),
('Carrier', N'Giao hàng', N'Chịu trách nhiệm vận chuyển hàng hóa đến khách hàng');

GO


INSERT INTO NhanVien (ma_nv,tennv, matkhau, phai, sdt, email, cccd, diachi, hinh, ma_vt,trangthai) VALUES
('QL_01',N'Võ Chung Khánh Đăng', '123456', 0, '0952465691', 'admin@zyuuki.vn', '060299002142', N'789 Đ. Phan Huy Ích, Phường Tân Sơn, Thành phố Hồ Chí Minh', NULL, 'Admin',1),
('NV_01',N'Trần Văn Nam', '123456',0, '0924724109', 'staff1@zyuuki.vn', '060298047323', N'68/22 Đ. An Dương Vương, Phường Phú Định, Thành phố Hồ Chí Minh', NULL, 'Staff',1),
('NV_02',N'Phạm Thị Linh', '123456',1, '0987652109', 'staff2@zyuuki.vn', '060396050442', N'45 Đ. Nguyễn Văn Linh, Phường Tân Thuận Tây, Thành phố Hồ Chí Minh', NULL, 'Staff',1),
('SP_01',N'Hoàng Quốc Khánh', '123456',0, '0912536844', 'carrier1@zyuuki.vn', '060201060212', N'33 Đ. Cao Lỗ, Phường Chánh Hưng, Thành phố Hồ Chí Minh', NULL, 'Carrier',1),
('SP_02',N'Trần Văn Huy', '123456',0, '0283611933', 'carrier2@zyuuki.vn', '060203002432', N'50 Đ. Lạc Long Quân, Phường Hòa Bình, Thành phố Hồ Chí Minh', NULL, 'Carrier',1),
('SP_03',N'Lê Thanh Minh', '123456',0, '0877927512', 'carrier3@zyuuki.vn', '060202067195', N'12 Đ. Nơ Trang Long, Phường Bình Lợi Trung, Thành phố Hồ Chí Minh', NULL, 'Carrier',1);
GO

INSERT INTO NguoiDung (tennd, matkhau, sdt, diachi, phuongxa, tinhthanh, email, hinh, trangthai) VALUES
(N'Lê Minh Hoàng', '123456', '0988252751', N'56 Cách Mạng Tháng 8',N'Phường Xuân Hòa',N'Thành phố Hồ Chí Minh', 'customer1@zyuuki.vn', NULL,1),
(N'Nguyễn Thu Trang', '123456', '0985263321', N'129 Nguyễn Đình Chiểu',N'Phường Xuân Hòa',N'Thành phố Hồ Chí Minh', 'customer2@zyuuki.vn', NULL,1),
(N'Trần Đức Hiếu', '123456', '0982422774', N'22 Pasteur',N'Phường Bến Thành',N'Thành phố Hồ Chí Minh', 'customer3@zyuuki.vn', NULL,1),
(N'Ngô Hoàng Khang', '123456', '0932635865', N'233 Lê Văn Sỹ',N'Phường Phú Nhuận',N'Thành phố Hồ Chí Minh', 'customer4@zyuuki.vn', NULL,1),
(N'Nguyễn Văn An', '123456', '0927321176', N'25 Đ. Điện Biên Phủ',N'Phường Thạnh Mỹ Tây',N'Thành phố Hồ Chí Minh', 'customer5@zyuuki.vn', NULL,1);
GO

INSERT INTO LoaiSanPham (ma_loai, tenloai, mota) VALUES
('guitar', N'Guitar', N'Các loại đàn guitar'),
('piano', N'Piano', N'Đàn piano điện và cơ'),
('flute', N'Sáo', N'Sáo trúc, sáo mèo và các loại sáo khác'),
('drum', N'Trống', N'Trống điện tử, trống jazz'),
('accessory', N'Phụ kiện', N'Dây đàn, bao đàn, chân đàn...');
GO

INSERT INTO NhaSanXuat (ma_nsx, tennsx, diachi, sdt, email) VALUES
('yamaha', N'Yamaha', N'10-1 Nakazawa-cho, Hamamatsu, Shizuoka, Nhật Bản', '+81 53-123-4567', 'contact@yamaha.jp'),
('casio', N'Casio', N'6-2 Hon-machi, Shibuya-ku, Tokyo, Nhật Bản', '+81 3-5566-7788', 'support@casio.jp'),
('fender', N'Fender', N'17600 North Perimeter Drive, Scottsdale, Arizona, Hoa Kỳ', '+1 480-555-2376', 'info@fender.com'),
('meilan', N'MeiLan', N'88 Xinjian Road, Longhua District, Shenzhen, Trung Quốc', '+86 755-8899-1122', 'info@meilan.cn'),
('vicfirth', N'Vic Firth', N'147 Norwood Street, Boston, Massachusetts, Hoa Kỳ', '+1 617-555-9044', 'contact@vicfirth.com');
GO

INSERT INTO SanPham (ma_sp, tensp, ma_nsx, ma_loai, giasp, soluongton, mota) VALUES
('SP01', N'Guitar Classic C40', 'yamaha', 'guitar', 2500000, 20, N'Guitar gỗ phù hợp cho người mới học'),
('SP02', N'Piano Điện PX-S1000', 'casio', 'piano', 18000000, 5, N'Dòng piano điện cao cấp của Casio'),
('SP03', N'Sáo trúc Việt', 'meilan', 'flute', 300000, 50, N'Sáo trúc truyền thống âm thanh ấm áp'),
('SP04', N'Trống Jazz Set', 'fender', 'drum', 12500000, 3, N'Bộ trống dành cho biểu diễn sân khấu'),
('SP05', N'Dây đàn DAddario', 'fender', 'accessory', 120000, 100, N'Dây đàn thay thế chất lượng cao'),
('SP06', N'Guitar Điện Strat', 'fender', 'guitar', 15000000, 15, N'Guitar điện Fender nổi tiếng'),
('SP07', N'Piano Cơ U1', 'yamaha', 'piano', 120000000, 2, N'Piano cơ Yamaha cao cấp'),
('SP08', N'Bao Đàn Guitar Dày', 'yamaha', 'accessory', 450000, 40, N'Bao đàn chất lượng cao, chống sốc'),
('SP09', N'Trống Điện DTX', 'yamaha', 'drum', 19000000, 7, N'Bộ trống điện tử Yamaha'),
('SP10', N'Dùi Trống 5A', 'vicfirth', 'accessory', 250000, 0, N'Dùi trống Vic Firth phổ thông'),
('SP11', N'Guitar Acoustic FG800M', 'yamaha', 'guitar', 5800000, 10, N'Guitar Acoustic tầm trung của Yamaha, âm thanh cân bằng, mặt gỗ Mahogany.'),
('SP12', N'Đàn Ukulele Soprano', 'meilan', 'guitar', 650000, 23, N'Ukulele gỗ tự nhiên, size Soprano, âm thanh vui tươi, dễ học.'),
('SP13', N'Keyboard CT-S300', 'casio', 'piano', 4200000, 45, N'Keyboard điện tử Casio, 61 phím cảm ứng lực, phù hợp cho người mới.'),
('SP14', N'Metronome cơ học', 'vicfirth', 'accessory', 750000, 65, N'Máy đập nhịp cơ học cổ điển, hỗ trợ luyện tập tiết tấu chính xác.'),
('SP15', N'Harmonica Diatonic', 'yamaha', 'flute', 400000, 23, N'Kèn Harmonica 10 lỗ Diatonic, tone C, dễ sử dụng.'),
('SP16', N'Amplifier Guitar 10W', 'fender', 'accessory', 2800000, 22, N'Amply nhỏ gọn Fender cho guitar điện, công suất 10W, có hiệu ứng Distortion.'),
('SP17', N'Piano Điện CDP-S150', 'casio', 'piano', 14500000, 11, N'Dòng piano điện mỏng nhẹ của Casio, 88 phím có độ nặng.'),
('SP18', N'Sáo Recorder Baroque', 'meilan', 'flute', 180000, 53, N'Sáo nhựa Recorder hệ thống Baroque, thích hợp cho giáo dục âm nhạc.'),
('SP19', N'Trống Cajun box', 'fender', 'drum', 3500000, 15, N'Trống Cajon làm bằng gỗ bạch dương, âm trầm và âm snare rõ ràng.'),
('SP20', N'Dây Micro Canon', 'vicfirth', 'accessory', 300000, 53, N'Dây cáp micro XLR dài 3m, chất lượng truyền tín hiệu tốt.'),
('SP21', N'Guitar Acoustic F310', 'yamaha', 'guitar', 3200000, 22, N'Mẫu đàn Acoustic phổ biến, âm thanh vang, rất được ưa chuộng.'),
('SP22', N'Piano Điện P-125', 'yamaha', 'piano', 19500000, 1, N'Piano điện Yamaha P-Series, âm thanh Pure CF, gọn và mạnh mẽ.'),
('SP23', N'Trống Lắc Tambourine', 'meilan', 'accessory', 150000, 3, N'Nhạc cụ gõ Tambourine, vỏ nhựa, chuông kim loại, âm thanh sáng.'),
('SP24', N'Bộ Dây Đàn Piano', 'yamaha', 'accessory', 1200000, 6, N'Bộ dây đàn piano cơ thay thế, chất liệu thép cao cấp.'),
('SP25', N'Sáo Flute Bạc', 'yamaha', 'flute', 8500000, 7, N'Sáo Flute tiêu chuẩn, thân mạ bạc, âm thanh trong trẻo, chuyên nghiệp.'),
('SP26', N'Giá Đỡ Nhạc Đa Năng', 'vicfirth', 'accessory', 550000, 0, N'Chân đỡ nhạc bằng thép, có thể điều chỉnh độ cao, gấp gọn.'),
('SP27', N'Guitar Điện Telecaster', 'fender', 'guitar', 17000000, 34, N'Guitar điện Fender Telecaster, âm thanh twang đặc trưng, thiết kế cổ điển.'),
('SP28', N'Piano Cơ B1', 'yamaha', 'piano', 80000000, 38, N'Piano cơ Yamaha B-series, nhỏ gọn, phù hợp cho căn hộ.'),
('SP29', N'Pad Luyện Tập Trống', 'vicfirth', 'accessory', 600000, 65, N'Bề mặt cao su, giúp luyện tập trống yên lặng và tăng cường độ nảy.'),
('SP30', N'Trống Đồng Latin', 'meilan', 'drum', 4800000, 16, N'Bộ Trống Conga/Bongo kiểu Latin, âm thanh vang, phù hợp cho nhạc Latin Jazz.');
GO


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

INSERT INTO DonDatHang (ma_nd, ma_nv,nguoinhan,sdt, diachi, phuongxa, tinhthanh, ngaydat, tongtien, trangthai, tt_thanhtoan, phuongthuc) VALUES
(1, 'NV_02',N'Lê Minh Hoàng', '0988252751', N'56 Cách Mạng Tháng 8',N'Phường Xuân Hòa',N'Thành phố Hồ Chí Minh','2025-11-12', 4500000, N'Hoàn thành', N'Đã thanh toán', 'VNPay'),
(2, 'NV_01',N'Nguyễn Thu Trang', '0985263321', N'129 Nguyễn Đình Chiểu',N'Phường Xuân Hòa',N'Thành phố Hồ Chí Minh', '2025-11-6', 17100000, N'Đang xử lý', N'Chưa thanh toán', 'COD'), 
(1, 'NV_02',N'Lê Minh Hoàng', '0988252751', N'56 Cách Mạng Tháng 8',N'Phường Xuân Hòa',N'Thành phố Hồ Chí Minh', '2025-09-05', 5250000, N'Hoàn thành', N'Đã thanh toán', 'VNPay'), 
(2, 'NV_02',N'Nguyễn Thu Trang', '0985263321', N'129 Nguyễn Đình Chiểu',N'Phường Xuân Hòa',N'Thành phố Hồ Chí Minh','2025-09-20', 120000000, N'Đã hủy', N'Chưa thanh toán', 'COD'), 
(1, 'NV_02',N'Lê Minh Hoàng', '0988252751', N'56 Cách Mạng Tháng 8',N'Phường Xuân Hòa',N'Thành phố Hồ Chí Minh', '2025-10-10', 12500000, N'Hoàn thành', N'Đã thanh toán', 'COD'), 
(2, 'NV_01',N'Nguyễn Thu Trang', '0985263321', N'129 Nguyễn Đình Chiểu',N'Phường Xuân Hòa',N'Thành phố Hồ Chí Minh', '2025-10-28', 15000000, N'Đang xử lý', N'Chưa thanh toán', 'COD'), 
(1, 'NV_02',N'Lê Minh Hoàng', '0988252751', N'56 Cách Mạng Tháng 8',N'Phường Xuân Hòa',N'Thành phố Hồ Chí Minh', '2025-11-01', 19000000, N'Hoàn thành', N'Đã thanh toán', 'COD'),
(2, 'NV_01',N'Nguyễn Thu Trang', '0985263321', N'129 Nguyễn Đình Chiểu',N'Phường Xuân Hòa',N'Thành phố Hồ Chí Minh', '2025-11-25', 18000000, N'Đang xử lý', N'Đã thanh toán', 'VNPay');
GO

INSERT INTO GiaoHang(ma_ddh, ma_nv, ngaybd, ngaykt, tongthu, trangthai) VALUES
(1,'SP_01','2025-11-14','2025-11-19',0,N'Đã giao'),
(3,'SP_02','2025-09-05','2025-09-11',0,N'Đã giao'),
(5,'SP_02','2025-10-10','2025-10-20',12500000,N'Đã giao'),
(7,'SP_03','2025-11-01','2025-11-8',19000000,N'Đã giao');

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

INSERT INTO DanhGia (ma_nd, ma_sp, noidung, sosao) VALUES
(1, 'SP01', N'Âm thanh rất hay, dễ chơi', 5), 
(1, 'SP02', N'Cảm giác chất âm không hợp, khó chơi', 3), 
(2, 'SP02', N'Đàn tốt, chất lượng cao nhưng hơi nặng', 4), 
(1, 'SP03', N'Giá rẻ, dễ thổi cho người mới học', 5); 
GO

INSERT INTO CapNhat (ma_nv, ma_sp, ngaycapnhat) VALUES
('NV_01', 'SP01', '2025-11-01 09:00:00'),
('NV_01', 'SP01', '2025-11-05 14:30:00'),
('NV_02', 'SP03', '2025-11-02 10:15:00'),
('NV_02', 'SP05', '2025-11-10 16:45:00');
GO

INSERT INTO GiamGia (ma_gg, tenma, loaima, dieukien, ngaybd, ngaykt, phantramgiam) VALUES
('WELCOME25', N'Chào bạn mới 2025', N'Voucher', 50000, '2025-01-01', '2025-12-31', 5), 
('BLACKFRI', N'Săn sale Black Friday', N'Voucher', 2000000, '2025-11-20', '2025-11-30', 15),  
('VIPMEMBER', N'Tri ân khách hàng VIP', N'Voucher', 500000, '2025-01-01', '2026-01-01', 25), 
('FREESHIP', N'Miễn phí vận chuyển', N'Freeship', 30000, '2025-06-01', '2025-12-31', 50),              
('XMAS2025', N'Giáng sinh an lành', N'Voucher', 500000, '2025-12-01', '2025-12-25', 10),         
('ACCESSORY', N'Giảm giá phụ kiện', N'Voucher', 100000, '2025-09-01', '2025-12-31', 20);   
GO

INSERT INTO ChiTietGiamGia (ma_nd, ma_gg, soluong) VALUES
(1, 'WELCOME25', 0),  
(1, 'VIPMEMBER', 2),  
(1, 'BLACKFRI', 1),   
(1, 'FREESHIP', 5),   
(2, 'VIPMEMBER', 3), 
(2, 'XMAS2025', 1),  
(3, 'WELCOME25', 1),  
(3, 'ACCESSORY', 2), 
(4, 'WELCOME25', 1),
(4, 'FREESHIP', 2),
(5, 'WELCOME25', 1),
(5, 'BLACKFRI', 1);
GO