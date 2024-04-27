namespace Service
{
    public class AsymSubnetCalculator
    {
        SubnetCalcHelper helper = new();

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
            int subnetAmount= inputEntity.SubnetAmount;

            List<int> hostAmount = inputEntity.HostAmount;
            List<int> neededHosts = new();

            // Iteriert durch die Hostliste und findet für jede Eingabe heraus wie groß das Netz mindestens sein muss,
            // damit alle Clients reinpassen (30 Host -> 32 Clients müssen verfügbar sein etc.)
            foreach (int hosts in hostAmount)
            {
                neededHosts.Add(helper.GetMinNeededHosts(hosts));

                // Iteriert durch eine Liste mit berechneten Hostgrößen um dadurch die Hostbits zu berechnen
                // Durch die Hostbit Anzahl kann dann die Subnetzmaske für jedes einzelne Subnetz berechnet werden
                foreach (var item in neededHosts)
                {
                    int hostbit = helper.GetMinNeededHosts(item);
                    string subnetmask = CalcSubnetmask(hostbit);
                }
            }
            return null;
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
