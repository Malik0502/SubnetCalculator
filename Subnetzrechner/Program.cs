using Service;
using System.Collections;

namespace Subnetzrechner
{
    public class Program
    {
        // Aufgaben für asymmetrischen Subnetzberechner 
        // Gegeben: Ip-Adresse, Anzahl der Subnetze und Anzahl der Hosts in den Subnetzen (unterschiedlich) - X
        // 1. Das kleinst möglich passende finden (Bei 30 Hosts -> 32, Bei 63 Hosts -> 64 etc.) - X
        // Davon dann Log (32 -> Log2(32) = 5 etc.) - X
        // 2. Subnetzmaske herausfinden
        // 3. Mit den Informationen dann Berechnung durchführen.
        // Sollte nicht groß unterschiedlich sein nach den Schritten

        static void Main(string[] args)
        {
            new Menu().StartMenu();
        }
    }
}