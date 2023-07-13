using ApiCatalog.Context;
using ApiCatalog.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ApiCatalog.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApiCatalogContext context) : base(context)
        {
        }

        public IEnumerable<Category> GetCategoriesProducts()
        {
            return Get().Include(c => c.Products);
        }
    }
}
