using System.Collections.Generic;

namespace ShoppingCartExcerise.Interfaces
{
    public interface IShoppingCart
    {
        IList<IShoppingCartItem> Items { get; }

        void AddProduct(IProduct product);

        double CalculateTotal();
    }
}
