using Moq;
using NUnit.Framework;
using ShoppingCartExcerise.Interfaces;
using System.Collections.Generic;

namespace ShoppingCartExcerise.Tests
{
    [TestFixture]
    public class ShoppingCartApplicationTests
    {
        private Mock<IConsoleWrapper> _mockConsoleWrapper;
        private Mock<IShoppingCartParser> _mockShoppingCartParser;
        private Mock<IShoppingCart> _mockShoppingCart;
        private ShoppingCartApplication _sut;

        [SetUp]
        public void SetUp()
        {
            _mockConsoleWrapper = new Mock<IConsoleWrapper>();
            _mockShoppingCartParser = new Mock<IShoppingCartParser>();
            _mockShoppingCart = new Mock<IShoppingCart>();

            _mockShoppingCart.Setup(msc => msc.Items).Returns(new List<IShoppingCartItem>());
            _mockShoppingCartParser.Setup(mscp => mscp.Parse(It.IsAny<string>())).Returns(_mockShoppingCart.Object);
            _sut = new ShoppingCartApplication(_mockShoppingCartParser.Object, _mockConsoleWrapper.Object);
        }

        [Test]
        [TestCase(0)]
        [TestCase(2)]
        public void exits_when_invalid_arguments(int argCount)
        {
            var args = new string[argCount];

            _sut.Run(args);

            _mockConsoleWrapper.Verify(mcw => mcw.HandleExit(), Times.Once);
            _mockShoppingCartParser.Verify(mscp => mscp.Parse(It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void calls_shopping_cart_parser()
        {
            _sut.Run(new string[] { "A" });

            _mockShoppingCartParser.Verify(mscp => mscp.Parse(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void handles_shoppingcartparse_exception()
        {
            _mockShoppingCartParser.Invocations.Clear();

            _mockShoppingCartParser.Setup(mscp => mscp.Parse(It.IsAny<string>())).Throws<ShoppingCartParseException>();

            _sut.Run(new string[] { "A" });

            _mockShoppingCart.Verify(msc => msc.CalculateTotal(), Times.Never);
            _mockConsoleWrapper.Verify(mcw => mcw.HandleExit(), Times.Once);
        }

        [Test]
        public void calls_shopping_cart_to_get_total()
        {
            _sut.Run(new string[] { "A" });

            _mockShoppingCart.Verify(msc => msc.CalculateTotal(), Times.Once);
        }

        [Test]
        public void calls_handle_exit_when_complete()
        {
            _sut.Run(new string[] { "A" });

            _mockConsoleWrapper.Verify(mcw => mcw.HandleExit(), Times.Once);
        }
    }
}
