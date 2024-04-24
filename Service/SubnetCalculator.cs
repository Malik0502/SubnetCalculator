using System.Collections;
using System.Numerics;

namespace Service
{
    public class SubnetCalculator
    {
        // Zeigt alle Subnetze an, die berechnet werden
        public void ShowAvailableSubnets(SubnetEntity inputEntity)
        {
            if (ValidateUserInput(inputEntity))
            {
                foreach (var item in CalculateAvailableSubnets(inputEntity))
                {

                }
            }
            else
            {
                Console.WriteLine("Ihre Eingaben haben das falsche Format");
            }
        }

        // Berechnet alle Subnetze mithilfe der User Inputs und speichert diese in einer ArrayList
        private ArrayList CalculateAvailableSubnets(SubnetEntity inputEntity)
        {
            string? ipAdressBinary = StringToBinaryString(inputEntity.IPAdress);
            string? subnetmaskBinary = StringToBinaryString(inputEntity.SubnetMask);
            // int logOfAmountSubnets = CalcLogarithmus(inputEntity.SubnetAmount);
            int amountOnesInMask = CountOnesInSubnetMask(subnetmaskBinary);
            string? networkAdressBinary = CalcNetworkAdressBinary(ipAdressBinary, subnetmaskBinary);
            return null;
        }

        // Zählt die einsen in der Subnetzmaske um später die Anzahl an Stellen in der Netzwerkadresse zu überspringen
        private int CountOnesInSubnetMask(string subnetmaskBinary)
        {
            char[] subnetmaskAsChars =  StringToCharArray(subnetmaskBinary);
            int counter = 0;
            foreach (var binaryNum in subnetmaskAsChars)
            {
                int binaryNumAsInt = CharToInt(binaryNum);
                if (binaryNumAsInt == 1) {
                    counter++;
                };
            }
            return counter;
        }

        // Berechnet die Netzwerkadresse mithilfe einer AND Operation und gibt diese als Binärstring aus
        public string CalcNetworkAdressBinary(string ipAdressBinary, string subnetMaskBinary)
        {
            string resultOfAND = "";
            string partialResultOfAND = "";
            int tableCounter = 0;

            string[] splittedIpAdressBinary = SplitString(ipAdressBinary);
            string[] splittedSubnetmaskBinary = SplitString(subnetMaskBinary);

            for(int i = 0; i < splittedSubnetmaskBinary.Length; i++)
            {
                char[] singleNumsSubnet = splittedSubnetmaskBinary[i].ToCharArray();
                char[] singleNumsIP = splittedIpAdressBinary[i].ToCharArray();
                partialResultOfAND = "";

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

        public double CalcLogarithmus(int subnetAmount){
            double amountOfSubnets = Convert.ToInt32(subnetAmount);
            amountOfSubnets = Math.Log2(amountOfSubnets);
            return amountOfSubnets;
        }

        // Nimmt einen String als Eingabe und gibt einen String in Form von Binärcode zurück
        public string StringToBinaryString(string stringToConvert)
        {
            string[] splittedAdress = SplitString(stringToConvert);
            ArrayList AdressInBinaryCode = new ArrayList();
            try
            {
                foreach (var octet in splittedAdress)
                {
                    int parsedOctet = int.Parse(octet);
                    string partialResult = "";

                    for (int i = parsedOctet; i > 0; i/=2){
                        partialResult += parsedOctet % 2;
                        parsedOctet /= 2;
                    }

                    partialResult = FillUpWithZeros(partialResult);
                    partialResult = ReverseString(partialResult);
                    AdressInBinaryCode.Add(partialResult);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Die IP-Adresse hat das falsche Format: {ex}");
            }
            
            return string.Join(".", AdressInBinaryCode.ToArray());
        }

        // Nimmt einen String im Binärformat und konvertiert diesen zu String im Dezimalformat
        public string BinaryToString(string binaryToConvert)
        {
            string[] splittedBinary = SplitString(binaryToConvert);
            int resultOfOctetInDecimal = 0;
            string result = "";
            int counter = 0;

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
        private string[] SplitString(string ipAdress){
            string[] splittedString = ipAdress.Split(".");
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

        // Fügt die einzelnen Chars von einem Oktet String in ein Dictionary dessen Key Values für die Umrechnung von Binär zu Dezimal verwendet werden
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
            char[] chars = { };
            foreach (var item in splittedString)
            {
                chars = item.ToCharArray();
            }
           
            return chars;
        }

        // Konvertiert Chars zu Integer
        private int CharToInt(char charToConvert)
        {
            int convertedChar = Convert.ToInt32(charToConvert);
            return convertedChar;
        }

        // Prüft ob die Eingaben des Nutzers die geeignete Länge für eine Mögliche Ip Adresse bzw. Subnetzmaske hat
        private bool ValidateUserInput(SubnetEntity inputEntity)
        {
            int inputIpAdressLength = inputEntity.IPAdress.Length;
            int inputSubnetmaskLength = inputEntity.SubnetMask.Length;

            if (inputIpAdressLength > 15 || inputIpAdressLength < 7 || inputSubnetmaskLength > 15 || inputSubnetmaskLength < 7)
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