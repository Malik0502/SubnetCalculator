using System.Collections;
using System.Globalization;
using System.Numerics;
using System.Xml.XPath;

namespace Service
{
    public class SubnetCalculator
    {
        public void ShowAvailableSubnets(string ipAdress, string subnetmask, int subnetAmount)
        {

        }


        private void CalculateAvailableSubnets(string ipAdress, string subnetmask, int subnetAmount)
        {
            string ipAdressInBinary = StringToBinaryString(ipAdress);
            string subnetmaskInBinary = StringToBinaryString(subnetmask);
        }

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

        // Nicht fertig
        // Bis jetzt wird eins der 4 Oktette genommen.
        // Dort werden die acht Zahlen einzeln gespeichert
        // Diese werden dann in einem Dicitonary von 128 bis 1 gespeichert
        // Gerade angefangen Berechnung von Binär zu machen
        // Result += Key * Value (Sollte zur Zahl werden)
        // Methode wurde zum Testen von string zu int geändert
        // UnitTest ist erfolgreich (11000000 wurde korrekterweise zu 192)
        // Jetzt fehlen nur noch die restlichen Oktetten.
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
                CharToInt(singleNumsFromOctet, conversionTable);

                foreach (var item in conversionTable)
                {
                    resultOfOctetInDecimal += item.Key * item.Value;
                }

                if(++counter == conversionTable.Count)
                {
                    result += resultOfOctetInDecimal; 
                }
                else{
                    result += resultOfOctetInDecimal + ".";
                }
                
            }
            return result;
        }

        private string[] SplitString(string ipAdress){
            string[] splittedString = ipAdress.Split(".");
            return splittedString;
        } 

        private string ReverseString(string stringToReverse){
            return new string(stringToReverse.Reverse().ToArray());
        }

        private string FillUpWithZeros(string stringToFillUp)
        {
            for(int i = stringToFillUp.Count(); i < 8; i++)
            {
                stringToFillUp += 0;
            }

            return stringToFillUp;
        }

        private void CharToInt(char[] charToConvert, Dictionary<int, int> conversionTable){
            int bitCounter = 128;
            int num = 0;
            foreach (var numAsChar in charToConvert)
            {
                string charToString = numAsChar.ToString();
                num = int.Parse(charToString);
                conversionTable.Add(bitCounter, num);
                bitCounter /= 2;
            }
        }
    }
}