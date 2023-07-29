using ApiCatalog.Context;
using ApiCatalog.Models;
using ApiCatalog.Pagination;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ApiCatalog.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApiCatalogContext context) : base(context)
        {
        }

        public async Task<PagedList<Category>> GetCategories(CategoriesParameters categoriesParameters)
        {
            return await PagedList<Category>.ToPagedList(Get().OrderBy(on => on.CategoryId), categoriesParameters.PageNumber, categoriesParameters.PageSize);
        }

        public async Task<IEnumerable<Category>> GetCategoriesProducts()
        {
            return await Get().Include(c => c.Products).ToListAsync();
        }
    }
}
