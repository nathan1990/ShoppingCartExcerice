namespace ShoppingCartExcerise.Interfaces
{
    public interface IShoppingCartItem
    {
        IProduct Product { get; set; }
        int Quantity { get; set; }
    }
}
