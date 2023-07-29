using ApiCatalog.Context;
using ApiCatalog.Models;
using ApiCatalog.Pagination;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalog.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ApiCatalogContext context) : base(context) 
        {}

        public async Task<PagedList<Product>> GetProducts(ProductsParameters productsParameters)
        {
            return await PagedList<Product>.ToPagedList(Get().OrderBy(on => on.ProductId), productsParameters.PageNumber, productsParameters.PageSize);
        }

        public async Task<IEnumerable<Product>> GetProductsByPrice()
        {
            return await Get().OrderBy(c => c.Price).ToListAsync();
        }
    }
}
