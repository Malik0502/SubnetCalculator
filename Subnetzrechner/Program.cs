using Service;
using Service.Interfaces;

namespace Subnetzrechner
{
    public class Program
    {
        static void Main(string[] args)
        {
            BinaryParser parser = new BinaryParser();
            SubnetCalcHelper subnetHelper = new SubnetCalcHelper(parser);
            AsymSubnetCalculator asymCalculator = new AsymSubnetCalculator(subnetHelper, parser);
            SubnetCalculator calculator = new SubnetCalculator(parser, subnetHelper);
            InformationHandler infoHandler = new InformationHandler(parser, asymCalculator, calculator);
            

            new Menu(infoHandler, parser).StartMenu();
        }
    }
}