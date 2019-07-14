using ShoppingCartExcerise.Interfaces;


namespace ShoppingCartExcerise.Discounters
{
    public abstract class BaseProductDiscounter : IProductDiscounter
    {
        public abstract char SKU { get; }

        protected abstract int DiscountQuantity { get; }

        protected abstract double DiscountAmount { get; }


        public double GetDiscount(int quantity)
        {
            return DiscountAmount * (quantity / DiscountQuantity);
        }

        public bool IsEligibleForDiscount(int quantity)
        {
            return quantity >= DiscountQuantity;
        }
    }
}
