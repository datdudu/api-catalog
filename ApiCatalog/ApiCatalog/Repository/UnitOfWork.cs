using ApiCatalog.Context;

namespace ApiCatalog.Repository
{
    public class UnitOfWork : IUnityOfWork
    {
        private ProductRepository _productRepo;
        private CategoryRepository _categoryRepo;
        public ApiCatalogContext _context;

        public UnitOfWork(ApiCatalogContext context)
        {
            _context = context;
        }

        public IProductRepository ProductRepository
        {
            get 
            {
                return _productRepo = _productRepo ?? new ProductRepository(_context);
            }
        }

        public ICategoryRepository CategoryRepository
        {
            get
            {
                return _categoryRepo = _categoryRepo ?? new CategoryRepository(_context);
            }
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
