using System;
using System.Diagnostics.CodeAnalysis;
using ShoppingCartExcerise.Interfaces;

namespace ShoppingCartExcerise
{
    [ExcludeFromCodeCoverage]
    public class ConsoleWrapper : IConsoleWrapper
    {
        public void WriteLine(string value)
        {
            Console.WriteLine(value);
        }

        public void HandleExit()
        {
            WriteLine("Press any key to exit...");
            Console.ReadKey(true);
        }
    }
}
