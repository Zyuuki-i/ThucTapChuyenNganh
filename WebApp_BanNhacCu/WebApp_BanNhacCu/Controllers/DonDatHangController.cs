using Microsoft.AspNetCore.Mvc;
using WebApp_BanNhacCu.Models;
using WebApp_BanNhacCu.Payments;

namespace WebApp_BanNhacCu.Controllers
{
    public class DonDatHangController : Controller
    {
        ZyuukiMusicStoreContext db = new ZyuukiMusicStoreContext();

        public IActionResult Index()
        {
            DonDatHang ddh = MySession.Get<DonDatHang>(HttpContext.Session, "tempDdh");
            List<ChiTietDonDatHang> dsCT = null;
            if (ddh == null)
            {
                dsCT = new List<ChiTietDonDatHang>();
            }
            else
            {
                dsCT = ddh.ChiTietDonDatHangs.ToList();
                dsCT.ForEach(ct => ct.MaSpNavigation = db.SanPhams.FirstOrDefault(sp => sp.MaSp == ct.MaSp));
                List<Hinh> dsHinh = new List<Hinh>();
                foreach (ChiTietDonDatHang ct in dsCT)
                {
                    Hinh? hinh = db.Hinhs.Where(h => h.MaSp == ct.MaSp).FirstOrDefault();
                    if (hinh != null)
                    {
                        dsHinh.Add(hinh);
                    }
                }
                ViewBag.DsHinh = dsHinh;
            }
            return View(dsCT);
        }

        private DonDatHang donDatHang(ref bool flag, string id, int soluong)
        {
            SanPham? sp = db.SanPhams.Find(id);
            if (sp == null)
            {
                return null;
            }
            if (soluong <= 0) { soluong = 1; }
            DonDatHang ddh = MySession.Get<DonDatHang>(HttpContext.Session, "tempDdh");
            if (ddh == null)
            {
                ddh = new DonDatHang();
            }
            ChiTietDonDatHang ct = null;
            foreach (ChiTietDonDatHang a in ddh.ChiTietDonDatHangs.Where(t => t.MaSp == sp.MaSp))
            {
                ct = a; break;
            }
            if (ct == null)
            {
                if (sp.Soluongton < soluong) { flag = true; }
                else
                {
                    flag = false;
                    ct = new ChiTietDonDatHang();
                    ct.MaSp = sp.MaSp;
                    ct.Soluong = soluong;
                    ct.Gia = sp.Giasp;
                    ct.Thanhtien = soluong * sp.Giasp;
                    ddh.ChiTietDonDatHangs.Add(ct);
                }
            }
            else
            {
                foreach (ChiTietDonDatHang a in ddh.ChiTietDonDatHangs.Where(t => t.MaSp == sp.MaSp))
                {
                    if (sp.Soluongton < a.Soluong + soluong) { flag = true; }
                    else
                    {
                        flag = false;
                        a.Soluong += soluong;
                    }
                }
            }
            MySession.Set<DonDatHang>(HttpContext.Session, "tempDdh", ddh);
            return ddh;
        }

        public IActionResult themVaoGio(string id, int soluong)
        {
            bool flag = false;
            DonDatHang ddh = donDatHang(ref flag, id, soluong);
            if (ddh == null)
            {
                return RedirectToAction("Index", "SanPham");
            }
            if (flag)
            {
                TempData["MessageError_GioHang"] = "Số lượng sản phẩm trong kho không đủ để đáp ứng yêu cầu của bạn!";
            }
            else
            {
                TempData["MessageSuccess_GioHang"] = "Thêm sản phẩm vào giỏ hàng thành công!";
            }
            return Redirect(Request.Headers["Referer"].ToString());
        }


        public IActionResult xoaKhoiGio(string id)
        {
            DonDatHang ddh = MySession.Get<DonDatHang>(HttpContext.Session, "tempDdh");
            if (ddh != null)
            {
                ChiTietDonDatHang ct = null;
                foreach (ChiTietDonDatHang a in ddh.ChiTietDonDatHangs.Where(t => t.MaSp == id))
                {
                    ct = a; break;
                }
                if (ct != null)
                {
                    ddh.ChiTietDonDatHangs.Remove(ct);
                    MySession.Set<DonDatHang>(HttpContext.Session, "tempDdh", ddh);
                }
            }
            return RedirectToAction("Index");
        }

        public IActionResult muaNgay(string id, int soluong)
        {
            bool flag = false;
            DonDatHang ddh = donDatHang(ref flag, id, soluong);
            if (ddh == null)
            {
                return RedirectToAction("Index", "SanPham");
            }
            if (flag)
            {
                TempData["MessageError_MuaNgay"] = "Số lượng sản phẩm trong kho không đủ để đáp ứng yêu cầu của bạn!";
                return RedirectToAction("Index", "SanPham");
            }
            return RedirectToAction("Index");
        }

        public IActionResult tangSL(string id)
        {
            DonDatHang ddh = MySession.Get<DonDatHang>(HttpContext.Session, "tempDdh");
            if (ddh != null)
            {
                ChiTietDonDatHang ct = null;
                foreach (ChiTietDonDatHang a in ddh.ChiTietDonDatHangs.Where(t => t.MaSp == id))
                {
                    ct = a; break;
                }
                if (ct != null)
                {
                    SanPham? sp = db.SanPhams.FirstOrDefault(sp => sp.MaSp == id);
                    if (sp.Soluongton < ct.Soluong + 1)
                    {
                        TempData["MessageError_DonHang"] = "Số lượng sản phẩm trong kho không đủ để đáp ứng yêu cầu của bạn!";
                        return RedirectToAction("Index");
                    }
                    ct.Soluong += 1;
                    ct.Thanhtien = ct.Soluong * ct.Gia;
                    MySession.Set<DonDatHang>(HttpContext.Session, "tempDdh", ddh);
                }
            }
            return RedirectToAction("Index");
        }
        public IActionResult giamSL(string id)
        {
            DonDatHang ddh = MySession.Get<DonDatHang>(HttpContext.Session, "tempDdh");
            if (ddh != null)
            {
                ChiTietDonDatHang ct = null;
                foreach (ChiTietDonDatHang a in ddh.ChiTietDonDatHangs.Where(t => t.MaSp == id))
                {
                    ct = a; break;
                }
                if (ct != null)
                {
                    if (ct.Soluong <= 1)
                    {
                        ddh.ChiTietDonDatHangs.Remove(ct);
                    }
                    else
                    {
                        ct.Soluong -= 1;
                        ct.Thanhtien = ct.Soluong * ct.Gia;
                    }
                    MySession.Set<DonDatHang>(HttpContext.Session, "tempDdh", ddh);
                }
            }
            return RedirectToAction("Index");
        }
        public IActionResult capNhatSL(string id, string sl)
        {
            DonDatHang ddh = MySession.Get<DonDatHang>(HttpContext.Session, "tempDdh");
            if (ddh != null)
            {
                ChiTietDonDatHang? ct = null;
                foreach (ChiTietDonDatHang a in ddh.ChiTietDonDatHangs.Where(t => t.MaSp == id))
                {
                    ct = a; break;
                }
                if (ct != null)
                {
                    SanPham? sp = db.SanPhams.FirstOrDefault(k => k.MaSp == id);
                    int soLuongMoi = int.Parse(sl);
                    if (sp.Soluongton < soLuongMoi)
                    {
                        TempData["MessageError_DonHang"] = "Số lượng sản phẩm trong kho không đủ để đáp ứng yêu cầu của bạn!";
                        return RedirectToAction("Index");
                    }
                    ct.Soluong = soLuongMoi;
                    ct.Thanhtien = ct.Soluong * ct.Gia;
                    MySession.Set<DonDatHang>(HttpContext.Session, "tempDdh", ddh);
                }
            }
            return RedirectToAction("Index");
        }

        public IActionResult thanhToan(string user)
        {
            if (!string.IsNullOrEmpty(user))
            {
                NguoiDung? nd = db.NguoiDungs.FirstOrDefault(t => t.Email == user);
                if (nd != null)
                {
                    DonDatHang ddh = MySession.Get<DonDatHang>(HttpContext.Session, "tempDdh");
                    if (ddh == null || ddh.ChiTietDonDatHangs.Count == 0)
                    {
                        TempData["MessageError"] = "Đơn hàng chưa được tạo!";
                        return RedirectToAction("Index", "Home");
                    }
                    ddh.MaNd = nd.MaNd;
                    ddh.MaNv = null;
                    ddh.Ngaydat = DateTime.Now;
                    ddh.Trangthai = "Đang xử lý";
                    ddh.Tongtien = ddh.ChiTietDonDatHangs.Sum(t => t.Thanhtien);
                    if (nd.Diachi == null || nd.Sdt == null)
                    {
                        TempData["MessageError_NguoiDung"] = "Vui lòng cập nhật địa chỉ và số điện thoại trước khi thanh toán!";
                        return RedirectToAction("XemTaiKhoan", "TaiKhoan", new { email = nd.Email });
                    }
                    ddh.Diachi = nd.Diachi;
                    ddh.TtThanhtoan = "Chưa thanh toán";
                    ddh.MaNdNavigation = nd;
                    foreach (ChiTietDonDatHang ct in ddh.ChiTietDonDatHangs)
                    {
                        SanPham? sp = db.SanPhams.FirstOrDefault(sp => sp.MaSp == ct.MaSp);
                        ct.MaSpNavigation = sp ?? new SanPham();
                    }
                    MySession.Set<DonDatHang>(HttpContext.Session, "tempDdh", ddh);
                    return View(ddh);
                }
                NhanVien? nv = db.NhanViens.FirstOrDefault(t => t.Email == user);
                if(nv != null)
                {
                    TempData["MessageError_NguoiDung"] = "Chưa hỗ trợ nhân viên đặt hàng!";
                    return RedirectToAction("XemTaiKhoan", "TaiKhoan", new { email = nv.Email });
                }
            }
            return RedirectToAction("DangNhap", "TaiKhoan");
        }

        public IActionResult xacNhanThanhToan(int id, string Tennd, string Sdt, string Diachi)
        {
            DonDatHang? tempDdh = MySession.Get<DonDatHang>(HttpContext.Session, "tempDdh");
            if (tempDdh == null || tempDdh.ChiTietDonDatHangs.Count == 0)
            {
                TempData["MessageError"] = "Đơn hàng chưa được tạo!";
                return RedirectToAction("Index", "Home");
            }
            try
            {
                foreach (ChiTietDonDatHang ct in tempDdh.ChiTietDonDatHangs)
                {
                    SanPham? sp = db.SanPhams.FirstOrDefault(s => s.MaSp == ct.MaSp);
                    if (sp != null)
                    {
                        if (sp.Soluongton < ct.Soluong)
                        {
                            string tenSp = db.SanPhams.FirstOrDefault(sp => sp.MaSp == ct.MaSp)?.Tensp ?? "Sản phẩm";
                            TempData["MessageError_DonHang"] = "Số lượng của " + tenSp + " đã thay đổi do không còn đủ số lượng trong kho!";
                            ct.Soluong = sp.Soluongton ?? 0;
                            ct.Thanhtien = ct.Soluong * ct.Gia;
                            MySession.Set<DonDatHang>(HttpContext.Session, "tempDdh", tempDdh);
                            return RedirectToAction("Index");
                        }
                        sp.Soluongton -= ct.Soluong;
                        db.SanPhams.Update(sp);
                    }
                }
                DonDatHang ddh = new DonDatHang();
                NguoiDung? nd = db.NguoiDungs.FirstOrDefault(t => t.MaNd == id);
                if (nd == null)
                {
                    TempData["MessageError"] = "Người dùng không tồn tại!";
                    return RedirectToAction("Index", "Home");
                }
                ddh.MaNd = nd.MaNd;
                ddh.MaNv = db.NhanViens.First().MaNv; // Gán admin đầu tiên làm người xử lý đơn hàng tạm thời
                ddh.Ngaydat = DateTime.Now;
                ddh.Diachi = Diachi ?? "Địa chỉ không xác định!";
                ddh.MaNdNavigation = nd;
                ddh.ChiTietDonDatHangs = tempDdh.ChiTietDonDatHangs
                     .Select(ct => new ChiTietDonDatHang
                     {
                         MaSp = ct.MaSp,
                         Soluong = ct.Soluong,
                         Gia = ct.Gia,
                         Thanhtien = ct.Thanhtien
                     }).ToList();
                ddh.Tongtien = tempDdh.ChiTietDonDatHangs.Sum(t => t.Thanhtien);
                ddh.Trangthai = "Chưa xác nhận";
                ddh.TtThanhtoan = "Đã thanh toán";
                db.DonDatHangs.Add(ddh);
                db.SaveChanges();
                HttpContext.Session.Remove("tempDdh");
                TempData["MessageSuccess_ThanhToan"] = "Thanh toán đơn hàng thành công!";
                return RedirectToAction("lichSuDDH", "TaiKhoan", new { id = nd.MaNd });
            }
            catch (Exception)
            {
                TempData["MessageError_ThanhToan"] = "Thanh toán đơn hàng thất bại!";
                return RedirectToAction("lichSuDDH", "TaiKhoan");
            }
        }

        [HttpPost]
        public IActionResult ThanhToanVNPay(int orderId)
        {
            // Lấy đơn hàng từ session
            DonDatHang ddh = MySession.Get<DonDatHang>(HttpContext.Session, "tempDdh");
            if (ddh == null || ddh.ChiTietDonDatHangs.Count == 0)
            {
                TempData["MessageError"] = "Đơn hàng chưa được tạo!";
                return RedirectToAction("Index", "Home");
            }

            // Tính tổng tiền VNĐ theo VNPay (phải là long, *100)
            long totalAmount = (long)(ddh.ChiTietDonDatHangs.Sum(t => t.Thanhtien) * 100);

            var vnpay = new VnPay();
            var config = HttpContext.RequestServices.GetRequiredService<IConfiguration>();

            string ipAddress = HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString() ?? "127.0.0.1";

            // Thêm các thông số bắt buộc của VNPay
            vnpay.AddRequestData("vnp_Version", config["VnPay:Version"]);            // "2.1.0"
            vnpay.AddRequestData("vnp_Command", config["VnPay:Command"]);            // "pay"
            vnpay.AddRequestData("vnp_TmnCode", config["VnPay:TmnCode"]);
            vnpay.AddRequestData("vnp_Amount", totalAmount.ToString());              // bắt buộc long, không dấu
            vnpay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", "VND");
            vnpay.AddRequestData("vnp_IpAddr", ipAddress);
            vnpay.AddRequestData("vnp_Locale", "vn");
            vnpay.AddRequestData("vnp_OrderInfo", $"ThanhToanDonHang_{orderId}");
            vnpay.AddRequestData("vnp_OrderType", "other");
            vnpay.AddRequestData("vnp_ReturnUrl", config["VnPay:ReturnUrl"]);
            vnpay.AddRequestData("vnp_TxnRef", orderId.ToString());

            // Tạo URL thanh toán VNPay
            string paymentUrl = vnpay.CreateRequestUrl(config["VnPay:BaseUrl"], config["VnPay:HashSecret"]);

            return Redirect(paymentUrl);
        }

        public IActionResult VNPayReturn()
        {
            var config = HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            string hashSecret = config["VnPay:HashSecret"];

            VnPay vnpay = new VnPay();

            // Lấy query string VNPay trả về
            foreach (var key in Request.Query.Keys)
            {
                if (!string.IsNullOrEmpty(key) && key.StartsWith("vnp_"))
                {
                    vnpay.AddResponseData(key, Request.Query[key]);
                }
            }

            string vnp_SecureHash = Request.Query["vnp_SecureHash"];
            bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, hashSecret);

            if (!checkSignature)
            {
                TempData["MessageError"] = "Chữ ký VNPay không hợp lệ!";
                return RedirectToAction("Index", "Home");
            }

            string responseCode = Request.Query["vnp_ResponseCode"];
            DonDatHang tempDdh = MySession.Get<DonDatHang>(HttpContext.Session, "tempDdh");

            if (responseCode == "00" && tempDdh != null)
            {
                // Thanh toán thành công → lưu đơn vào DB
                NguoiDung nd = db.NguoiDungs.FirstOrDefault(t => t.MaNd == tempDdh.MaNd);
                if (nd != null)
                {
                    foreach (ChiTietDonDatHang ct in tempDdh.ChiTietDonDatHangs)
                    {
                        SanPham sp = db.SanPhams.FirstOrDefault(s => s.MaSp == ct.MaSp);
                        if (sp != null)
                        {
                            sp.Soluongton -= ct.Soluong;
                            db.SanPhams.Update(sp);
                        }
                    }

                    DonDatHang ddh = new DonDatHang
                    {
                        MaNd = nd.MaNd,
                        MaNv = db.NhanViens.First().MaNv,
                        Ngaydat = DateTime.Now,
                        Diachi = tempDdh.Diachi,
                        Trangthai = "Chưa xác nhận",
                        TtThanhtoan = "Đã thanh toán",
                        ChiTietDonDatHangs = tempDdh.ChiTietDonDatHangs.Select(ct => new ChiTietDonDatHang
                        {
                            MaSp = ct.MaSp,
                            Soluong = ct.Soluong,
                            Gia = ct.Gia,
                            Thanhtien = ct.Thanhtien
                        }).ToList(),
                        Tongtien = tempDdh.ChiTietDonDatHangs.Sum(t => t.Thanhtien)
                    };

                    db.DonDatHangs.Add(ddh);
                    db.SaveChanges();

                    HttpContext.Session.Remove("tempDdh");
                    TempData["MessageSuccess_ThanhToan"] = "Thanh toán VNPay thành công!";
                }
            }
            else
            {
                TempData["MessageError_ThanhToan"] = "Thanh toán thất bại hoặc bị hủy!";
            }

            return RedirectToAction("Index", "Home");
        }

    }
}
