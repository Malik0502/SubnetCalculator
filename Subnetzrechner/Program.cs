using Service;

namespace Subnetzrechner
{
    public class Program
    {
        // Aufgaben für asymmetrischen Subnetzberechner 
        // Gegeben: Ip-Adresse, Anzahl der Subnetze und Anzahl der Hosts in den Subnetzen (unterschiedlich)
        // 1. Das kleinst möglich passende finden (Bei 30 Hosts -> 32, Bei 63 Hosts -> 64 etc.)
        // Davon dann Log (32 -> Log2(32) = 5 etc.)
        // 2. Subnetzmaske herausfinden
        // 3. Mit den Informationen dann Berechnung durchführen.
        // Sollte nicht groß unterschiedlich sein nach den Schritten

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