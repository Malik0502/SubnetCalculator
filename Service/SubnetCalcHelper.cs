using Service.Interfaces;

namespace Service
{
    public class SubnetCalcHelper : ISubnetHelper
    {
        private readonly IParser parser;

        public SubnetCalcHelper(IParser parser) 
        {
            this.parser = parser;
        }

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
            string[] splittedIpAdressBinary = SplitString(ipAdressBinary);
            string[] splittedSubnetmaskBinary = SplitString(subnetMaskBinary);


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
        /// Zählt die einsen in der Subnetzmaske um später die Anzahl an Stellen in der Netzwerkadresse zu überspringen
        /// </summary>
        /// <param name="subnetmaskBinary"></param>
        /// <returns></returns>
        public int CountOnesInSubnetMask(string subnetmaskBinary)
        {
            IParser parser = this.parser;
            char[] subnetmaskAsChars = parser.StringToCharArray(subnetmaskBinary);
            int counter = 0;
            foreach (var binaryNum in subnetmaskAsChars)
            {
                if (binaryNum == '1') counter++;
            }
            return counter;
        }

        /// <summary>
        /// Teilt einen String auf um die Punkte bei der IP wegzubekommen
        /// Prüft außerdem ob die strings das richtige Format haben um Fehler zu vermeiden
        /// </summary>
        /// <param name="ipAdress"></param>
        /// <returns></returns>
        public string[] SplitString(string ipAdress)
        {
            string[] splittedString = ipAdress.Split(".");

            if (splittedString.Length > 4)
            {
                Console.WriteLine("Die Ip-Adresse / Die Subnetzmaske hat das falsche Format");
                Console.WriteLine("Zu viele Oktette");
                Environment.Exit(0);
            }
            return splittedString;
        }

        /// <summary>
        /// Nimmt einen String und dreht diesen um
        /// </summary>
        /// <param name="stringToReverse"></param>
        /// <returns></returns>
        public string ReverseString(string stringToReverse)
        {
            return new string(stringToReverse.Reverse().ToArray());
        }

        /// <summary>
        /// Füllt den String mit Nullen um legitimes Binärformat zu bekommen
        /// </summary>
        /// <param name="stringToFillUp"></param>
        /// <returns></returns>
        public string FillUpWithZeros(string stringToFillUp)
        {
            for (int i = stringToFillUp.Count(); i < 8; i++)
            {
                stringToFillUp += 0;
            }
            return stringToFillUp;
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

        /// <summary>
        /// Erhöht einen Teil einer übergebenen Ip-Adresse
        /// <para>Ip wird in 4 gleichgroße Teile gespalten
        /// erhöht das oktet, dass startIndex / 8 als index hat
        /// Das Oktett wird in Dezimal umgewandelt, dann um 1 erhöht und dann wieder zurück addiert</para>
        /// </summary>
        /// <param name="iPAdress"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public string IncrementIpAdress(string iPAdress, int startIndex)
        {
            // Teilt den String auf
            string[] splittedString = SplitString(iPAdress);
            int partIndex = startIndex / 8;
            string result = "";

            // Konvertiert zu Dezimalzahl
            int partAsDecimal = Convert.ToInt32(splittedString[partIndex], 2);
            partAsDecimal++;

            // Wenn Zahl == 255,
            // dann wird Oktett, dass vor dem erhöhten Oktett steht um eins erhöht
            // partAsDecimal wird dann auf 0 gesetzt
            if (partAsDecimal >= 255)
            {
                partAsDecimal = 0;
                int partBeforeConverted = Convert.ToInt32(splittedString[partIndex - 1], 2);
                partBeforeConverted++;
                splittedString[partIndex - 1] = Convert.ToString(partBeforeConverted, 2).PadLeft(8, '0');
                splittedString[partIndex] = Convert.ToString(partAsDecimal, 2).PadLeft(8, '0');
            }
            else {
                splittedString[partIndex] = Convert.ToString(partAsDecimal, 2).PadLeft(8, '0');
            }

            // Setzt alles wieder zu einem String zusammen
            foreach (string item in splittedString)
            {
                result += (item != splittedString.Last()) ? item + "." : item;
            }
            
            return result;
        }

    }
}
