using ApiCatalog.Models;
using ApiCatalog.Pagination;

namespace ApiCatalog.Repository
{
    public interface IProductRepository :IRepository<Product>
    {
        PagedList<Product> GetProducts(ProductsParameters productsParameters);
        IEnumerable<Product> GetProductsByPrice();
    }
}
