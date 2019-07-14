using Newtonsoft.Json;
using ShoppingCartExcerise.Interfaces;
using ShoppingCartExcerise.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ShoppingCartExcerise
{
    public class ProductJsonRepository : IProductRepository
    {
        private readonly IFileIo _fileIo;
        private IList<Product> _products;
        private const string JSON_FILE_NAME = "products.json";

        public ProductJsonRepository(IFileIo fileIo)
        {
            _fileIo = fileIo;
        }

        public void Initialise()
        {
            if (!_fileIo.Exists(JSON_FILE_NAME))
            {
                throw new FileNotFoundException("Unable to find product json file", JSON_FILE_NAME);
            }

            _products = JsonConvert.DeserializeObject<List<Product>>(_fileIo.ReadAllText(JSON_FILE_NAME));
        }

        public IProduct GetProductBySku(char skuToFind)
        {
            return _products.SingleOrDefault(p => char.ToLowerInvariant(p.SKU) == char.ToLowerInvariant(skuToFind));
        }
    }
}
