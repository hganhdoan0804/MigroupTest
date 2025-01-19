using Migroup2.Models;

namespace Migroup2.Repository
{
    public interface IProductTypeRepository
    {
        TLoaiSp Add(TLoaiSp productType);
        TLoaiSp Update(TLoaiSp productType);
        TLoaiSp Delete(string productCode);
        TLoaiSp GetProductType(string productCode);
        IEnumerable<TLoaiSp> GetAllProductType();
    }

    public class ProductTypeRepository : IProductTypeRepository
    {
        private readonly QlbanVaLiContext _context;
        public ProductTypeRepository (QlbanVaLiContext context)
        {
            _context = context;
        }

        public TLoaiSp Add(TLoaiSp productType)
        {
            _context.TLoaiSps.Add(productType);
            _context.SaveChanges();
            return productType;
        }

        public TLoaiSp Delete(string productCode)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TLoaiSp> GetAllProductType()
        {
            return _context.TLoaiSps;
        }

        public TLoaiSp GetProductType(string productCode)
        {
            return _context.TLoaiSps.Find(productCode);
        }

        public TLoaiSp Update(TLoaiSp productType)
        {
            _context.Update(productType);
            _context.SaveChanges();
            return productType;
        }
    }
}
