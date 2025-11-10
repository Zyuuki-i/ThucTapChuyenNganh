using Microsoft.AspNetCore.Mvc;
using web_session.Models;
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
                    dsHinh = db.Hinhs.Where(h => h.MaSp == ct.MaSp).ToList();
                }
                ViewBag.DsHinh = dsHinh;
            }
            return View(dsCT);
        }

        private DonDatHang donDatHang(string id, int soluong)
        {
            SanPham? sp = db.SanPhams.Find(id);
            if (sp == null)
            {
                return null;
            }
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
                ct = new ChiTietDonDatHang();
                ct.MaSp = sp.MaSp;
                ct.Soluong = soluong;
                ct.Gia = sp.Giasp;
                ct.Chietkhau = 0;
                ct.Thanhtien = soluong * sp.Giasp;
                ct.MaSpNavigation = db.SanPhams.Find(ct.MaSp);
                ddh.ChiTietDonDatHangs.Add(ct);
            }
            else
            {
                foreach (ChiTietDonDatHang a in ddh.ChiTietDonDatHangs.Where(t => t.MaSp == sp.MaSp))
                {
                    a.Soluong += soluong;
                }
            }
            MySession.Set<DonDatHang>(HttpContext.Session, "tempDdh", ddh);
            return ddh;
        }

        public IActionResult themVaoGio(string id, int soluong)
        {
            DonDatHang ddh = donDatHang(id, soluong);
            if(ddh == null)
            {
                return RedirectToAction("Index","SanPham");
            }
            TempData["Message"] = "Thêm sản phẩm vào giỏ hàng thành công!";
            return Redirect(Request.Headers["Referer"].ToString());
        }

        public IActionResult muaNgay(string id, int soluong)
        {
            DonDatHang ddh = donDatHang(id, soluong);
            if (ddh == null)
            {
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
                    if(ct.Soluong <= 1)
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

        public IActionResult thanhToan(string user)
        {
            if(string.IsNullOrEmpty(user))
            {
                return RedirectToAction("DangNhap", "TaiKhoan");
            }
            else
            {
                TaiKhoan tk = db.TaiKhoans.FirstOrDefault(t => t.Email == user);
                if (tk == null)
                {
                    return RedirectToAction("DangNhap", "TaiKhoan");
                }
                else
                {
                    KhachHang kh = db.KhachHangs.FirstOrDefault(k => k.MaKh == tk.MaTk);
                    DonDatHang ddh = MySession.Get<DonDatHang>(HttpContext.Session, "tempDdh");
                    if (ddh == null || ddh.ChiTietDonDatHangs.Count == 0)
                    {
                        return RedirectToAction("Index", "SanPham");
                    }
                    try
                    {
                        DonDatHang DDH = new DonDatHang();
                        DDH.MaKh = kh.MaKh;
                        DDH.MaGiamgia = null;
                        DDH.Ngayxuat = DateTime.Now;
                        DDH.TtThanhtoan = "Chưa thanh toán";
                        DDH.ChiTietDonDatHangs = new List<ChiTietDonDatHang>();
                        foreach(ChiTietDonDatHang ct in ddh.ChiTietDonDatHangs)
                        {
                            ChiTietDonDatHang CT = new ChiTietDonDatHang();
                            CT.MaSp = ct.MaSp;
                            CT.Soluong = ct.Soluong;
                            CT.Gia = ct.Gia;
                            CT.Chietkhau = ct.Chietkhau;
                            CT.Thanhtien = ct.Thanhtien;
                            DDH.ChiTietDonDatHangs.Add(CT);
                        }
                        DDH.Tongtien = DDH.ChiTietDonDatHangs.Sum(CT => CT.Thanhtien);
                        db.DonDatHangs.Add(DDH);
                        db.SaveChanges();
                        HttpContext.Session.Remove("tempDdh");
                        return View(DDH);
                    }
                    catch (Exception)
                    {
                        TempData["Message"] = "Đã có lỗi xảy ra trong quá trình thanh toán. Vui lòng thử lại!";
                        return RedirectToAction("Index");
                    }
                }
            }
        }


    }
}
