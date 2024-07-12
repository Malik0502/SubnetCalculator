namespace Service.Interfaces
{
    public interface IAsymCalculator
    {
        public List<string> CalcAvailableAsymSubnets(AsymSubnetEntity inputEntity);
    }
}
