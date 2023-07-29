using ApiCatalog.Models;
using ApiCatalog.Pagination;

namespace ApiCatalog.Repository
{
    public interface IProductRepository :IRepository<Product>
    {
        Task<PagedList<Product>> GetProducts(ProductsParameters productsParameters);
        Task<IEnumerable<Product>> GetProductsByPrice();
    }
}
