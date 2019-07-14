using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using ShoppingCartExcerise.Interfaces;
using ShoppingCartExcerise.Models;

namespace ShoppingCartExcerise.Tests
{
    [TestFixture]
    public class ShoppingCartItemCalculatorTests
    {
        private Mock<IConsoleWrapper> _stubConsoleWrapper;
        private Mock<IProductDiscounter> _stubProductDiscounter;
       
        private IShoppingCartItem _dummyShoppingCartItem;
        private ShoppingCartItemCalculator _sut;

        [SetUp]
        public void SetUp()
        {
            _stubConsoleWrapper = new Mock<IConsoleWrapper>();
            _stubProductDiscounter = new Mock<IProductDiscounter>();

            var productDiscounters = new List<IProductDiscounter> { _stubProductDiscounter.Object };

            _sut = new ShoppingCartItemCalculator(productDiscounters, _stubConsoleWrapper.Object);
        }

        [Test]
        [TestCase(1, 10.00, 10.00)]
        [TestCase(5, 10.00, 50.00)]
        [TestCase(0, 10.00, 00.00)]
        public void returns_correct_total_price(int quantity, double price, double expectedTotal)
        {
            _stubProductDiscounter.Setup(mpd => mpd.IsEligibleForDiscount(It.IsAny<int>())).Returns(false);

            _dummyShoppingCartItem = new ShoppingCartItem(new Product('A', price), quantity);

            var actualTotal = _sut.CalculateTotal(_dummyShoppingCartItem);

            Assert.AreEqual(expectedTotal, actualTotal);
        }

        [Test]
        [TestCase(1, 10.00, 2.00, 8.00)]
        [TestCase(5, 10.00, 10.00, 40.00)]
        [TestCase(0, 10.00, 5.00, 0.00)]
        public void returns_discounted_total_price_when_eligible_for_discount(int quantity, double price, double discount, double expectedTotal)
        {
            _stubProductDiscounter.Setup(mpd => mpd.SKU).Returns('A');
            _stubProductDiscounter.Setup(mpd => mpd.IsEligibleForDiscount(It.IsAny<int>())).Returns(true);
            _stubProductDiscounter.Setup(mpd => mpd.GetDiscount(It.IsAny<int>())).Returns(discount);

            _dummyShoppingCartItem = new ShoppingCartItem(new Product('A', price), quantity);

            var actualTotal = _sut.CalculateTotal(_dummyShoppingCartItem);

            Assert.AreEqual(expectedTotal, actualTotal);
        }
    }
}
