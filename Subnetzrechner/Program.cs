using Service;
using System.Collections;

namespace Subnetzrechner
{
    public class Program
    {
        // Aufgaben für asymetrischen Subnetzberechner 
        // Gegeben: Ip-Adresse, Anzahl der Subnetze und Anzahl der Hosts in den Subnetzen (unterschiedlich) - X
        // 1. Das kleinst möglich passende finden (Bei 30 Hosts -> 32, Bei 63 Hosts -> 64 etc.) - X
        // Davon dann Log (32 -> Log2(32) = 5 etc.) - X
        // 2. Subnetzmaske herausfinden - X
        // 3. Mit den Informationen dann Berechnung durchführen.
        // Sollte nicht groß unterschiedlich sein nach den Schritten

        // Hab jetzt herausgefunden, dass ich es so machen kann wie ich dachte. Allerdings muss die Berechnung nach größe sortiert sein
        // Es fängt mit dem größten benötigten Netzwerk an. 
        // Die Ip-Adresse nach der letzten Adresse (Broadcastadresse) im Netzwerk wird für die AND Operation beim nächsten Subnetz benutzt
        // Die Schritte wie alles funktioniert sind in der Textdatei auf dem Desktop
        // Alle Rechnungen und Schritte sind dort durchgeführt.

        static void Main(string[] args)
        {
            new Menu().StartMenu();
        }
    }
}