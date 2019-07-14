using ShoppingCartExcerise.Interfaces;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace ShoppingCartExcerise
{
    [ExcludeFromCodeCoverage]
    public class FileIo : IFileIo
    {
        public bool Exists(string path)
        {
            return File.Exists(path);
        }

        public string ReadAllText(string path)
        {
            return File.ReadAllText(path);
        }
    }
}
