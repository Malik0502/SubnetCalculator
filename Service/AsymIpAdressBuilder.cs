using Service.Interfaces;

namespace Service{
    public class AsymIpAdressBuilder : IAsymIpAdressBuilder{
        
        // Baut die gesamte IP Adresse zusammen
        public string BuildIpAdress(string subnet, int amountOnesMask, char[] networkAdressAsChars, int i, int hostbit)
        {
            int posCounter = 0;
            subnet += BuildZeroPartIp(out posCounter, amountOnesMask, networkAdressAsChars);

            subnet = BuildMainIpPart(i, hostbit, subnet, networkAdressAsChars, posCounter);

            subnet = BuildZerosAtEndIfNeeded(amountOnesMask, hostbit, networkAdressAsChars, subnet);

            return subnet;
        }

        // Baut den ersten Teil der Ip-Adresse
        // Baut den Teil so lang wie Einsen in der Subnetzmaske vorhanden sind.
        private string BuildZeroPartIp(out int posCounter, int amountOnesMask, Char[] networkAdressAsChars)
        {
            string subnet = string.Empty;
            for (posCounter = 0; posCounter < amountOnesMask; posCounter++)
            {
                subnet += ((posCounter + 1) % 8 != 0) ? networkAdressAsChars[posCounter] : networkAdressAsChars[posCounter] + ".";
            }
            return subnet;
        }

        private string ConvertCounterToBinary(int counter, int hostbit)
        {
            return Convert.ToString(counter, 2).PadLeft(Convert.ToInt32(hostbit), '0');
        }

        // Konvertiert den Zähler i ins Binäre System.
        // Dann wird durch die Zahl als Chars durch iteriert
        private string BuildMainIpPart(int counter, int hostbit, string subnet, char[] networkAdressAsChars, int posCounter)
        {
            string binary = ConvertCounterToBinary(counter, hostbit);
            char[] binaryAsChar = binary.ToCharArray();
            foreach (var item in binaryAsChar)
            {
                subnet += ((networkAdressAsChars.Length - posCounter) % 8 != 0) ? item : item + ".";
                posCounter++;
            }
            return subnet;
        }

        // Fügt am Ende die restlichen Nullen hinzu, falls nötig
        private string BuildZerosAtEndIfNeeded(int amountOnesMask, int hostbit, char[] networkAdressAsChars, string subnet)
        {
            for (int binaryRest = amountOnesMask + Convert.ToInt32(hostbit); binaryRest < networkAdressAsChars.Length; binaryRest++)
            {
                subnet += (binaryRest % 8 != 0) ? networkAdressAsChars[binaryRest] : networkAdressAsChars[binaryRest] + ".";
            }
            return subnet;
        }
    }
}

