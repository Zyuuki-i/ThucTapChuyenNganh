using Microsoft.AspNetCore.Mvc;
using WebApp_BanNhacCu.Models;
using WebApp_BanNhacCu.Areas.Admin.MyModels;

namespace WebApp_BanNhacCu.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DonDatHangController : Controller
    {
        ZyuukiMusicStoreContext db = new ZyuukiMusicStoreContext();
        public IActionResult Index(int thang=0, int trang = 1)
        {
            List<CDonDatHang> ds;
            if (thang != 0)
            {
                ds = db.DonDatHangs
                    .Where(t => t.Ngaydat != null && t.Ngaydat.Value.Month == thang)
                    .Select(t => CDonDatHang.chuyenDoi(t))
                    .ToList();
            }
            else
            {
                ds = db.DonDatHangs.Select(t => CDonDatHang.chuyenDoi(t)).ToList();
            }
            int soSP = 9;
            int tongSP = ds.Count;
            int soTrang = (int)Math.Ceiling((double)tongSP / soSP);

            List<CDonDatHang> dsDon = ds.Skip((trang - 1) * soSP).Take(soSP).ToList();

            ViewBag.trangHienTai = trang;
            ViewBag.tongTrang = soTrang;
            TempData["thang"] = thang;
            return View(dsDon);
        }
        
        public IActionResult HuyDDH(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var ddh = db.DonDatHangs.Find(id);
            if (ddh == null)
            {
                return NotFound();
            }
            ddh.Trangthai = "Đã hủy";
            db.Update(ddh);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult chiTietDDH(int? id)
        {
            if (id == null) return NotFound();

            var ddh = db.DonDatHangs.Find(id);
            if (ddh == null) return NotFound();

            var model = CDonDatHang.chuyenDoi(ddh);

            var chiTiet = db.ChiTietDonDatHangs
                            .Where(ct => ct.MaDdh == id)
                            .ToList();
            foreach (var item in chiTiet)
            {
                item.MaSpNavigation = db.SanPhams.Find(item.MaSp) ?? new SanPham();
            }
            ViewBag.ChiTietDon = chiTiet;
            ViewBag.KH = db.NguoiDungs.Find(ddh.MaNd);
            ViewBag.NV = ddh.MaNv != null ? db.NhanViens.Find(ddh.MaNv) : null;
            return View(model);
        }
    }
}

