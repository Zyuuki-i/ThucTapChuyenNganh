using Microsoft.AspNetCore.Mvc;
using WebApp_BanNhacCu.Models;

namespace WebApp_BanNhacCu.ViewComponent
{
    public class DemGioHangViewComponent : Microsoft.AspNetCore.Mvc.ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            DonDatHang ddh = MySession.Get<DonDatHang>(HttpContext.Session, "tempDdh");
            int sl = ddh == null ? 0 : ddh.ChiTietDonDatHangs.Count();
            ViewBag.SoLuongGio = sl;
            return View(sl);
        }
    }
}
