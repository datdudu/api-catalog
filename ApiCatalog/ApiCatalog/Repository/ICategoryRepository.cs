using ApiCatalog.Models;
using ApiCatalog.Pagination;

namespace ApiCatalog.Repository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        PagedList<Category> GetCategories(CategoriesParameters categoriesParameters);
        IEnumerable<Category> GetCategoriesProducts();
    }
}
