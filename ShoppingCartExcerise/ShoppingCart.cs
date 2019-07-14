using ShoppingCartExcerise.Interfaces;
using ShoppingCartExcerise.Models;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingCartExcerise
{
    public class ShoppingCart : IShoppingCart
    {
        private readonly IConsoleWrapper _consoleWrapper;
        private readonly IShoppingCartItemCalculator _shoppingCartItemCalculator;

        public ShoppingCart(IShoppingCartItemCalculator shoppingCartItemCalculator, IConsoleWrapper consoleWrapper)
        {
            _shoppingCartItemCalculator = shoppingCartItemCalculator;
            _consoleWrapper = consoleWrapper;
            Items = new List<IShoppingCartItem>();
        }

        public IList<IShoppingCartItem> Items { get; set; }

        public void AddProduct(IProduct product)
        {
            var existingItem = Items.SingleOrDefault(x => x.Product.Equals(product));

            if (existingItem != null)
            {
                existingItem.Quantity = ++existingItem.Quantity;
            }
            else
            {
                Items.Add(new ShoppingCartItem(product, 1));
            }
        }

        public double CalculateTotal()
        {
            double total = 0.0;
            foreach(var item in Items)
            {
                _consoleWrapper.WriteLine($"Caclulating total for item {item}");
                total += _shoppingCartItemCalculator.CalculateTotal(item);
                _consoleWrapper.WriteLine($"Running total: {total}");
            }

            return total;
        }
    }
}
