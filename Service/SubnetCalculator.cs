namespace Service
{
    public class SubnetCalculator
    {
        SubnetCalcHelper helper = new();

        // Zeigt alle Subnetze an, die berechnet werden in Dezimalformat an
        public void ShowAvailableSubnets(SubnetEntity inputEntity)
        {
            if (!ValidateUserInput(inputEntity))
            {
                Console.WriteLine("Ihre Eingaben haben das falsche Format");
                Environment.Exit(0);
            }
            foreach (string subnet in CalcAvailableSubnets(inputEntity))
            {
                string subnetAsDecimal = helper.BinaryToString(subnet);
                Console.WriteLine(subnetAsDecimal);
            }
        }

        // Berechnet mögliche Subnetze mithilfe einer Ip-Adresse, einer Subnetzmaske und der Anzahl an gewünschten Teilnetzen
        public List<string> CalcAvailableSubnets(SubnetEntity inputEntity)
        {
            string? ipAdressBinary = helper.StringToBinary(inputEntity.IPAdress);
            string? subnetmaskBinary = helper.StringToBinary(inputEntity.SubnetMask);

            int subnetAmount = inputEntity.SubnetAmount;

            string? networkAdressBinary = helper.CalcNetworkAdressBinary(ipAdressBinary, subnetmaskBinary);
            double logOfAmountSubnets = helper.CalcLogarithmus(helper.GetMinNeededHosts(subnetAmount));               
            /* Alte Version:
             * Math.Ceiling(helper.CalcLogarithmus(inputEntity.SubnetAmount));*/

            int amountOnesInMask = helper.CountOnesInSubnetMask(subnetmaskBinary);

            char[] singleNumsNetworkAdress = helper.StringToCharArray(networkAdressBinary);
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

        // Prüft ob die Eingaben des Nutzers die geeignete Länge für eine Mögliche Ip Adresse bzw. Subnetzmaske hat
        // Eine Ip-Adresse / Subnetzmaske mitsamt Punkten kann mindestens 7 und maximal 15 Zeichen beinhalten
        // Ebenfalls wenn eines der beiden 0 ist wird false ausgegeben
        private bool ValidateUserInput(SubnetEntity inputEntity)
        {
            int inputIpAdressLength = inputEntity.IPAdress.Length;
            int inputSubnetmaskLength = inputEntity.SubnetMask.Length;

            return (inputIpAdressLength > 15 || inputIpAdressLength < 7 || inputSubnetmaskLength > 15 || inputSubnetmaskLength < 7 || inputIpAdressLength == 0 || inputSubnetmaskLength == 0)
                ? false : true;
        }
    }
}