using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.Numerics;

namespace Service
{
    public class SubnetCalculator
    {
        // Zeigt alle Subnetze an, die berechnet werden in Dezimalformat an
        public void ShowAvailableSubnets(SubnetEntity inputEntity)
        {
            if (ValidateUserInput(inputEntity))
            {
                foreach (string subnet in CalcAvailableSubnets(inputEntity))
                {
                    string subnetAsDecimal = BinaryToString(subnet);
                    Console.WriteLine(subnetAsDecimal);
                }
            }
            else
            {
                Console.WriteLine("Ihre Eingaben haben das falsche Format");
            }
        }

        public void ShowAvailableAsymSubnets(AsymSubnetEntity inputEntity)
        {
                foreach (string subnet in CalcAvailableAsymSubnets(inputEntity))
                {
                    string asyncSubnetAsDecimal = BinaryToString(subnet);
                    Console.WriteLine(asyncSubnetAsDecimal);
                }
        }

        public List<string> CalcAvailableAsymSubnets(AsymSubnetEntity inputEntity)
        {

            return null;
        }

        // Berechnet mögliche Subnetze mithilfe einer Ip-Adresse, einer Subnetzmaske und der Anzahl an gewünschten Teilnetzen
        public List<string> CalcAvailableSubnets(SubnetEntity inputEntity)
        {
            string? ipAdressBinary = StringToBinaryString(inputEntity.IPAdress);
            string? subnetmaskBinary = StringToBinaryString(inputEntity.SubnetMask);

            int subnetAmount = inputEntity.SubnetAmount;

            string? networkAdressBinary = CalcNetworkAdressBinary(ipAdressBinary, subnetmaskBinary);
            double logOfAmountSubnets = Math.Ceiling(CalcLogarithmus(inputEntity.SubnetAmount));
            int amountOnesInMask = CountOnesInSubnetMask(subnetmaskBinary);

            char[] singleNumsNetworkAdress = StringToCharArray(networkAdressBinary);
            string subnet = "";

            List<string> subnets = new();

            // Baut den ersten Teil der Subnet Adresse zusammen. So weit wie es Einsen in der Subnetzmaske gibt
            for (int subnetCount = 0; subnetCount < subnetAmount; subnetCount++)
            {
                int posCounter;
                for (posCounter = 1; posCounter - 1 < amountOnesInMask; posCounter++)
                {
                    if(posCounter % 8 == 0){
                        subnet += singleNumsNetworkAdress[posCounter - 1] + ".";
                    }
                    else{
                        subnet += singleNumsNetworkAdress[posCounter - 1];
                    }
                }
                
                // Baut den Teil zusammen, der geändert werden muss und zählt diesen Hoch
                // Nimmt den subnetCount und wandelt diesen in Binär um. 
                // Dieser wird dann mit Nullen aufgefüllt und nach rechts alignt bis der String so lang idt wie die Anzahl der benötigten Netze
                string binary = Convert.ToString(subnetCount, 2).PadLeft(Convert.ToInt32(logOfAmountSubnets), '0');
                char[] binaryAsChar = binary.ToCharArray();

                foreach (var binaryNum in binaryAsChar)
                {
                    if ((singleNumsNetworkAdress.Length - posCounter) % 8 == 0)
                    {
                        subnet += binaryNum + ".";
                    }
                    else
                    {
                        subnet += binaryNum;
                    }
                    posCounter++;
                }

                // Fügt die restlichen Nummer an die Ip-Adresse ran.
                for (int binaryRest = amountOnesInMask + Convert.ToInt32(logOfAmountSubnets); binaryRest < singleNumsNetworkAdress.Length; binaryRest++)
                {
                    if(binaryRest % 8 == 0){
                        subnet += singleNumsNetworkAdress[binaryRest] + ".";
                    }
                    else{
                        subnet += singleNumsNetworkAdress[binaryRest];
                    }
                }
                subnets.Add(subnet);
                subnet = "";
            }
            return subnets;
        }

        // Zählt die einsen in der Subnetzmaske um später die Anzahl an Stellen in der Netzwerkadresse zu überspringen
        private int CountOnesInSubnetMask(string subnetmaskBinary)
        {
            char[] subnetmaskAsChars = StringToCharArray(subnetmaskBinary);
            int counter = 0;
            foreach (var binaryNum in subnetmaskAsChars)
            {
                if (binaryNum == '1') {
                    counter++;
                }
            }
            return counter;
        }

        // Berechnet die Netzwerkadresse mithilfe einer AND Operation und gibt diese als Binärstring aus
        public string CalcNetworkAdressBinary(string ipAdressBinary, string subnetMaskBinary)
        {
            string resultOfAND = "";
            string partialResultOfAND = "";
            int tableCounter = 0;

            // Teilt die strings in 4 gleichgroße Oktette um diese einzeln zu verwenden
            string[] splittedIpAdressBinary = SplitString(ipAdressBinary);
            string[] splittedSubnetmaskBinary = SplitString(subnetMaskBinary);


            // Itertiert durch jedes Oktett und teilt dieses in einzelne Chars ein um die Werte genau zu vergleichen
            for(int i = 0; i < splittedSubnetmaskBinary.Length; i++)
            {
                char[] singleNumsSubnet = splittedSubnetmaskBinary[i].ToCharArray();
                char[] singleNumsIP = splittedIpAdressBinary[i].ToCharArray();
                partialResultOfAND = "";

                // Iteriert durch alle Zahlen der Subnetzmaske und der Ip-Adresse in Binär und vergleicht ob beide gleich 1 sind
                // Wenn ja wird eine 1 in den String hinzugefügt um die Netzwerkadresse zu bilden.
                for(int j = 0; j < singleNumsSubnet.Length; j++)
                {
                    if ((singleNumsSubnet[j] & singleNumsIP[j]) == '1')
                    {
                        partialResultOfAND += "1";
                    }
                    else
                    {
                        partialResultOfAND += "0";
                    }
                }
                // Fügt die passenden Punkte hinzu um das Format der Ip-Adresse zu erfüllen
                if (tableCounter == splittedSubnetmaskBinary.Length - 1)
                {
                    resultOfAND += partialResultOfAND;
                }
                else
                {
                    resultOfAND += partialResultOfAND + ".";
                }
                tableCounter++;
            }
            return resultOfAND;
        }

        // Berechnet den Logarithmus
        public double CalcLogarithmus(int subnetAmount){
            double amountOfSubnets = Convert.ToInt32(subnetAmount);
            amountOfSubnets = Math.Log2(amountOfSubnets);
            return amountOfSubnets;
        }

        // Nimmt einen String als Eingabe und gibt einen String in Form von Binärcode zurück
        public string StringToBinaryString(string stringToConvert)
        {
            string[] splittedAdress = SplitString(stringToConvert);
            ArrayList AdressInBinary = new ArrayList();
            try
            {
                // Iteriert durch die 4 Oktette und nutzt Modulo um den Rest herauszufinden.
                // Dieser ist dann eine Stelle des Binärcodes
                foreach (var octet in splittedAdress)
                {
                    int parsedOctet = int.Parse(octet);
                    string partialResult = "";

                    for (int i = parsedOctet; i > 0; i/=2){
                        partialResult += parsedOctet % 2;
                        parsedOctet /= 2;
                    }

                    // Das unfertige Ergebnis wird mit den fehlenden Nullen aufgefüllt.
                    // Dann wird dieser umgedeht und in eine Liste hinzugefügt
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
            int resultOfOctetInDecimal = 0;
            string result = "";
            int counter = 0;

            // Iteriert durch den splitted String Array 
            // Es wird ein leeres Dictionary erstellt um die Umrechnung durchzuführen
            // Das Dictionary wird mit den einzelnen Chars von einem Oktett gefüllt
            // Key = Vielfaches von 2 , 1 bis 128
            // Value = Binärzahl vom Oktett von vorne nach hinten
            foreach (string octet in splittedBinary)
            {
                resultOfOctetInDecimal = 0;
                Dictionary<int, int> conversionTable = new Dictionary<int, int>();
                
                char[] singleNumsFromOctet = octet.ToCharArray();
                AddConvertedCharToDic(singleNumsFromOctet, conversionTable);

                foreach (var item in conversionTable)
                {
                    resultOfOctetInDecimal += item.Key * item.Value;
                }

                // Fügt die passenden Punkte hinzu um das Format der Ip-Adresse zu erfüllen
                if(counter == splittedBinary.Length - 1)
                {
                    result += resultOfOctetInDecimal; 
                }
                else{
                    result += resultOfOctetInDecimal + ".";
                }
                counter++;
                
            }
            return result;
        }

        // Teilt einen String auf um die Punkte bei der IP wegzubekommen
        // Prüft außerdem ob die strings das richtige Format haben um Fehler zu vermeiden.
        private string[] SplitString(string ipAdress){
            string[] splittedString = ipAdress.Split(".");

            if(splittedString.Length > 4){
                Console.WriteLine("Die Ip-Adresse / Die Subnetzmaske hat das falsche Format");
                Console.WriteLine("Zu viele Oktette");
                Environment.Exit(0);
            } 
            return splittedString;
        } 

        // Nimmt einen String und dreht diesen um
        private string ReverseString(string stringToReverse){
            return new string(stringToReverse.Reverse().ToArray());
        }

        // Füllt den String mit Nullen um Binärformat zu bekommen
        private string FillUpWithZeros(string stringToFillUp)
        {
            for(int i = stringToFillUp.Count(); i < 8; i++)
            {
                stringToFillUp += 0;
            }

            return stringToFillUp;
        }

        // Fügt die einzelnen Chars von einem Oktet String in ein Dictionary hinzu
        // Die Key Values sind vielfache von 2 die von 1 - 128 gehen
        // Hilfsfunktion für Binär in Dezimal
        private void AddConvertedCharToDic(char[] charToConvert, Dictionary<int, int> conversionTable){
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

        // Prüft ob die Eingaben des Nutzers die geeignete Länge für eine Mögliche Ip Adresse bzw. Subnetzmaske hat
        // Eine Ip-Adresse / Subnetzmaske mitsamt Punkten kann mindestens 7 und maximal 15 Zeichen beinhalten
        // Ebenfalls wenn eines der beiden 0 ist wird false ausgegeben
        private bool ValidateUserInput(SubnetEntity inputEntity)
        {
            int inputIpAdressLength = inputEntity.IPAdress.Length;
            int inputSubnetmaskLength = inputEntity.SubnetMask.Length;

            if (inputIpAdressLength > 15 || inputIpAdressLength < 7 || inputSubnetmaskLength > 15 || inputSubnetmaskLength < 7 || inputIpAdressLength == 0 || inputSubnetmaskLength == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}