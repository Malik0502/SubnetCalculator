using System.Data;
using Service.Interfaces;

namespace Service
{
    public class AsymSubnetCalculator : IAsymCalculator
    {
        private readonly ISubnetHelper _helper;
        private readonly IParser _parser;
        private readonly IBinaryString _binaryString;
        private readonly IAsymIpAdressBuilder _ipAdressBuilder;

        public AsymSubnetCalculator(ISubnetHelper helper, IParser parser, IBinaryString binaryString, IAsymIpAdressBuilder ipAdressBuilder) 
        {
            _helper = helper;
            _parser = parser;
            _binaryString = binaryString;
            _ipAdressBuilder = ipAdressBuilder;
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
            int neededHosts = _helper.GetMinNeededHosts(hosts);
            int hostbit = Convert.ToInt32(_helper.CalcLogarithmus(neededHosts));

            string subnetmaskBinary = _helper.CalcSubnetmask(hostbit);
            int amountOnesMask = _binaryString.CountOnesInSubnetMask(subnetmaskBinary);

            // Berechnet die Netzwerkadresse und teilt die einzelnen Zeichen in einem Array auf
            string networkadress = _helper.CalcNetworkAdressBinary(iPAdressBinary, subnetmaskBinary);
            char[] networkAdressAsChars = _parser.StringToCharArray(networkadress);

            string subnet = "";

            for (int i = 0; i < neededHosts + 1; i++)
            {
                // Wenn i gleich neededHost ist wird die Netzwerkadresse des nächsten Netzes berechnet
                // Dafür benutzen wir die Broadcastadresse und erhöhen diese um 1
                // Dabei entsteht die Netzwerkadresse fürs nächste Subnetz
                if (i == neededHosts)
                {
                    resultAsymCalc.Add(_binaryString.IncrementIpAdress(resultAsymCalc.Last(), amountOnesMask));
                    subnet = "";
                }
                else
                {
                    subnet = _ipAdressBuilder.BuildIpAdress(subnet, amountOnesMask, networkAdressAsChars, i, hostbit);

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
            PrepareForRecursion(hostAmount, subnetAmount);
            return resultAsymCalc;
        }

        private void PrepareForRecursion(List<int> hostAmount, int subnetAmount)
        {
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
        }
    }   
}
