using Autofac;
using Autofac.Core;
using Service;
using Service.Interfaces;

namespace Subnetzrechner
{
    public class Program
    {
        static void Main(string[] args)
        {
            var container = DependencyContainer.BuildContainer();

            using (var scope = container.BeginLifetimeScope())
            {
                var writer = scope.Resolve<IMenu>();
                writer.StartMenu();
            }
        }
    }
}