using System.Diagnostics.CodeAnalysis;

namespace ShoppingCartExcerise.Discounters
{
    [ExcludeFromCodeCoverage]
    public class ProductBDiscounter : BaseProductDiscounter
    {
        public override char SKU => 'B';

        protected override int DiscountQuantity => 2;

        protected override double DiscountAmount => 15.00;
    }
}
