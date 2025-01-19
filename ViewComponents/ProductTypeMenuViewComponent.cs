using Microsoft.AspNetCore.Mvc;
using Migroup2.Repository;
namespace Migroup2.ViewComponents
{
    public class ProductTypeMenuViewComponent : ViewComponent
    {
        private readonly IProductTypeRepository _productTypeRepository;
        public ProductTypeMenuViewComponent(IProductTypeRepository productTypeRepository)
        {
            _productTypeRepository = productTypeRepository;
        }

        public IViewComponentResult Invoke()
        {
            var productType = _productTypeRepository.GetAllProductType().OrderBy(x => x.Loai);
            return View(productType);
        }
    }
}
