using Autofac;
using ShoppingCartExcerise.Interfaces;


namespace ShoppingCartExcerise
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ShoppingCartApplication>().As<IShoppingCartApplication>();
            builder.RegisterType<ShoppingCartParser>().As<IShoppingCartParser>();
            builder.RegisterType<ShoppingCartItemCalculator>().As<IShoppingCartItemCalculator>();
            builder.RegisterType<FileIo>().As<IFileIo>();
            builder.RegisterType<ConsoleWrapper>().As<IConsoleWrapper>();
            builder.RegisterType<ShoppingCart>().As<IShoppingCart>().SingleInstance();
            builder.RegisterType<ProductJsonRepository>().As<IProductRepository>().SingleInstance();

            builder.RegisterAssemblyTypes(ThisAssembly).AssignableTo<IProductDiscounter>().AsImplementedInterfaces();
        }
    }
}
