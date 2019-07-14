using ShoppingCartExcerise.Interfaces;

namespace ShoppingCartExcerise
{
    public class ShoppingCartApplication : IShoppingCartApplication
    {
        private readonly IShoppingCartParser _shoppingCartParser;
        private readonly IConsoleWrapper _consoleWrapper;

        public ShoppingCartApplication(IShoppingCartParser shoppingCartParser, IConsoleWrapper consoleWrapper)
        {
            _shoppingCartParser = shoppingCartParser;
            _consoleWrapper = consoleWrapper;
        }

        public void Run(string[] args)
        {
            if (ValidateArgs(args))
            {
                try
                {
                    var shoppingCartString = args[0];

                    _consoleWrapper.WriteLine($"Shopping cart input: {shoppingCartString}");

                    var shoppingCart = _shoppingCartParser.Parse(shoppingCartString);

                    _consoleWrapper.WriteLine($"Shopping cart parsed succesfully - contains {shoppingCart.Items.Count} items");

                    var shoppingCartTotal = shoppingCart.CalculateTotal();

                    _consoleWrapper.WriteLine($"Grand total of cart: {shoppingCartTotal}");

                    _consoleWrapper.HandleExit();
                }
                catch (ShoppingCartParseException ex)
                {
                    _consoleWrapper.WriteLine($"Unable to parse shopping chart - {ex}");
                    _consoleWrapper.HandleExit();
                }
            }
        }

        private bool ValidateArgs(string[] args)
        {
            if(args.Length != 1)
            {
                _consoleWrapper.WriteLine($"Invalid argument length");
                _consoleWrapper.HandleExit();
                return false;
            }

            return true;
        }
    }
}
