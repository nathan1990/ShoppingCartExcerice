namespace ShoppingCartExcerise.Interfaces
{
    public interface IShoppingCartParser
    {
        IShoppingCart Parse(string shoppingCartString);
    }
}
