using System.Collections;
using System.Net;

namespace Service
{
    public class SubnetCalcHelper
    {
        // Berechnet die Netzwerkadresse mithilfe einer AND Operation und gibt diese als Binärstring aus
        public string CalcNetworkAdressBinary(string ipAdressBinary, string subnetMaskBinary)
        {
            string result = "";
            string partialResult = "";
            int tableCounter = 0;

            // Teilt die strings in 4 gleichgroße Oktette um diese einzeln zu verwenden
            string[] splittedIpAdressBinary = SplitString(ipAdressBinary);
            string[] splittedSubnetmaskBinary = SplitString(subnetMaskBinary);


            // Itertiert durch jedes Oktett und teilt dieses in einzelne Chars ein um die Werte genau zu vergleichen
            for (int i = 0; i < splittedSubnetmaskBinary.Length; i++)
            {
                char[] singleNumsSubnet = splittedSubnetmaskBinary[i].ToCharArray();
                char[] singleNumsIP = splittedIpAdressBinary[i].ToCharArray();
                partialResult = "";

                // Iteriert durch alle Zahlen der Subnetzmaske und der Ip-Adresse in Binär und vergleicht ob beide gleich 1 sind
                // Wenn ja wird eine 1 in den String hinzugefügt um die Netzwerkadresse zu bilden.
                for (int j = 0; j < singleNumsSubnet.Length; j++)
                {
                    if ((singleNumsSubnet[j] & singleNumsIP[j]) == '1') partialResult += "1";

                    else partialResult += "0";

                }
                // Fügt die passenden Punkte hinzu um das Format der Ip-Adresse zu erfüllen
                if (tableCounter == splittedSubnetmaskBinary.Length - 1) result += partialResult;

                else result += partialResult + ".";

                tableCounter++;
            }
            return result;
        }

        // Nimmt einen String als Eingabe und gibt einen String in Form von Binärcode zurück
        public string StringToBinary(string stringToConvert)
        {
            string[] splittedAdress = SplitString(stringToConvert);
            ArrayList AdressInBinary = new ArrayList();
            try
            {
                // Iteriert durch die 4 Oktette und nutzt Modulo um den Rest herauszufinden
                // Dieser ist dann eine Stelle des Binärcodes
                foreach (var octet in splittedAdress)
                {
                    int parsedOctet = int.Parse(octet);
                    string partialResult = "";

                    for (int i = parsedOctet; i > 0; i /= 2)
                    {
                        partialResult += parsedOctet % 2;
                        parsedOctet /= 2;
                    }

                    // Das unfertige Ergebnis wird mit den fehlenden Nullen aufgefüllt.
                    // Dann wird dieser umgedeht und in eine Liste hinzugefügt
                    // Dabei entsteht dann die richtige Zahl im Binärformat
                    partialResult = FillUpWithZeros(partialResult);
                    partialResult = ReverseString(partialResult);
                    AdressInBinary.Add(partialResult);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Die IP-Adresse hat das falsche Format: {ex}");
            }

            // Die einzelnen Oktete in Binär werden dann mit einem . zwischen den einzelnen Indexen als String zusammengefügt
            return string.Join(".", AdressInBinary.ToArray());
        }

        // Nimmt einen String im Binärformat und konvertiert diesen zu String im Dezimalformat
        public string BinaryToString(string binaryToConvert)
        {
            string[] splittedBinary = SplitString(binaryToConvert);
            int partialResult = 0;
            string result = "";
            int counter = 0;

            // Iteriert durch den splitted String Array 
            // Es wird ein leeres Dictionary erstellt um die Umrechnung durchzuführen
            // Das Dictionary wird mit den einzelnen Chars von einem Oktett gefüllt
            // Key = Vielfaches von 2 , 1 bis 128
            // Value = Binärzahl vom Oktett von vorne nach hinten
            foreach (string octet in splittedBinary)
            {
                partialResult = 0;
                Dictionary<int, int> conversionTable = new Dictionary<int, int>();

                char[] singleNumsFromOctet = octet.ToCharArray();
                AddConvertedCharToDic(singleNumsFromOctet, conversionTable);

                foreach (var item in conversionTable)
                {
                    partialResult += item.Key * item.Value;
                }

                // Fügt die passenden Punkte hinzu um das Format der Ip-Adresse zu erfüllen
                if (counter == splittedBinary.Length - 1) result += partialResult;

                else result += partialResult + ".";
                
                counter++;

            }
            return result;
        }

        // Zählt die einsen in der Subnetzmaske um später die Anzahl an Stellen in der Netzwerkadresse zu überspringen
        public int CountOnesInSubnetMask(string subnetmaskBinary)
        {
            char[] subnetmaskAsChars = StringToCharArray(subnetmaskBinary);
            int counter = 0;
            foreach (var binaryNum in subnetmaskAsChars)
            {
                if (binaryNum == '1') counter++;
            }
            return counter;
        }

        // Konvertiert einen string zu einem Array aus Chars
        public char[] StringToCharArray(string StringToConvert)
        {
            string[] splittedString = SplitString(StringToConvert);
            char[] chars = new char[32];

            for (int i = 0; i < splittedString.Length; i++)
            {
                char[] splittedStringAsCharArray = splittedString[i].ToCharArray();
                splittedStringAsCharArray.CopyTo(chars, splittedStringAsCharArray.Length * i);
            }

            return chars;
        }

        // Teilt einen String auf um die Punkte bei der IP wegzubekommen
        // Prüft außerdem ob die strings das richtige Format haben um Fehler zu vermeiden
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

        // Nimmt einen String und dreht diesen um
        public string ReverseString(string stringToReverse)
        {
            return new string(stringToReverse.Reverse().ToArray());
        }

        // Füllt den String mit Nullen um legitimes Binärformat zu bekommen
        public string FillUpWithZeros(string stringToFillUp)
        {
            for (int i = stringToFillUp.Count(); i < 8; i++)
            {
                stringToFillUp += 0;
            }
            return stringToFillUp;
        }

        // Berechnet den Logarithmus
        public double CalcLogarithmus(int subnetAmount)
        {
            double amountOfSubnets = Convert.ToInt32(subnetAmount);
            amountOfSubnets = Math.Log2(amountOfSubnets);
            return amountOfSubnets;
        }

        // Fügt die einzelnen Chars von einem Oktet String in ein Dictionary hinzu
        // Die Key Values sind vielfache von 2 die von 1 - 128 gehen
        // Hilfsfunktion für Umwandlung Binär in Dezimal
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

        // Checkt mit einer If-Schleife ob die Anzahl an Host größer oder kleiner 2^i ist
        // Wenn größer -> Schleife geht einen weiter
        // Wenn kleiner -> returned das Ergebnis von 2^i
        // So groß muss das Subnetz sein um mit der geforderten Anzahl an Hosts klar zu kommen
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

        // Die Schleife wird auf < 24 begrenzt, da ein Subnetz maximal 2^24 Hosts haben kann
        // Es wird mit If geprüft ob ob die hostAmount größer oder kleiner als 2^i
        // Wenn größer -> Schleife geht einen weiter
        // Wenn kleiner -> returned das Ergebnis von 2^i
        // Dies ist die Anzahl an Hostbits die du brauchst
        // Notiz: Könnte später vllt mit GetMinNeededHosts zusammengefügt werden. Jedenfalls optimiert werden um aus zwei eine Funktion zu machen
        public int CalcNeededHostbits(int hostAmount)
        {
            for (int i = 0; i < 24; i++)
            {
                if (hostAmount > Math.Pow(2, i) - 2) continue;

                else return i;
            }
            return 0;
        }

        // Erhöht einen Teil einer übergebenen Ip-Adresse
        // Ip wird in 4 gleichgroße Teile gespalten
        // erhöht das oktet, dass startIndex / 8 als index hat
        // Das Oktett wird in Dezimal umgewandelt, dann um 1 erhöht und dann wieder zurück addiert
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
                if (item != splittedString.Last()) result += item + ".";

                else result += item;
            }
            
            return result;
        }

    }
}
