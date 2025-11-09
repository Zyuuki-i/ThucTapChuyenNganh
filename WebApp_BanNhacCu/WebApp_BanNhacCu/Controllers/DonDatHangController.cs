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
        
    }

}
