using Moq;
using NUnit.Framework;
using ShoppingCartExcerise.Discounters;
using ShoppingCartExcerise.Interfaces;
using ShoppingCartExcerise.Models;
using System.Collections.Generic;

namespace ShoppingCartExcerise.Tests
{
    public class ShoppingCartTests
    {
        private Mock<IShoppingCartItemCalculator> _stubShoppingCartItemCalulator;
        private Mock<IConsoleWrapper> _stubConsoleWrapper;
        private ShoppingCart _sut;

        [SetUp]
        public void Setup()
        {
            _stubShoppingCartItemCalulator = new Mock<IShoppingCartItemCalculator>();
            _stubConsoleWrapper = new Mock<IConsoleWrapper>();

            _sut = new ShoppingCart(_stubShoppingCartItemCalulator.Object, _stubConsoleWrapper.Object);
        }

        [Test]
        public void given_no_existing_item_product_is_added_with_quantity_of_one()
        {
            _sut.AddProduct(new Product('A', 10.00));

            Assert.AreEqual(1, _sut.Items.Count);
            Assert.AreEqual(1, _sut.Items[0].Quantity);
        }

        [Test]
        public void multiple_same_products_added_returns_correct_quantity()
        {
            _sut.AddProduct(new Product('A', 10.00));
            _sut.AddProduct(new Product('A', 10.00));

            Assert.AreEqual(1, _sut.Items.Count);
            Assert.AreEqual(2, _sut.Items[0].Quantity);
        }

        [Test]
        public void calculates_total_correctly()
        {
            _sut.AddProduct(new Product('A', 20.00));
            _sut.AddProduct(new Product('A', 20.00));
            _sut.AddProduct(new Product('B', 10.00));

            _stubShoppingCartItemCalulator.Setup(s => s.CalculateTotal(It.Is<ShoppingCartItem>(sci => sci.Product.SKU == 'A'))).Returns(40.00);
            _stubShoppingCartItemCalulator.Setup(s => s.CalculateTotal(It.Is<ShoppingCartItem>(sci => sci.Product.SKU == 'B'))).Returns(10.00);

            var actualTotal = _sut.CalculateTotal();

            Assert.AreEqual(50.00, actualTotal);
        }

        [Test]
        [TestCaseSource("ShoppingCartTestCases")]
        public void calculates_correct_total_of_cart(List<IShoppingCartItem> items, double expectedTotal)
        {
            var productDiscounters = new List<IProductDiscounter> { new ProductADiscounter(), new ProductBDiscounter() };
            var shoppingCartItemCalculator = new ShoppingCartItemCalculator(productDiscounters, _stubConsoleWrapper.Object);

            var sut = new ShoppingCart(shoppingCartItemCalculator, _stubConsoleWrapper.Object);

            sut.Items = items;

            var actualTotal = sut.CalculateTotal();

            Assert.AreEqual(expectedTotal, actualTotal);
        }

        public static IEnumerable<TestCaseData> ShoppingCartTestCases
        {
            get
            {

                var productA = new Product('A', 50.00);
                var productB = new Product('B', 30.00);
                var productC = new Product('C', 20.00);
                var productD = new Product('D', 15.00);

                yield return new TestCaseData(new List<IShoppingCartItem>(), 0.00);
                yield return new TestCaseData(new List<IShoppingCartItem>
                                                    {
                                                        new ShoppingCartItem(productA, 1)
                                                    }, 50.00);
                yield return new TestCaseData(new List<IShoppingCartItem>
                                                    {
                                                        new ShoppingCartItem(productA, 1),
                                                        new ShoppingCartItem(productB, 1)
                                                     }, 80.00);
                yield return new TestCaseData(new List<IShoppingCartItem>
                                                    {
                                                        new ShoppingCartItem(productC, 1),
                                                        new ShoppingCartItem(productD, 1),
                                                        new ShoppingCartItem(productB, 1),
                                                        new ShoppingCartItem(productA, 1)
                                                     }, 115.00);
                yield return new TestCaseData(new List<IShoppingCartItem>
                                                    {
                                                        new ShoppingCartItem(productA, 2)
                                                     }, 100.00);
                yield return new TestCaseData(new List<IShoppingCartItem>
                                                    {
                                                        new ShoppingCartItem(productA, 3)
                                                     }, 130.00);
                yield return new TestCaseData(new List<IShoppingCartItem>
                                                    {
                                                        new ShoppingCartItem(productA, 3),
                                                        new ShoppingCartItem(productB, 2)
                                                     }, 175.00);
            }
        }

    }
}
