namespace Service.Interfaces
{
    public interface ISubnetHelper
    {
        public string CalcNetworkAdressBinary(string ipAdressBinary, string subnetMaskBinary);

        public int CountOnesInSubnetMask(string subnetmaskBinary);

        public string[] SplitString(string ipAdress);

        public string ReverseString(string strinToReverse);

        public string FillUpWithZeros(string stringToFillUp);

        public double CalcLogarithmus(int subnetAmount);

        public void AddConvertedCharToDic(char[] charToConvert, Dictionary<int, int> conversionTable);

        public int GetMinNeededHosts(int hostAmount);

        public int CalcNeededHostbits(int hostAmount);

        public string IncrementIpAdress(string ipAdress, int startIndex);
    }
}
