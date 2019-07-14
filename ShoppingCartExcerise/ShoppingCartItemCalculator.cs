using ShoppingCartExcerise.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingCartExcerise
{
    public class ShoppingCartItemCalculator : IShoppingCartItemCalculator
    {
        private readonly IEnumerable<IProductDiscounter> _productDiscounters;
        private readonly IConsoleWrapper _consoleWrapper;
        private const double ZERO_COST = 00.00;

        public ShoppingCartItemCalculator(IEnumerable<IProductDiscounter> productDiscounters, IConsoleWrapper consoleWrapper)
        {
            _productDiscounters = productDiscounters;
            _consoleWrapper = consoleWrapper;
        }

        public double CalculateTotal(IShoppingCartItem shoppingCartItem)
        {
            var fullPrice = shoppingCartItem.Product.Price * shoppingCartItem.Quantity;

            if (fullPrice == ZERO_COST)
                return fullPrice;

            var discounter = _productDiscounters.SingleOrDefault(x => char.ToLowerInvariant(x.SKU) == char.ToLowerInvariant(shoppingCartItem.Product.SKU));

            if (discounter == null || !discounter.IsEligibleForDiscount(shoppingCartItem.Quantity))
                return fullPrice;

            var discount = discounter.GetDiscount(shoppingCartItem.Quantity);

            _consoleWrapper.WriteLine($"Applying discount for SKU: {shoppingCartItem.Product.SKU} for the amount of {discount}");

            return fullPrice - discount;
        }
    }
}
