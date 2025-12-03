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

            List<CDonDatHang> dsSapXep = new List<CDonDatHang>();
            foreach (var item in ds)
            {
                if (item.Trangthai == "Chưa xác nhận")
                {
                    dsSapXep.Add(item);
                }
            }
            foreach (var item in ds)
            {
                if (item.Trangthai != "Chưa xác nhận")
                {
                    dsSapXep.Add(item);
                }
            }

            int soSP = 9;
            int tongSP = dsSapXep.Count;
            int soTrang = (int)Math.Ceiling((double)tongSP / soSP);

            List<CDonDatHang> dsDon = dsSapXep.Skip((trang - 1) * soSP).Take(soSP).ToList();

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

        public IActionResult xacNhanDDH(int? id)
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
            ViewBag.NV = db.NhanViens.Where(nv => nv.MaVt == "VT03").ToList();
            return View(CDonDatHang.chuyenDoi(ddh));
        }

        public IActionResult Shipper(string id)
        {
            NhanVien? nv = db.NhanViens.Find(id);
            return PartialView(CNhanVien.chuyendoi(nv));
        }

        public IActionResult banGiaoDDH(string Manv, int MaDdh)
        {
            var ddh = db.DonDatHangs.Find(MaDdh);
            if (ddh == null)
            {
                return NotFound();
            }
            ddh.MaNv = Manv;
            ddh.Trangthai = "Đã xác nhận";
            db.DonDatHangs.Update(ddh);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

