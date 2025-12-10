using Microsoft.AspNetCore.Mvc;
using WebApp_BanNhacCu.Models;
using WebApp_BanNhacCu.Areas.Admin.MyModels;

namespace WebApp_BanNhacCu.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DonDatHangController : Controller
    {
        ZyuukiMusicStoreContext db = new ZyuukiMusicStoreContext();
        public IActionResult Index(int thang = 0)
        {
            ViewBag.thang = thang;
            return View();
        }

        public IActionResult IndexAjax(string trangthai = "", int thang = 0, int trang = 1)
        {
            List<CDonDatHang> ds;
            if (trangthai == "Hoàn thành")
            {
                ds = ds = db.DonDatHangs
                    .Where(t => t.Trangthai == "Hoàn thành")
                    .Select(t => CDonDatHang.chuyenDoi(t))
                    .ToList();
            }
            else if (trangthai == "Đã hủy")
            {
                ds = ds = db.DonDatHangs
                    .Where(t => t.Trangthai == "Đã hủy")
                    .Select(t => CDonDatHang.chuyenDoi(t))
                    .ToList();
            }
            else if (trangthai == "Đang xử lý")
            {
                ds = ds = db.DonDatHangs
                    .Where(t => t.Trangthai == "Đang xử lý")
                    .Select(t => CDonDatHang.chuyenDoi(t))
                    .ToList();
            }
            else if (trangthai == "Đã xử lý")
            {
                ds = ds = db.DonDatHangs
                    .Where(t => t.Trangthai == "Đã xử lý")
                    .Select(t => CDonDatHang.chuyenDoi(t))
                    .ToList();
            }
            else if (trangthai == "Chờ xác nhận")
            {
                ds = ds = db.DonDatHangs
                    .Where(t => t.Trangthai == "Chờ xác nhận")
                    .Select(t => CDonDatHang.chuyenDoi(t))
                    .ToList();
            }
            else
            {
                ds = ds = db.DonDatHangs
                    .Select(t => CDonDatHang.chuyenDoi(t))
                    .ToList();
            }

            if (thang != 0)
            {
                ds = ds
                    .Where(t => t.Ngaydat != null && t.Ngaydat.Value.Month == thang)
                    .ToList();
            }

            int soSP = 5;
            int tongSP = ds.Count;
            int soTrang = (int)Math.Ceiling((double)tongSP / soSP);

            List<CDonDatHang> dsDon = ds.Skip((trang - 1) * soSP).Take(soSP).ToList();

            ViewBag.trangHienTai = trang;
            ViewBag.tongTrang = soTrang;
            return PartialView(dsDon);
        }

        public IActionResult HuyDDH(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            var ddh = db.DonDatHangs.Find(id);
            if (ddh == null)
            {
                return RedirectToAction("Index");
            }
            if (ddh.Trangthai == "Đang xử lý")
            {
                ddh.Trangthai = "Đã hủy";
                db.Update(ddh);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult chiTietDDH(int? id)
        {
            if (id == null) return RedirectToAction("Index");

            var ddh = db.DonDatHangs.Find(id);
            if (ddh == null) return RedirectToAction("Index");

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
            DonDatHang? ddh = db.DonDatHangs.FirstOrDefault(d => d.MaDdh == id);
            if(ddh != null)
            {
                ddh.Trangthai = "Hoàn thành";
                ddh.TtThanhtoan = "Đã thanh toán";
                db.Update(ddh);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}

