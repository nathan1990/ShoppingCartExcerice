namespace ShoppingCartExcerise.Interfaces
{
    public interface IProductRepository
    {
        void Initialise();
        IProduct GetProductBySku(char skuToFind);
    }
}
