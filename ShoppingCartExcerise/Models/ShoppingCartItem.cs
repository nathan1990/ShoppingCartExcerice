using ShoppingCartExcerise.Interfaces;
using System.Diagnostics.CodeAnalysis;

namespace ShoppingCartExcerise.Models
{
    [ExcludeFromCodeCoverage]
    public class ShoppingCartItem : IShoppingCartItem
    {
        public ShoppingCartItem (IProduct product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }

        public IProduct Product { get; set; }
        public int Quantity { get; set; }

        public override string ToString() => $"Product : {Product}, Quantity: {Quantity}";
    }
}
