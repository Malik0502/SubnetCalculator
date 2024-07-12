using Service.Interfaces;

namespace Service
{
    public class SubnetCalculator : ICalculator
    {
        private readonly IParser parser;
        private readonly ISubnetHelper helper;
        private readonly IBinaryString binaryString;

        public SubnetCalculator(IParser parser, ISubnetHelper helper, IBinaryString binaryString)
        {
            this.parser = parser;
            this.helper = helper;
            this.binaryString = binaryString;
        }

        /// <summary>
        /// Berechnet mögliche Subnetze mithilfe einer Ip-Adresse, einer Subnetzmaske und der Anzahl an gewünschten Teilnetzen
        /// </summary>
        /// <param name="inputEntity"></param>
        /// <returns></returns>
        public List<string> CalcAvailableSubnets(SubnetEntity inputEntity)
        {
            string? ipAdressBinary = parser.StringToBinary(inputEntity.IPAdress!);
            string? subnetmaskBinary = parser.StringToBinary(inputEntity.SubnetMask!);

            int subnetAmount = inputEntity.SubnetAmount;

            string? networkAdressBinary = helper.CalcNetworkAdressBinary(ipAdressBinary, subnetmaskBinary);
            double logOfAmountSubnets = helper.CalcLogarithmus(helper.GetMinNeededHosts(subnetAmount));               

            int amountOnesInMask = binaryString.CountOnesInSubnetMask(subnetmaskBinary);

            char[] singleNumsNetworkAdress = parser.StringToCharArray(networkAdressBinary);
            string subnet = "";

            List<string> subnets = new();

            // Baut den ersten Teil der Subnet Adresse zusammen. So weit wie es Einsen in der Subnetzmaske gibt
            for (int subnetCount = 0; subnetCount < subnetAmount; subnetCount++)
            {
                int posCounter;
                for (posCounter = 1; posCounter - 1 < amountOnesInMask; posCounter++)
                {
                    subnet += (posCounter % 8 != 0) ? singleNumsNetworkAdress[posCounter - 1] : singleNumsNetworkAdress[posCounter - 1] + ".";
                }
                
                // Baut den Teil zusammen, der geändert werden muss und zählt diesen Hoch
                // Nimmt den subnetCount und wandelt diesen in Binär um
                // Dieser wird dann mit Nullen aufgefüllt und nach rechts alignt bis der String so lang ist wie die Anzahl der Hostbits
                string binary = Convert.ToString(subnetCount, 2).PadLeft(Convert.ToInt32(logOfAmountSubnets), '0');
                char[] binaryAsChar = binary.ToCharArray();

                foreach (var binaryNum in binaryAsChar)
                {
                    subnet += ((singleNumsNetworkAdress.Length - posCounter) % 8 != 0) ? binaryNum : binaryNum + ".";

                    posCounter++;
                }

                // Fügt die restlichen Nummer an die Ip-Adresse ran
                for (int binaryRest = amountOnesInMask + Convert.ToInt32(logOfAmountSubnets); binaryRest < singleNumsNetworkAdress.Length; binaryRest++)
                {
                    subnet += (binaryRest % 8 != 0) ? singleNumsNetworkAdress[binaryRest] : singleNumsNetworkAdress[binaryRest] + ".";
                }
                subnets.Add(subnet);
                subnet = "";
            }
            return subnets;
        }

    }
}