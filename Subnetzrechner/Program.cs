using Service;

namespace Subnetzrechner
{
    public class Program
    {
        static void Main(string[] args)
        {
            // 1. Schritt Ip-Adresse und Subnetzmaske in Binär
            // 2. Schritt Wie viele einsen in Maske (muss gespeichert werden)
            // 3. Schritt Vergleich Ip Adresse mit Subnetzmaske (Wenn Beide an gleicher Stelle 1 dann 1 sonst null hinschreiben) = Erstes Subnetz
            // 4. Schritt Wie oft passt die Zahl 2 in die Anzahl der Subnetze (Nur Hochzahl wichtig) bei 8 hätte man 2 hoch 3 (3 ist wichtig)
            // 5. Schritt Anzahl der einsen aus Maske in Binär-Ip überspringen und dann so viele Zahlen anschauen wie hochzahl ist 
            // 6. Schritt Diesen Bereich in Binär hochzählen (Bsp. .001 , 010, 011, 100, 101, 110, 111 etc.)

            Console.WriteLine("IP-Adresse");
            string ipAdress = Console.ReadLine();

            Console.WriteLine("Subnetzmaske:");
            string subnetmask = Console.ReadLine();

            Console.WriteLine("Anzahl Subnetze:");
            int subnetAmount = int.Parse(Console.ReadLine());

            Console.ReadKey();
        }
    }
}