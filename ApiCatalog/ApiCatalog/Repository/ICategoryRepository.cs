using ApiCatalog.Models;
using ApiCatalog.Pagination;

namespace ApiCatalog.Repository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<PagedList<Category>> GetCategories(CategoriesParameters categoriesParameters);
        Task<IEnumerable<Category>> GetCategoriesProducts();
    }
}
