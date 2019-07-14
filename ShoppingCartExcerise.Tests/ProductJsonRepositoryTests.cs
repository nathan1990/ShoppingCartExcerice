using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using ShoppingCartExcerise.Interfaces;
using ShoppingCartExcerise.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace ShoppingCartExcerise.Tests
{
    [TestFixture]
    public class ProductJsonRepositoryTests
    {
        private ProductJsonRepository _sut;
        private Mock<IFileIo> _stubFileIo;
        private List<IProduct> _dummyProducts;

        [SetUp]
        public void Setup()
        {
            _dummyProducts = GetDummyProducts();
            var jsonProductString = JsonConvert.SerializeObject(_dummyProducts);

            _stubFileIo = new Mock<IFileIo>();
            _stubFileIo.Setup(sfi => sfi.Exists(It.IsAny<string>())).Returns(true);
            _stubFileIo.Setup(sfi => sfi.ReadAllText(It.IsAny<string>())).Returns(jsonProductString);

            _sut = new ProductJsonRepository(_stubFileIo.Object);
        }

        [Test]
        public void initialise_throws_filenotfoundexception_when_json_file_does_not_exist()
        {
            _stubFileIo.Invocations.Clear();

            _stubFileIo.Setup(sfi => sfi.Exists(It.IsAny<string>())).Returns(false);

            Assert.Throws<FileNotFoundException>(() => _sut.Initialise());
        }

        [Test]
        [TestCase('A')]
        [TestCase('C')]
        [TestCase('F')]
        [TestCase('a')]
        [TestCase('c')]
        [TestCase('f')]
        public void can_find_product_by_sku(char skuToFind)
        {
            _sut.Initialise();

            var foundProduct = _sut.GetProductBySku(skuToFind);

            Assert.IsNotNull(foundProduct);
        }

        [Test]
        public void returns_null_when_passed_unknown_sku()
        {
            _sut.Initialise();

            var foundProduct = _sut.GetProductBySku('Y');

            Assert.IsNull(foundProduct);
        }

        [Test]
        public void throws_invalidopertaionexception_when_same_sku_defined_twice()
        {
            _stubFileIo.Invocations.Clear();

            _dummyProducts.Add(new Product('A', 10.00));

            var jsonProductString = JsonConvert.SerializeObject(_dummyProducts);

            _stubFileIo.Setup(sfi => sfi.Exists(It.IsAny<string>())).Returns(true);
            _stubFileIo.Setup(sfi => sfi.ReadAllText(It.IsAny<string>())).Returns(jsonProductString);

            _sut.Initialise();

            Assert.Throws<InvalidOperationException>(() => _sut.GetProductBySku('A'));
        }

        private List<IProduct> GetDummyProducts()
        {
            return new List<IProduct>
            {
                new Product('A', 10.00),
                new Product('C', 12.00),
                new Product('F', 15.00)
            };
        }
    }
}
