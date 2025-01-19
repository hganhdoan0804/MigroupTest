using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Migroup2.Models;
using System.Diagnostics;
using X.PagedList;

namespace Migroup2.Controllers
{
    public class HomeController : Controller
    {
        private QlbanVaLiContext dbContext = new QlbanVaLiContext();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(int? page)
        {
            int pageSize = 4;
            int pageNumber = (page == null || page < 0) ? 1 : page.Value;
            var listProduct = dbContext.TDanhMucSps.AsNoTracking().OrderBy(x => x.TenSp);
            PagedList<TDanhMucSp> pageList = new PagedList<TDanhMucSp>(listProduct, pageNumber, pageSize);
            return View(pageList);
        }

        public IActionResult ProductsByType(string productType, int? page)
        {
            int pageSize = 4;
            int pageNumber = (page == null || page < 0) ? 1 : page.Value;
            var listProduct = dbContext.TDanhMucSps.AsNoTracking().Where(x => x.MaLoai == productType).OrderBy(x => x.TenSp);
            PagedList<TDanhMucSp> pageList = new PagedList<TDanhMucSp>(listProduct, pageNumber, pageSize);
            ViewBag.ProductType = productType;
            return View(pageList);
        }

        public IActionResult Details(string productCode)
        {
            var product = dbContext.TDanhMucSps.SingleOrDefault(x => x.MaSp == productCode);
            var productPicture = dbContext.TAnhSps.Where(x => x.MaSp == productCode).ToList();
            ViewBag.ProductPicture = productPicture;
            return View(product);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
