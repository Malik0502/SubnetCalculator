using Service;

namespace Subnetzrechner
{
    public class Program
    {
        static void Main(string[] args)
        {
            // 1. Schritt Ip-Adresse und Subnetzmaske in Binär - X
            // 2. Schritt Wie viele einsen in Maske (muss gespeichert werden) - X
            // 3. Schritt Vergleich Ip Adresse mit Subnetzmaske (Wenn Beide an gleicher Stelle 1 dann 1 sonst null hinschreiben) = Erstes Subnetz
            // 3. Schritt auch genannt als AND Operation von Ip Adresse und Subnetzmaske - X
            // 4. Schritt Wie oft passt die Zahl 2 in die Anzahl der Subnetze (Nur Hochzahl wichtig) bei 8 hätte man 2 hoch 3 (3 ist wichtig) - X
            // 5. Schritt Anzahl der einsen aus Maske in Binär-Ip überspringen und dann so viele Zahlen anschauen wie hochzahl ist (Wenn Maske = Acht einsen dann schaut man sich ab da die nächsten 3 Werte an)
            // 6. Schritt Diesen Bereich in Binär hochzählen (Bsp. .001 , 010, 011, 100, 101, 110, 111 etc.)
            // Schritt 5 und 6 in Netzwerkadresse!!!!!

            

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