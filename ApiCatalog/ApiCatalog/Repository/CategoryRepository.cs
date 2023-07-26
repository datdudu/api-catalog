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

        public PagedList<Category> GetCategories(CategoriesParameters categoriesParameters)
        {
            return PagedList<Category>.ToPagedList(Get().OrderBy(on => on.CategoryId), categoriesParameters.PageNumber, categoriesParameters.PageSize);
        }

        public IEnumerable<Category> GetCategoriesProducts()
        {
            return Get().Include(c => c.Products);
        }
    }
}
