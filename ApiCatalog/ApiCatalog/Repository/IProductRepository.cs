using ApiCatalog.Models;

namespace ApiCatalog.Repository
{
    public interface IProductRepository :IRepository<Product>
    {
        IEnumerable<Product> GetProductsByPrice();
    }
}
