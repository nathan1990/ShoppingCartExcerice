using System.IO;

namespace ShoppingCartExcerise.Interfaces
{
    public interface IFileIo
    {
        bool Exists(string path);
        string ReadAllText(string path);
    }
}
