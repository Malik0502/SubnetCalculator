using Service;

namespace Subnetzrechner
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("IP-Adresse");
                string? ipAdressInput = Console.ReadLine();

                Console.WriteLine("Subnetzmaske:");
                string? subnetmaskInput = Console.ReadLine();

                Console.WriteLine("Anzahl Subnetze:");
                int subnetAmountInput = int.Parse(Console.ReadLine());

                SubnetEntity inputEntity = new(){
                    IPAdress = ipAdressInput,
                    SubnetMask = subnetmaskInput,
                    SubnetAmount = subnetAmountInput
                };

                new SubnetCalculator().ShowAvailableSubnets(inputEntity);
                
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Bitte überprüfen Sie ihre Werte");
                Console.WriteLine($"Fehlercode: {ex}");
            }
            
        }
    }
}