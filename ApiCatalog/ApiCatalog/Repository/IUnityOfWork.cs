namespace ApiCatalog.Repository
{
    public interface IUnityOfWork 
    {
        IProductRepository ProductRepository { get; }
        ICategoryRepository CategoryRepository { get; }

        void Commit();
    }
}
