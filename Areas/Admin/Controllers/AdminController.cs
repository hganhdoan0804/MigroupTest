using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Migroup2.Models;
using X.PagedList;
using X.PagedList.Extensions;

namespace Migroup2.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin")]
    [Route("admin/admin")]
    public class AdminController : Controller
    {
        QlbanVaLiContext dbContext = new QlbanVaLiContext();
        [Route("index")]
        [HttpGet]
        public IActionResult Index(int? page, string searchQuery)
        {
            int pageSize = 8;
            int pageNumber = (page == null || page < 0) ? 1 : page.Value;
            IQueryable<TDanhMucSp> listProduct = dbContext.TDanhMucSps.AsNoTracking();
            if (!string.IsNullOrEmpty(searchQuery))
            {
                listProduct = listProduct.Where(x => x.TenSp.Contains(searchQuery));
                ViewBag.SearchQuery = searchQuery;
            }
            var pageList = listProduct.OrderBy(x => x.TenSp).ToPagedList(pageNumber, pageSize);
            return View(pageList);
        }


        [Route("Add")]
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.MaterialCode = new SelectList(dbContext.TChatLieus.ToList(), "MaChatLieu", "ChatLieu");
            ViewBag.ProductionCode = new SelectList(dbContext.THangSxes.ToList(), "MaHangSx", "HangSx");
            ViewBag.CountryCode = new SelectList(dbContext.TQuocGia.ToList(), "MaNuoc", "TenNuoc");
            ViewBag.CategoryCode = new SelectList(dbContext.TLoaiSps.ToList(), "MaLoai", "Loai");
            ViewBag.ObjectCode = new SelectList(dbContext.TLoaiDts.ToList(), "MaDt", "TenLoai");
            return View();
        }

        [Route("Add")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(TDanhMucSp product)
        {
            if (ModelState.IsValid)
            {
                dbContext.TDanhMucSps.Add(product);
                dbContext.SaveChanges();
                return RedirectToAction("Index", "Admin");
            }
            return View(product);
        }

        [Route("Edit")]
        [HttpGet]
        public IActionResult Edit(string productCode)
        {
            ViewBag.MaterialCode = new SelectList(dbContext.TChatLieus.ToList(), "MaChatLieu", "ChatLieu");
            ViewBag.ProductionCode = new SelectList(dbContext.THangSxes.ToList(), "MaHangSx", "HangSx");
            ViewBag.CountryCode = new SelectList(dbContext.TQuocGia.ToList(), "MaNuoc", "TenNuoc");
            ViewBag.CategoryCode = new SelectList(dbContext.TLoaiSps.ToList(), "MaLoai", "Loai");
            ViewBag.ObjectCode = new SelectList(dbContext.TLoaiDts.ToList(), "MaDt", "TenLoai");
            var product = dbContext.TDanhMucSps.Find(productCode);
            return View(product);
        }

        [Route("Edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TDanhMucSp product)
        {
            if (ModelState.IsValid)
            {
                dbContext.Update(product);
                dbContext.SaveChanges();
                return RedirectToAction("Index", "Admin");
            }
            return View(product);
        }

        [Route("Delete")]
        [HttpGet]
        public IActionResult Delete(string productCode)
        {
            TempData["Message"] = "";
            var detailProduct = dbContext.TChiTietSanPhams.Where(x => x.MaSp == productCode).ToList();
            if (detailProduct.Count > 0)
            {
                TempData["Message"] = "Không thể xóa sản phẩm này vì nó đang có chi tiết liên quan.";
                return RedirectToAction("Index", "Admin");
            }
            var productPicture = dbContext.TAnhSps.Where(x => x.MaSp == productCode).ToList();
            if (productPicture.Any())
            {
                dbContext.RemoveRange(productPicture);
            }
            dbContext.Remove(dbContext.TDanhMucSps.Find(productCode));
            dbContext.SaveChanges();
            TempData["Message"] = "Xóa thành công";
            return RedirectToAction("Index", "Admin");
        }
    }
}
