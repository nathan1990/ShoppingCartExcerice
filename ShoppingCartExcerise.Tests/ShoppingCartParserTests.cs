using Moq;
using NUnit.Framework;
using ShoppingCartExcerise.Interfaces;
using ShoppingCartExcerise.Models;
using System;
using System.Collections.Generic;

namespace ShoppingCartExcerise.Tests
{
    [TestFixture]
    public class ShoppingCartParserTests
    {
        private Mock<IProductRepository> _mockProductRepository;
        private Mock<IShoppingCart> _mockShoppingCart;
        private IProduct _dummyProduct;
        private IShoppingCartParser _sut;

        [SetUp]
        public void SetUp()
        {
            _mockProductRepository = new Mock<IProductRepository>();
            _mockShoppingCart = new Mock<IShoppingCart>();

            _dummyProduct = new Product('A', 10.00);

            _mockProductRepository.Setup(mpr => mpr.GetProductBySku(It.IsAny<char>())).Returns(_dummyProduct);

            _sut = new ShoppingCartParser(_mockProductRepository.Object, _mockShoppingCart.Object);
        }

        [Test]
        public void initialises_product_repository()
        {
            _sut.Parse("A");

            _mockProductRepository.Verify(mpr => mpr.Initialise(), Times.Once);
        }

        [Test]
        [TestCaseSource("ShoppingCartTestCases")]
        public void gets_products_from_product_repository(string shoppingCartString, int expectedCallCount)
        {
            _sut.Parse(shoppingCartString);

            _mockProductRepository.Verify(mpr => mpr.GetProductBySku(It.IsAny<char>()), Times.Exactly(expectedCallCount));
        }

        [Test]
        [TestCaseSource("ShoppingCartTestCases")]
        public void adds_products_to_shopping_cart(string shoppingCartString, int expectedCallCount)
        {
            _sut.Parse(shoppingCartString);

            _mockShoppingCart.Verify(msc => msc.AddProduct(It.IsAny<IProduct>()), Times.Exactly(expectedCallCount));
        }

        [Test]
        public void throws_shoppingcartparse_exception_with_sku_when_unknown_product()
        {
            _mockProductRepository.Invocations.Clear();

            _mockProductRepository.Setup(mpr => mpr.GetProductBySku(It.IsAny<char>())).Returns((IProduct)null);

            var exception = Assert.Throws<ShoppingCartParseException>(() => _sut.Parse("A"));

            Assert.AreEqual('A', exception.SKU);
        }

        [Test]
        public void throws_shoppingcartparse_exception_with_no_sku_when_product_repository_throws_exception()
        {
            _mockProductRepository.Invocations.Clear();

            _mockProductRepository.Setup(mpr => mpr.Initialise()).Throws<InvalidOperationException>();

            var exception = Assert.Throws<ShoppingCartParseException>(() => _sut.Parse("A"));

            Assert.False(exception.SKU.HasValue);
        }

        public static IEnumerable<TestCaseData> ShoppingCartTestCases
        {
            get
            {
                yield return new TestCaseData("A", 1);
                yield return new TestCaseData("AB", 2);
                yield return new TestCaseData("A    B", 2);
                yield return new TestCaseData("AAABB", 5);
            }
        }
    }
}
