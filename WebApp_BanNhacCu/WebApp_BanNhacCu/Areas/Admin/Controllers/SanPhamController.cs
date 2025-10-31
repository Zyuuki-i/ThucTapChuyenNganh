using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp_BanNhacCu.Areas.Admin.MyModels;
using WebApp_BanNhacCu.Models;

namespace WebApp_BanNhacCu.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SanPhamController : Controller
    {
        ZyuukiMusicStoreContext db = new ZyuukiMusicStoreContext();
        public IActionResult Index()
        {
            List<CSanPham> ds = db.SanPhams.Select(t => CSanPham.chuyenDoi(t)).ToList();
            return View(ds);
        }
        public IActionResult formThemSP()
        {
            ViewBag.DSNsx = new SelectList(db.NhaSanXuats.ToList(), "MaNsx", "MaNsx");
            ViewBag.DSLoai = new SelectList(db.LoaiSanPhams.ToList(), "MaLoai", "MaLoai");
            return View();
        }

        public IActionResult themSanPham(CSanPham x)
        {
            ViewBag.DSNsx = new SelectList(db.NhaSanXuats.ToList(), "MaNsx", "MaNsx");
            ViewBag.DSLoai = new SelectList(db.LoaiSanPhams.ToList(), "MaLoai", "MaLoai");
            if (ModelState.IsValid)
            {
                if (db.SanPhams.Find(x.MaSp) != null)
                {
                    ModelState.AddModelError("", "Sản phẩm đã tồn tại!!!");
                    return View("formThemSP");
                }
                try
                {
                    SanPham sp = CSanPham.chuyenDoi(x);
                    db.SanPhams.Add(sp);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Có lỗi khi thêm sản phẩm!!!");
                    return View("formThemsp");
                }
            }
            return View("formThemSP");
        }

        public IActionResult formXoaSP(string id)
        {
            SanPham? s = db.SanPhams.Find(id);
            if (s == null)
            {
                return RedirectToAction("Index");
            }
            CSanPham sp = CSanPham.chuyenDoi(s);
            return View(sp);
        }

        public IActionResult xoaSanPham(string id)
        {
            SanPham? sp = db.SanPhams.Find(id);
            if (db.KhoHangs.Any(t => t.MaSp == id) || db.KhoHangs.Any(t => t.MaSp == id) || db.ChiTietHoaDons.Any(t => t.MaSp == id))
            {
                ModelState.AddModelError("", "Không thể xóa sản phẩm này!!!");
                return View("formXoaSP", CSanPham.chuyenDoi(sp));
            }
            try
            {
                db.SanPhams.Remove(sp);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Có lỗi khi xóa sản phẩm!!!");
                return View("formXoaSP", CSanPham.chuyenDoi(sp));
            }
        }

        public IActionResult formSuaSP(string id)
        {
            SanPham? sp = db.SanPhams.Find(id);
            if (sp == null)
            {
                return RedirectToAction("Index");
            }
            CSanPham csp = CSanPham.chuyenDoi(sp);
            ViewBag.DSNsx = new SelectList(db.NhaSanXuats.ToList(), "MaNsx", "MaNsx");
            ViewBag.DSLoai = new SelectList(db.LoaiSanPhams.ToList(), "MaLoai", "MaLoai");
            return View(csp);
        }

        public IActionResult suaSanPham(CSanPham x)
        {
            ViewBag.DSNsx = new SelectList(db.NhaSanXuats.ToList(), "MaNsx", "MaNsx");
            ViewBag.DSLoai = new SelectList(db.LoaiSanPhams.ToList(), "MaLoai", "MaLoai");
            if (ModelState.IsValid)
            {
                try
                {
                    SanPham sp = CSanPham.chuyenDoi(x);
                    db.SanPhams.Update(sp);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Có lỗi khi sửa sản phẩm!!!");
                    return View("formSuaSP", x);
                }
            }
            return View("formSuaSP", x);
        }

        public IActionResult timKiem(string MaSp)
        {
            List<CSanPham> ds = new List<CSanPham>();
            SanPham? sp = db.SanPhams.Find(MaSp);
            if(sp != null)
            {
                CSanPham csp = CSanPham.chuyenDoi(sp);
                ds.Add(csp);
            }
            return View(ds);
        }

        public IActionResult chiTietSP()
        {
            return RedirectToAction("Index");
        }
    }
}
