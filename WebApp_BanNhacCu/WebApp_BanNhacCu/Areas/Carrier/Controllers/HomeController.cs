using Microsoft.AspNetCore.Mvc;
using WebApp_BanNhacCu.Areas.Carrier.MyModels;
using WebApp_BanNhacCu.Models;

namespace WebApp_BanNhacCu.Areas.Carier.Controllers
{
    [Area("Carrier")]
    public class HomeController : Controller
    {
        ZyuukiMusicStoreContext db = new ZyuukiMusicStoreContext();
        public IActionResult Index()
        {
            string? email = HttpContext.Session.GetString("UserEmail");
            if (email == null)
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            NhanVien? nv = db.NhanViens.FirstOrDefault(n => n.Email == email);
            List<GiaoHang> ds = new List<GiaoHang>();
            if (nv != null)
            {
                foreach (GiaoHang gh in db.GiaoHangs.ToList())
                {
                    if (gh.MaNv == nv.MaNv && gh.Trangthai != "Đã giao hàng" && gh.Trangthai != "Giao thất bại")
                    {
                        gh.MaNvNavigation = nv;
                        gh.MaDdhNavigation = db.DonDatHangs.FirstOrDefault(d => d.MaDdh == gh.MaDdh);
                        if (gh.MaDdhNavigation != null)
                        {
                            gh.MaDdhNavigation.MaNdNavigation = db.NguoiDungs.FirstOrDefault(u => u.MaNd == gh.MaDdhNavigation.MaNd);
                        }
                        ds.Add(gh);
                    }
                }
            }
            ViewBag.donChoGiao = ds.Where(t => t.Trangthai == "Chờ giao hàng").ToList();
            ViewBag.donDangGiao = ds.Where(t => t.Trangthai == "Đang giao hàng").ToList();
            return View();
        }

        public IActionResult donHoanThanh()
        {
            string? email = HttpContext.Session.GetString("UserEmail");
            if (email == null)
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            NhanVien? nv = db.NhanViens.FirstOrDefault(n => n.Email == email);
            List<GiaoHang> ds = new List<GiaoHang>();
            if (nv != null)
            {
                foreach (GiaoHang gh in db.GiaoHangs.ToList())
                {
                    if (gh.MaNv == nv.MaNv && gh.Trangthai == "Đã giao hàng")
                    {
                        gh.MaNvNavigation = nv;          
                        ds.Add(gh);
                    }
                }
            }
            return View(ds);
        }

        public IActionResult donThatBai()
        {
            string? email = HttpContext.Session.GetString("UserEmail");
            if (email == null)
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            NhanVien? nv = db.NhanViens.FirstOrDefault(n => n.Email == email);
            List<GiaoHang> ds = new List<GiaoHang>();
            if (nv != null)
            {
                foreach (GiaoHang gh in db.GiaoHangs.ToList())
                {
                    if (gh.MaNv == nv.MaNv && gh.Trangthai == "Giao thất bại")
                    {
                        gh.MaNvNavigation = nv;
                        ds.Add(gh);
                    }
                }
            }
            return View(ds);
        }

        public IActionResult chiTietDH(int id)
        {
            string? email = HttpContext.Session.GetString("UserEmail");
            if (email == null)
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            NhanVien? nv = db.NhanViens.FirstOrDefault(n => n.Email == email);
            DonDatHang? ddh = db.DonDatHangs.FirstOrDefault(d => d.MaDdh == id);
            if (ddh == null)
            {
                return RedirectToAction("Index");
            }
            foreach (ChiTietDonDatHang ct in db.ChiTietDonDatHangs.Where(t=>t.MaDdh==ddh.MaDdh).ToList())
            {
                ct.MaSpNavigation = db.SanPhams.FirstOrDefault(s => s.MaSp == ct.MaSp);
                ddh.ChiTietDonDatHangs.Add(ct);
            }
            ddh.MaNvNavigation = db.NhanViens.FirstOrDefault(n => n.MaNv == ddh.MaNv);
            ddh.MaNdNavigation = db.NguoiDungs.FirstOrDefault(u => u.MaNd == ddh.MaNd);
            ViewBag.Shipper = nv;
            return View(ddh);
        }

        public IActionResult xacNhan(int? id)
        {
            if(id != null)
            {
                GiaoHang? gh = db.GiaoHangs.FirstOrDefault(g => g.MaGh == id && g.Trangthai == "Chờ giao hàng");
                if (gh != null)
                {
                    gh.Trangthai = "Đang giao hàng";
                    db.Update(gh);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }

        public IActionResult giaoThanhCong(int? id)
        {
            if (id != null)
            {
                GiaoHang? gh = db.GiaoHangs.FirstOrDefault(g => g.MaGh == id);
                if (gh != null)
                {
                    DonDatHang? ddh = db.DonDatHangs.FirstOrDefault(d => d.MaDdh == gh.MaDdh);
                    if(ddh != null)
                    {
                        gh.Trangthai = "Đã giao hàng";
                        ddh.Trangthai = "Chờ xác nhận";
                        db.Update(gh);
                        db.Update(ddh);
                        db.SaveChanges();
                    }
                }
            }
            return RedirectToAction("Index");
        }

        public IActionResult giaoThatBai(int? id)
        {
            if (id != null)
            {
                GiaoHang? gh = db.GiaoHangs.FirstOrDefault(g => g.MaGh == id);
                if (gh != null)
                {
                    DonDatHang? ddh = db.DonDatHangs.FirstOrDefault(d => d.MaDdh == gh.MaDdh);
                    if (ddh != null)
                    {
                        gh.Trangthai = "Giao thất bại";
                        ddh.Trangthai = "Đang xử lý";
                        db.Update(gh);
                        db.Update(ddh);
                        db.SaveChanges();
                    }
                }
            }
            return RedirectToAction("Index");
        }

    }
}
