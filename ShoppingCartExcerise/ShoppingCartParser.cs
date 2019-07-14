using ShoppingCartExcerise.Interfaces;
using ShoppingCartExcerise.Models;
using System;
using System.Linq;

namespace ShoppingCartExcerise
{
    public class ShoppingCartParser : IShoppingCartParser
    {
        private readonly IProductRepository _productRepository;
        private readonly IShoppingCart _shoppingCart;

        public ShoppingCartParser(IProductRepository productRepository, IShoppingCart shoppingCart)
        {
            _productRepository = productRepository;
            _shoppingCart = shoppingCart;
        }

        public IShoppingCart Parse(string shoppingCartString)
        {
            try
            {
                _productRepository.Initialise();
                var skusToAdd = shoppingCartString.ToCharArray().Where(x => !char.IsWhiteSpace(x));

                foreach(var sku in skusToAdd)
                {
                    var product = _productRepository.GetProductBySku(sku);
                    if(product == null)
                    {
                        throw new ShoppingCartParseException(sku, "Unknown product");
                    }

                    _shoppingCart.AddProduct(product);
                }

                return _shoppingCart;
            }
            catch(ShoppingCartParseException scpe)
            {
                throw scpe;
            }
            catch(Exception ex)
            {
                throw new ShoppingCartParseException(ex.Message, ex.InnerException);
            }
        }
    }
}
