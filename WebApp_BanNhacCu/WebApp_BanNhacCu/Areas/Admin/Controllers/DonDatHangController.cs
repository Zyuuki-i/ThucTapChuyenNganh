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
            List<CDonDatHang> ds = new List<CDonDatHang>();
            foreach (DonDatHang ddh in db.DonDatHangs.ToList())
            {
                if (ddh.MaGiamgia != null)
                {
                    MaGiamGia mgg = db.MaGiamGia.Find(ddh.MaGiamgia);
                    ddh.MaGiamgiaNavigation = mgg;
                }
                if (ddh.MaKh != null)
                {
                    KhachHang kh = db.KhachHangs.Find(ddh.MaKh);
                    ddh.MaKhNavigation = kh;
                }
                ds.Add(CDonDatHang.chuyendoi(ddh));
            }
            return View(ds);
        }


    }
}

