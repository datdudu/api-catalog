using ApiCatalog.Context;
using ApiCatalog.Models;

namespace ApiCatalog.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ApiCatalogContext context) : base(context) 
        {}
        public IEnumerable<Product> GetProductsByPrice()
        {
            return Get().OrderBy(c => c.Price).ToList();
        }
    }
}
