using Microsoft.AspNetCore.Mvc;
using WebApp_BanNhacCu.Models;

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
                    if(hinh != null)
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
            KhoHang? kho = db.KhoHangs.Find(id);
            if (sp == null || kho == null)
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
                if (kho.Soluongton < soluong) { flag = true; }
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
                    if (kho.Soluongton < a.Soluong + soluong) { flag = true; }
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
                    KhoHang? kho = db.KhoHangs.FirstOrDefault(k => k.MaSp == id);
                    if (kho.Soluongton < ct.Soluong + 1)
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
                ChiTietDonDatHang ct = null;
                foreach (ChiTietDonDatHang a in ddh.ChiTietDonDatHangs.Where(t => t.MaSp == id))
                {
                    ct = a; break;
                }
                if (ct != null)
                {
                    KhoHang? kho = db.KhoHangs.FirstOrDefault(k => k.MaSp == id);
                    int soLuongMoi = int.Parse(sl);
                    if (kho.Soluongton < soLuongMoi)
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
                    ddh.Ngaydat = DateTime.Now;
                    ddh.Trangthai = "Đang xử lý";
                    ddh.Tongtien = ddh.ChiTietDonDatHangs.Sum(t => t.Thanhtien);
                    if (nd.Diachi == null || nd.Sdt == null)
                    {
                        TempData["MessageError_NguoiDung"] = "Vui lòng cập nhật địa chỉ và số điện thoại trước khi thanh toán!";
                        return RedirectToAction("XemTaiKhoan", "TaiKhoan", new {email=nd.Email});
                    }
                    ddh.Diachi = nd.Diachi;
                    ddh.TtThanhtoan = "Chưa thanh toán";
                    ddh.MaNdNavigation = nd;
                    foreach (ChiTietDonDatHang ct in ddh.ChiTietDonDatHangs)
                    {
                        ct.MaSpNavigation = db.SanPhams.FirstOrDefault(sp => sp.MaSp == ct.MaSp);
                    }
                    MySession.Set<DonDatHang>(HttpContext.Session, "tempDdh", ddh);
                    return View(ddh);
                }
            }
            return RedirectToAction("DangNhap", "TaiKhoan");
        }

        public IActionResult xacNhanThanhToan(int id)
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
                    KhoHang? kho = db.KhoHangs.FirstOrDefault(k => k.MaSp == ct.MaSp);
                    if (kho != null)
                    {
                        if(kho.Soluongton < ct.Soluong)
                        {
                            string tenSp = db.SanPhams.FirstOrDefault(sp => sp.MaSp == ct.MaSp)?.Tensp ?? "Sản phẩm";
                            TempData["MessageError_DonHang"] = "Số lượng của " + tenSp + " đã thay đổi do không còn đủ số lượng trong kho!";
                            ct.Soluong = kho.Soluongton;
                            ct.Thanhtien = ct.Soluong * ct.Gia;
                            MySession.Set<DonDatHang>(HttpContext.Session, "tempDdh", tempDdh);
                            return RedirectToAction("Index");
                        }
                        kho.Soluongton -= ct.Soluong;
                        db.KhoHangs.Update(kho);
                    }
                }
                DonDatHang ddh = new DonDatHang();
                NguoiDung? nd = db.NguoiDungs.FirstOrDefault(t => t.MaNd == id);
                ddh.MaNd = id;
                ddh.Ngaydat = DateTime.Now;
                ddh.Diachi = nd.Diachi;
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
                ddh.Trangthai = "Hoàn thành";
                ddh.TtThanhtoan = "Đã thanh toán";
                db.DonDatHangs.Add(ddh);
                db.SaveChanges();
                HttpContext.Session.Remove("tempDdh");
                TempData["MessageSuccess_ThanhToan"] = "Thanh toán đơn hàng thành công!";
                return RedirectToAction("lichSuDDH", "TaiKhoan",new {id=nd.MaNd}); //Chuyển đến trang lịch sử đơn hàng của người dùng
            }
            catch (Exception)
            {
                TempData["MessageError_ThanhToan"] = "Thanh toán đơn hàng thất bại!";
                return RedirectToAction("lichSuDDH", "TaiKhoan"); //Chuyển đến trang lịch sử đơn hàng của người dùng
            }
        }
    }
}
