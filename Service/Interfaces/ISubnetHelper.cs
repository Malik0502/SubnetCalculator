namespace Service.Interfaces
{
    public interface ISubnetHelper
    {
        public string CalcNetworkAdressBinary(string ipAdressBinary, string subnetMaskBinary);

        public double CalcLogarithmus(int subnetAmount);

        public void AddConvertedCharToDic(char[] charToConvert, Dictionary<int, int> conversionTable);

        public int GetMinNeededHosts(int hostAmount);

        public int CalcNeededHostbits(int hostAmount);

        public string CalcSubnetmask(double hostbits);
    }
}
