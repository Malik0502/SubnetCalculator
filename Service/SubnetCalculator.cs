using System.Collections;
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
            int ipAdressInBinary = StringToBinary(ipAdress);
            int subnetmaskInBinary = StringToBinary(subnetmask);
        }

        public int StringToBinary(string stringToConvert)
        {
            string[] splittedIP = splitIpAdressString(stringToConvert);
            ArrayList binaryCode = new ArrayList();

            try
            {
                foreach (var item in splittedIP)
                {
                    int parsedItem = int.Parse(item);
                    for(int i = 0; i <= parsedItem; i++){
                        int result = parsedItem % 2;
                        binaryCode.Add(result);
                    }
                }
                return int.Parse(reverseList(binaryCode));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Die IP-Adresse hat das falsche Format: {ex}");
            }
            
            return 0;
        }

        public string BinaryToString(int binaryToConvert)
        {
            return "";
        }

        private string[] splitIpAdressString(string ipAdress){
            string[] splittedString = ipAdress.Split(".");
            return splittedString;
        } 

        private string reverseList(ArrayList listToReverse){
            string result = "";
            for (int i = listToReverse.Count; i >= 0; i--)
            {
                result += listToReverse[i];
            }

            return result;
        }
    }
}