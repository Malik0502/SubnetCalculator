using System.Collections;

namespace Service
{
    public class SubnetCalculator
    {
        public void ShowAvailableSubnets(SubnetEntity inputEntity)
        {
            foreach (var item in CalculateAvailableSubnets(inputEntity))
            {

            }
        }


        public ArrayList CalculateAvailableSubnets(SubnetEntity inputEntity)
        {
            string ipAdressInBinary = StringToBinaryString(inputEntity.IPAdress);
            string subnetmaskInBinary = StringToBinaryString(inputEntity.SubnetMask);
            int zeroAmountMask = CountOnesInSubnetMask(subnetmaskInBinary);
            return null;
        }

        private int CountOnesInSubnetMask(string subnetmaskInBinary)
        {
            char[] subnetmaskAsChars =  StringToCharArray(subnetmaskInBinary);
            ArrayList singleSubnetmaskEntriesAsInt = new ArrayList();
            int counter = 0;
            foreach (var binaryNum in subnetmaskAsChars)
            {
                int binaryNumAsInt = charToInt(binaryNum);
                if (binaryNumAsInt == 1) {
                    counter++;
                };
            }
            return counter;
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
                AddCharToIntToDictionary(singleNumsFromOctet, conversionTable);

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

        private void AddCharToIntToDictionary(char[] charToConvert, Dictionary<int, int> conversionTable){
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

        private int charToInt(char charToConvert)
        {
            int convertedChar = Convert.ToInt32(charToConvert);
            return convertedChar;
        }
    }
}