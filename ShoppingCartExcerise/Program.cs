using Autofac;
using ShoppingCartExcerise.Interfaces;

namespace ShoppingCartExcerise
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<AutofacModule>();

            var container = builder.Build();

            var shoppingCartApplication = container.Resolve<IShoppingCartApplication>();

            shoppingCartApplication.Run(args);
        }
    }
}
