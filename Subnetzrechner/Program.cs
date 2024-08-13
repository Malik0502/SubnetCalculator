using Autofac;
using Autofac.Core;
using Service;
using Autofac;
using Service.Interfaces;

namespace Subnetzrechner
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Das ist der Weg um Dependency Injection von Hand aus aufzulösen
            //BinaryParser parser = new BinaryParser();
            //SubnetCalcHelper subnetHelper = new SubnetCalcHelper(parser);
            //AsymSubnetCalculator asymCalculator = new AsymSubnetCalculator(subnetHelper, parser);
            //SubnetCalculator calculator = new SubnetCalculator(parser, subnetHelper);
            //InformationHandler infoHandler = new InformationHandler(parser, asymCalculator, calculator);

            //new Menu(infoHandler, parser).StartMenu();

            // Dies ist der Weg um die Dependency Injection über einen Builder und einen Container auflösen zu lassen

            var builder = new ContainerBuilder();

            builder.RegisterType<BinaryParser>().As<IParser>();
            builder.RegisterType<SubnetCalcHelper>().As<ISubnetHelper>();
            builder.RegisterType<AsymSubnetCalculator>().As<IAsymCalculator>();
            builder.RegisterType<SubnetCalculator>().As<ICalculator>();
            builder.RegisterType<InformationHandler>().As<IInformation>();
            builder.RegisterType<Menu>().As<IMenu>();
            
            var container = builder.Build();

            using (var scope = container.BeginLifetimeScope())
            {
                var menu = scope.Resolve<IMenu>();
                menu.StartMenu();
            }


            new Menu(infoHandler, parser).StartMenu();
        }
    }
}