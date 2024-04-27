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
            List<double> hostbits = new();

            foreach (int hosts in hostAmount)
            {
                neededHosts.Add(helper.GetMinNeededHosts(hosts));
                foreach (var item in neededHosts)
                {
                    hostbits.Add(helper.CalcLogarithmus(item));
                }
            }

            return null;
        }


        

        private void CalcSubnetmask()
        {

        }
    }
}
