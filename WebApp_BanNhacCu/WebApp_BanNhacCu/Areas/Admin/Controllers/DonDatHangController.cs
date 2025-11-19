using Microsoft.AspNetCore.Mvc;
using WebApp_BanNhacCu.Models;
using WebApp_BanNhacCu.Areas.Admin.MyModels;

namespace WebApp_BanNhacCu.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DonDatHangController : Controller
    {
        ZyuukiMusicStoreContext db = new ZyuukiMusicStoreContext();
        public IActionResult Index()
        {
            List<CDonDatHang> ds = db.DonDatHangs.Select(t => CDonDatHang.chuyenDoi(t)).ToList();
            return View(ds);
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

            ViewBag.ChiTietDon = chiTiet;

            return View(model);
        }


    }
}

