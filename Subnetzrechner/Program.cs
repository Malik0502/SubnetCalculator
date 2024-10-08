﻿using Autofac;
using Service;
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
            // Genaue Implementierung in DependencyContainer.cs

            var container = DependencyContainer.BuildContainer();

            using (var scope = container.BeginLifetimeScope())
            {
                var menu = scope.Resolve<IMenu>();
                menu.StartMenu();
            }
        }
    }
}