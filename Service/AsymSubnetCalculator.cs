using Service.Interfaces;

namespace Service
{
    public class AsymSubnetCalculator : IAsymCalculator
    {
        private readonly ISubnetHelper helper;
        private readonly IParser parser;
        private readonly IBinaryString binaryString;

        public AsymSubnetCalculator(ISubnetHelper helper, IParser parser, IBinaryString binaryString) 
        {
            this.helper = helper;
            this.parser = parser;
            this.binaryString = binaryString;
        }

        private List<string> resultAsymCalc = new();

        /// <summary>
        /// Berechnet Asymetrisch Subnetze
        /// </summary>
        /// <param name="inputEntity"></param>
        /// <returns></returns>
        public List<string> CalcAvailableAsymSubnets(AsymSubnetEntity inputEntity)
        {
            string iPAdressBinary = inputEntity.IPAdress!;
            int subnetAmount = inputEntity.SubnetAmount;

            List<int> hostAmount = inputEntity.HostAmount;
            char[] binaryAsChar = Array.Empty<char>();

            // Abbruchbedingung
            if (hostAmount.Count == 0)
            {
                return resultAsymCalc;
            }

            hostAmount.Sort();
            hostAmount.Reverse();

            int hosts = hostAmount[0];

            // Berechnet mithilfe von neededHosts, die benötigten Hostbits und daraufhin die Subnetzmaske
            int neededHosts = helper.GetMinNeededHosts(hosts);
            int hostbit = Convert.ToInt32(helper.CalcLogarithmus(neededHosts));

            string subnetmaskBinary = helper.CalcSubnetmask(hostbit);
            int amountOnesMask = binaryString.CountOnesInSubnetMask(subnetmaskBinary);

            // Berechnet die Netzwerkadresse und teilt die einzelnen Zeichen in einem Array auf
            string networkadress = helper.CalcNetworkAdressBinary(iPAdressBinary, subnetmaskBinary);
            char[] networkAdressAsChars = parser.StringToCharArray(networkadress);

            string subnet = "";

            for (int i = 0; i < neededHosts + 1; i++)
            {
                // Wenn i gleich neededHost ist wird die Netzwerkadresse des nächsten Netzes berechnet
                // Dafür benutzen wir die Broadcastadresse und erhöhen diese um 1
                // Dabei entsteht die Netzwerkadresse fürs nächste Subnetz
                if (i == neededHosts)
                {
                    resultAsymCalc.Add(binaryString.IncrementIpAdress(resultAsymCalc.Last(), amountOnesMask));
                    subnet = "";
                }
                else
                {
                    // Baut den ersten Teil der Ip-Adresse
                    // Baut den Teil so lang wie Einsen in der Subnetzmaske vorhanden sind.
                    int posCounter;
                    for (posCounter = 0; posCounter < amountOnesMask; posCounter++)
                    {
                        subnet += ((posCounter + 1) % 8 != 0) ? networkAdressAsChars[posCounter] : networkAdressAsChars[posCounter] + ".";
                    }

                    // Wichtigster Teil
                    // Konvertiert den Zähler i ins Binäre System.
                    // Dann wird durch die Zahl als Chars durch iteriert
                    string binary = Convert.ToString(i, 2).PadLeft(Convert.ToInt32(hostbit), '0');
                    binaryAsChar = binary.ToCharArray();
                    foreach (var item in binaryAsChar)
                    {
                        subnet += ((networkAdressAsChars.Length - posCounter) % 8 != 0) ? item : item + ".";

                        posCounter++;
                    }

                    // Fügt am Ende die restlichen Nullen hinzu, falls nötig
                    for (int binaryRest = amountOnesMask + Convert.ToInt32(hostbit); binaryRest < networkAdressAsChars.Length; binaryRest++)
                    {
                        subnet += (binaryRest % 8 != 0) ? networkAdressAsChars[binaryRest] : networkAdressAsChars[binaryRest] + ".";
                    }

                    // Fügt nur wichtige Ip-Adressen in die Liste hinzu. 
                    // Dazu gehört die erste Adresse nach der Netzwerkadresse
                    // Die letzte Adresse vor der Broadcastadresse
                    // Sowie die Broadcastadresse
                    if (i == 1 || i == neededHosts - 1 || i == neededHosts - 2)
                    {
                        resultAsymCalc.Add(subnet);
                    }
                    subnet = "";
                }
            }
                // Die erste Stelle aus hostAmount wird entfernt um eine Abbruchbedingung für die Rekursive Funktion zu haben
                hostAmount.RemoveAt(0);

                // Erstellt ein neues asymSubnetEntity mit den neuen veränderten Werten
                AsymSubnetEntity asymSubnetEntity = new AsymSubnetEntity()
                {
                    IPAdress = resultAsymCalc.Last(),
                    SubnetAmount = subnetAmount - 1,
                    HostAmount = hostAmount,
                };

                // Entfernt die Netzwerkadresse des nächsten Subnetzes und die Broadcastadresse aus der Liste
                // Dies sorgt dafür, dass wir am Ende eine Liste zurückbekommen die den nutzbaren Adressraum darstellt
                resultAsymCalc.RemoveAt(resultAsymCalc.Count - 1);
                resultAsymCalc.RemoveAt(resultAsymCalc.Count - 1);

                // Ruft die Funktion mit den veränderten Werten erneut auf
                CalcAvailableAsymSubnets(asymSubnetEntity);
            return resultAsymCalc;
        }
    }
}
