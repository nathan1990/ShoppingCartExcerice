using NUnit.Framework;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ShoppingCartExcerise.Tests
{
    [TestFixture]
    public class ShoppingCartParseExceptionTests
    {
        private ShoppingCartParseException _exceptionUnderTest;
        private const char DUMMY_SKU = 'Z';
        private const string DUMMY_MESSAGE = "Test Exception Message";

        [SetUp]
        public void Setup()
        {
            _exceptionUnderTest = new ShoppingCartParseException(DUMMY_SKU, DUMMY_MESSAGE);
        }

        [Test]
        public void sets_properties_on_creation()
        {
            Assert.AreEqual(DUMMY_SKU, _exceptionUnderTest.SKU);
            Assert.AreEqual(DUMMY_MESSAGE, _exceptionUnderTest.Message);
        }

        [Test]
        public void sku_property_persists_after_round_trip_serialization()
        {
            BinaryFormatter bf = new BinaryFormatter();
            using(MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, _exceptionUnderTest);

                ms.Seek(0, 0);

                _exceptionUnderTest = bf.Deserialize(ms) as ShoppingCartParseException;
            }

            Assert.AreEqual(DUMMY_SKU, _exceptionUnderTest.SKU);
        }
    }
}
