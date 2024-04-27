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

            return null;
        }
    }
}
