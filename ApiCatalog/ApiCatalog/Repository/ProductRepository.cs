using ApiCatalog.Context;
using ApiCatalog.Models;
using ApiCatalog.Pagination;

namespace ApiCatalog.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ApiCatalogContext context) : base(context) 
        {}

        public PagedList<Product> GetProducts(ProductsParameters productsParameters)
        {
            //return Get()
            //    .OrderBy(on => on.Name)
            //    .Skip((productsParameters.PageNumber -1) * productsParameters.PageSize)
            //    .Take((productsParameters.PageSize))
            //    .ToList();

            return PagedList<Product>.ToPagedList(Get().OrderBy(on => on.ProductId), productsParameters.PageNumber, productsParameters.PageSize);
        }

        public IEnumerable<Product> GetProductsByPrice()
        {
            return Get().OrderBy(c => c.Price).ToList();
        }
    }
}
