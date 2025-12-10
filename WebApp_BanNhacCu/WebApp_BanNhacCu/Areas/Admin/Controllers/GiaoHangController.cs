using Microsoft.AspNetCore.Mvc;
using WebApp_BanNhacCu.Areas.Admin.MyModels;
using WebApp_BanNhacCu.Models;

namespace WebApp_BanNhacCu.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GiaoHangController : Controller
    {
        ZyuukiMusicStoreContext db = new ZyuukiMusicStoreContext();
        public IActionResult Index(string manv="", int trang=1)
        {
            List<CDonDatHang> ds = db.DonDatHangs
                .Where(t => t.Trangthai == "Đang xử lý")
                .Select(t => CDonDatHang.chuyenDoi(t))
                .ToList();
            ViewBag.NV = db.NhanViens.Where(t=>t.MaVt=="Carrier").ToList();
            ViewBag.MaNV = manv;

            int soSP = 8;
            int tongSP = ds.Count;
            int soTrang = (int)Math.Ceiling((double)tongSP / soSP);

            List<CDonDatHang> dsDon = ds.Skip((trang - 1) * soSP).Take(soSP).ToList();
            ViewBag.trangHienTai = trang;
            ViewBag.tongTrang = soTrang;
            return View(dsDon);
        }

        public IActionResult Shipper(string id)
        {
            NhanVien? nv = db.NhanViens.Find(id);
            return PartialView(nv);
        }

        public IActionResult banGiaoDDH(IFormCollection fc)
        {
            string manv = fc["Manv"].ToString();
            bool flag = false;
            NhanVien? nv = db.NhanViens.Find(manv);
            if (nv == null)
            {
                TempData["MessageError_GiaoHang"] = "Có lỗi khi chọn nhân viên hoặc nhân viên không tồn tại!";
            }
            else
            {
                foreach (DonDatHang ddh in db.DonDatHangs.ToList())
                {
                    if (fc.ContainsKey(ddh.MaDdh.ToString()))
                    {
                        if(!flag) flag = true;
                        ddh.Trangthai = "Đã xử lý";
                        GiaoHang gh = new GiaoHang();
                        gh.MaDdh = ddh.MaDdh;
                        gh.MaNv = nv.MaNv;
                        gh.Ngaybd = DateTime.Now;
                        gh.Ngaykt = DateTime.Now.AddDays(7);
                        gh.Tongthu = ddh.Phuongthuc == "COD" ? ddh.Tongtien : 0;
                        gh.Trangthai = "Chờ giao hàng";
                        db.GiaoHangs.Add(gh);
                    }
                }
                if (flag)
                {
                    db.SaveChanges();
                    TempData["MessageSuccess_GiaoHang"] = "Bàn giao thành công tới nhân viên " + nv.Tennv;
                }
                else
                {
                    TempData["MessageError_GiaoHang"] = "Vui lòng chọn đơn cần bàn giao!";
                }
            }
            int tranght = 1;
            Int32.TryParse(fc["trangHT"],out tranght);
            return RedirectToAction("Index", new {manv = manv, trang = tranght });
        }

        public IActionResult trangThaiGH(int trang = 1)
        {
            List<GiaoHang> ds = db.GiaoHangs.ToList();

            foreach (GiaoHang gh in ds)
            {
                gh.MaNvNavigation = db.NhanViens.Find(gh.MaNv);
            }

            int soSP = 6;
            int tongSP = ds.Count;
            int soTrang = (int)Math.Ceiling((double)tongSP / soSP);

            ds = ds.Skip((trang - 1) * soSP).Take(soSP).ToList();

            ViewBag.trangHienTai = trang;
            ViewBag.tongTrang = soTrang;
            return View(ds);
        }
    }
}
