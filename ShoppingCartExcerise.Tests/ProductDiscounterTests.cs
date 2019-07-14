using NUnit.Framework;
using ShoppingCartExcerise.Discounters;
using ShoppingCartExcerise.Interfaces;

namespace ShoppingCartExcerise.Tests
{
    [TestFixture]
    public class ProductDiscounterTests
    {
        private IProductDiscounter _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new TestProductDiscounter();
        }

        [Test]
        [TestCase(1, false)]
        [TestCase(2, true)]
        [TestCase(3, true)]
        public void calculates_discount_eligiblity_correctly(int quantity, bool expected)
        {
            var actual = _sut.IsEligibleForDiscount(quantity);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase(1, 0.00)]
        [TestCase(2, 10.00)]
        [TestCase(3, 10.00)]
        [TestCase(4, 20.00)]
        public void calculates_discount_amount_correctly(int quantity, double expectedAmount)
        {
            var acutalAmount = _sut.GetDiscount(quantity);

            Assert.AreEqual(expectedAmount, acutalAmount);
        }
    }

    public class TestProductDiscounter : BaseProductDiscounter
    {
        public override char SKU => 'A';

        protected override int DiscountQuantity => 2;

        protected override double DiscountAmount => 10.00;
    }
}
