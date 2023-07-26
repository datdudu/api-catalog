using ApiCatalog.Models;
using ApiCatalog.Pagination;

namespace ApiCatalog.Repository
{
    public interface IProductRepository :IRepository<Product>
    {
        IEnumerable<Product> GetProducts(ProductsParameters productsParameters);
        IEnumerable<Product> GetProductsByPrice();
    }
}
