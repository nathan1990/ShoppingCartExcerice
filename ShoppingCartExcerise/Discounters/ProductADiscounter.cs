using System.Diagnostics.CodeAnalysis;

namespace ShoppingCartExcerise.Discounters
{
    [ExcludeFromCodeCoverage]
    public class ProductADiscounter : BaseProductDiscounter
    {
        public override char SKU => 'A';

        protected override int DiscountQuantity => 3;

        protected override double DiscountAmount => 20.00;
    }
}
