using Service.Interfaces;

namespace Service
{
    public class SubnetCalcHelper : ISubnetHelper
    {
        /// <summary>
        /// Berechnet die Netzwerkadresse mithilfe einer AND Operation und gibt diese als Binärstring aus
        /// </summary>
        /// <param name="ipAdressBinary"></param>
        /// <param name="subnetMaskBinary"></param>
        /// <returns></returns>
        public string CalcNetworkAdressBinary(string ipAdressBinary, string subnetMaskBinary)
        {
            string result = "";
            string partialResult;

            // Teilt die strings in 4 gleichgroße Oktette um diese einzeln zu verwenden
            string[] splittedIpAdressBinary = ipAdressBinary.Split(".");
            string[] splittedSubnetmaskBinary = subnetMaskBinary.Split(".");


            // Itertiert durch jedes Oktett und teilt dieses in einzelne Chars ein um die Werte genau zu vergleichen
            for (int iterationCount = 0; iterationCount < splittedSubnetmaskBinary.Length; iterationCount++)
            {
                char[] singleNumsSubnet = splittedSubnetmaskBinary[iterationCount].ToCharArray();
                char[] singleNumsIP = splittedIpAdressBinary[iterationCount].ToCharArray();
                partialResult = "";

                // Iteriert durch alle Zahlen der Subnetzmaske und der Ip-Adresse in Binär und vergleicht ob beide ungleich 1 sind
                // Wenn ja wird eine 0 in den String hinzugefügt um die Netzwerkadresse zu bilden.
                // Wenn beide gleich 1 sind, wird eine 1 hinzugefügt
                for (int j = 0; j < singleNumsSubnet.Length; j++)
                {
                    partialResult += ((singleNumsSubnet[j] & singleNumsIP[j]) != '1') ? "0" : "1";
                }
                // Fügt die passenden Punkte hinzu um das Format der Ip-Adresse zu erfüllen
                result += (iterationCount != splittedSubnetmaskBinary.Length - 1) ? partialResult + ".": partialResult;
            }
            return result;
        }

        /// <summary>
        /// Berechnet den Logarithmus
        /// </summary>
        /// <param name="subnetAmount"></param>
        /// <returns></returns>
        public double CalcLogarithmus(int subnetAmount)
        {
            double amountOfSubnets = Convert.ToInt32(subnetAmount);
            amountOfSubnets = Math.Log2(amountOfSubnets);
            return amountOfSubnets;
        }

        /// <summary>
        /// Fügt die einzelnen Chars von einem Oktet String in ein Dictionary hinzu
        /// <para>Die Key Values sind vielfache von 2 die von 1 - 128 gehen
        /// Hilfsfunktion für Umwandlung Binär in Dezimal</para>
        /// </summary>
        /// <param name="charToConvert"></param>
        /// <param name="conversionTable"></param>
        public void AddConvertedCharToDic(char[] charToConvert, Dictionary<int, int> conversionTable)
        {
            int bitCounter = 128;
            int num;
            foreach (var numAsChar in charToConvert)
            {
                string charToString = numAsChar.ToString();
                num = int.Parse(charToString);
                conversionTable.Add(bitCounter, num);
                bitCounter /= 2;
            }
        }

        /// <summary>
        /// Checkt mit einer If-Schleife ob die Anzahl an Host größer oder kleiner 2^i ist
        /// <para>Wenn größer -> Schleife geht einen weiter 
        /// Wenn kleiner -> returned das Ergebnis von 2^i </para>
        /// <para>So groß muss das Subnetz sein um mit der geforderten Anzahl an Hosts klar zu kommen </para>
        /// </summary>
        /// <param name="hostAmount"></param>
        /// <returns></returns>
        public int GetMinNeededHosts(int hostAmount)
        {
            int neededHostAmount = 0;
            for (int i = 0; i < 32; i++)
            {
                if (hostAmount > Math.Pow(2, i)) continue;

                else return Convert.ToInt32(Math.Pow(2, i));

            }
            return neededHostAmount;
        }

        /// <summary>
        /// Die Schleife wird auf< 24 begrenzt, da ein Subnetz maximal 2^24 Hosts haben kann
        /// Es wird mit If geprüft ob ob die hostAmount größer oder kleiner als 2^i
        /// Wenn größer -> Schleife geht einen weiter
        /// Wenn kleiner -> returned das Ergebnis von 2^i
        /// Dies ist die Anzahl an Hostbits die du brauchst
        /// </summary>
        /// <param name="hostAmount"></param>
        /// <returns></returns>
        public int CalcNeededHostbits(int hostAmount)
        {
            for (int i = 0; i < 24; i++)
            {
                if (hostAmount > Math.Pow(2, i) - 2) continue;

                else return i;
            }
            return 0;
        }

        public string CalcSubnetmask(double hostbits)
        {
            // Berechnet die Anzahl an Einsen die nötig sind
            // Hostbits = 32 - Anzahl Nullen in Subnetzmaske
            int amountOfOnes = 32 - Convert.ToInt32(hostbits);
            string subnetmask = ""; 

            // Setzt die Subnetzmaske in Binär zusammen. Wenn I <= Anzahl Einsen dann wird eine 1 gesetzt,
            // Sonst eine 0
            // Dazwischen wird geprüft ob i Modulo 8 = 0 ergibt, dann ist das Ende eines Oktets erreicht. D.h. Punkt muss gesetzt werden
            for (int i = 1; i <= 32; i++)
            {
                subnetmask += (i <= amountOfOnes) ? (i % 8 == 0 && i != 32) ? "1" + "." : "1" : (i % 8 == 0 && i != 32) ? "0" + "." : "0";
            }
            return subnetmask;
        }

    }
}
