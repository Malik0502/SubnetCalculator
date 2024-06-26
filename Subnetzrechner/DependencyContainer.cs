using Autofac;
using Service.Interfaces;
using Service;

namespace Subnetzrechner
{
    public static class DependencyContainer
    {
        public static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<BinaryParser>().As<IParser>();
            builder.RegisterType<SubnetCalcHelper>().As<ISubnetHelper>();
            builder.RegisterType<AsymSubnetCalculator>().As<IAsymCalculator>();
            builder.RegisterType<SubnetCalculator>().As<ICalculator>();
            builder.RegisterType<InformationHandler>().As<IInformation>();
            builder.RegisterType<Menu>().As<IMenu>();

            return builder.Build();
        }
    }
}
