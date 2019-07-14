using ShoppingCartExcerise.Interfaces;
using System;
using System.Diagnostics.CodeAnalysis;

namespace ShoppingCartExcerise.Models
{
    [Serializable]
    [ExcludeFromCodeCoverage]
    public class Product : IProduct, IEquatable<Product>
    {
        public Product(char sku, double price)
        {
            SKU = sku;
            Price = price;
        }


        public char SKU { get; set; }

        public double Price { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Product);
        }

        public bool Equals(Product other)
        {
            return other != null &&
                   char.ToLowerInvariant(SKU) == char.ToLowerInvariant(other.SKU) &&
                   Price == other.Price;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(SKU, Price);
        }

        public override string ToString() => $"SKU: {SKU}, Price: {Price}";
    }
}
