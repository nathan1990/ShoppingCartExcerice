namespace ShoppingCartExcerise.Interfaces
{
    public interface IProductDiscounter
    {
        char SKU { get; }

        double GetDiscount(int quantity);

        bool IsEligibleForDiscount(int quantity);
    }
}
