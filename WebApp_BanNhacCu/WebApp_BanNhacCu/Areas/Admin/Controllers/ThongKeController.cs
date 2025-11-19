using Microsoft.AspNetCore.Mvc;
using WebApp_BanNhacCu.Models;
using WebApp_BanNhacCu.Areas.Admin.MyModels;

namespace WebApp_BanNhacCu.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ThongKeController : Controller
    {
        ZyuukiMusicStoreContext db = new ZyuukiMusicStoreContext();
        public IActionResult Index()
        {
            List<CThongKecs> ds = new List<CThongKecs>();
            for (int i = 1; i <= 12; i++)
            {
                ds.Add(new CThongKecs
                {
                    Thang = i,
                    SoLuongDon = 0,
                    TongTien = 0
                });
            }
            var donHang = db.DonDatHangs.ToList();

            foreach (var d in donHang)
            {
                if (d.Ngaydat != null)
                {
                    int thang = d.Ngaydat.Value.Month;
                    foreach (var item in ds)
                    {
                        if (item.Thang == thang)
                        {
                            item.SoLuongDon += 1;
                            item.TongTien += (d.Tongtien ?? 0);
                            break;
                        }
                    }
                }
            }
            return View(ds);
        }
    }
}
