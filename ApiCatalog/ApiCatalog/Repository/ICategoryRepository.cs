using ApiCatalog.Models;

namespace ApiCatalog.Repository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        IEnumerable<Category> GetCategoriesProducts();
    }
}
