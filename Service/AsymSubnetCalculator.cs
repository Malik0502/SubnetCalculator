namespace Service
{
    public class AsymSubnetCalculator
    {
        private SubnetCalcHelper helper = new();
        private List<string> resultAsymCalc = new();

        public void ShowAvailableAsymSubnets(AsymSubnetEntity inputEntity)
        {
            foreach (string subnet in CalcAvailableAsymSubnets(inputEntity))
            {
                string asyncSubnetAsDecimal = helper.BinaryToString(subnet);
                Console.WriteLine(asyncSubnetAsDecimal);
            }
        }

        public List<string> CalcAvailableAsymSubnets(AsymSubnetEntity inputEntity)
        {
            string iPAdressBinary = helper.StringToBinaryString(inputEntity.IPAdress);
            int subnetAmount = inputEntity.SubnetAmount;

            List<int> hostAmount = inputEntity.HostAmount;
            

            if (hostAmount.Count != 0)
            {
                hostAmount.Sort();
                hostAmount.Reverse();

                int hosts = hostAmount[0];

                // Berechnet mithilfe von neededHosts, die benötigten Hostbits und daraufhin die Subnetzmaske
                int neededHosts = helper.GetMinNeededHosts(hosts);
                int hostbit = Convert.ToInt32(helper.CalcLogarithmus(neededHosts));

                string subnetmaskBinary = CalcSubnetmask(hostbit);
                int amountOnesMask = helper.CountOnesInSubnetMask(subnetmaskBinary);

                string networkadress = helper.CalcNetworkAdressBinary(iPAdressBinary, subnetmaskBinary);
                char[] networkAdressAsChars = helper.StringToCharArray(networkadress);
                
                string subnet = "";

                Console.WriteLine($"Subnetz {resultAsymCalc.Count + 1}:");

                // Baut den ersten Teil der Subnet Adresse zusammen. So weit wie es Einsen in der Subnetzmaske gibt
                for (int subnetCount = 0; subnetCount < neededHosts + 1; subnetCount++)
                {
                    int posCounter;
                    for (posCounter = 0; posCounter < amountOnesMask; posCounter++)
                    {
                        if ((posCounter + 1) % 8 == 0 && posCounter != 0)
                        {
                            subnet += networkAdressAsChars[posCounter] + ".";
                        }
                        else
                        {
                            subnet += networkAdressAsChars[posCounter];
                        }
                    }

                    // Baut den Teil zusammen, der geändert werden muss und zählt diesen Hoch
                    // Nimmt den subnetCount und wandelt diesen in Binär um
                    // Dieser wird dann mit Nullen aufgefüllt und nach rechts alignt bis der String so lang ist wie die Anzahl der benötigten Netze
                    
                    if(subnetCount == neededHosts)
                    {
                        string binary = Convert.ToString(subnetCount, 2).PadLeft(Convert.ToInt32(hostbit - 1), '0');
                    }
                    else
                    {
                        string binary = Convert.ToString(subnetCount, 2).PadLeft(Convert.ToInt32(hostbit), '0');
                    }
                    char[] binaryAsChar = binary.ToCharArray();

                    foreach (var binaryNum in binaryAsChar)
                    {
                        if ((networkAdressAsChars.Length - posCounter) % 8 == 0)
                        {
                            subnet += binaryNum + ".";
                        }
                        else
                        {
                            subnet += binaryNum;
                        }
                        posCounter++;
                    }

                    // Fügt die restlichen Nummer an die Ip-Adresse ran
                    for (int binaryRest = amountOnesMask + Convert.ToInt32(hostbit); binaryRest < networkAdressAsChars.Length; binaryRest++)
                    {
                        if (binaryRest % 8 == 0)
                        {
                            subnet += networkAdressAsChars[binaryRest] + ".";
                        }
                        else
                        {
                            subnet += networkAdressAsChars[binaryRest];
                        }
                    }
                    resultAsymCalc.Add(subnet);
                    subnet = "";
                }
                hostAmount.RemoveAt(0);
                AsymSubnetEntity asymSubnetEntity = new AsymSubnetEntity()
                {
                    IPAdress = resultAsymCalc.Last(),
                    SubnetAmount = subnetAmount - 1,
                    HostAmount = hostAmount,
                };
                resultAsymCalc.RemoveAt(resultAsymCalc.Count - 1);
                CalcAvailableAsymSubnets(asymSubnetEntity);

            }
            else
            {
                return resultAsymCalc;
            }
            return resultAsymCalc;
        }

        public string CalcSubnetmask(double hostbits)
        {
            // Berechnet die Anzahl an Einsen die nötig sind
            // Hostbits = Anzahl Nullen in Subnetzmaske
            int amountOfOnes = 32 - Convert.ToInt32(hostbits);
            string subnetmask = ""; 

            // Setzt die Subnetzmaske in Binär zusammen. Wenn I <= Anzahl Einsen dann wird eine 1 gesetzt
            // Sonst eine 0
            // Falls i Modulo 8 = 0 ergibt, dann ist das Ende eines Oktets erreicht. D.h. Punkt muss gesetzt werden
            for (int i = 1; i <= 32; i++)
            {
                if(i <= amountOfOnes)
                {
                    if (i % 8 == 0 && i != 32)
                    {
                        subnetmask += "1" + ".";
                    }
                    else
                    {
                        subnetmask += "1";
                    }
                }
                else
                {
                    if (i % 8 == 0 && i != 32)
                    {
                        subnetmask += "0" + ".";
                    }
                    else
                    {
                        subnetmask += "0";
                    }
                }
            }
            return subnetmask;
        }
    }
}
