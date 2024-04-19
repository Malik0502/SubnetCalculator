using System.Collections;
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
            string ipAdressInBinary = StringToBinary(ipAdress);
            string subnetmaskInBinary = StringToBinary(subnetmask);
        }

        public string StringToBinary(string stringToConvert)
        {
            string[] splittedAdress = SplitIpAdressString(stringToConvert);
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

        public string BinaryToString(int binaryToConvert)
        {
            return "";
        }

        private string[] SplitIpAdressString(string ipAdress){
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
    }
}