namespace Service.Interfaces
{
    public interface IBinaryString
    {
        public int CountOnesInSubnetMask(string subnetmaskBinary);

        public string FillUpWithZeros(string stringToFillUp);

        public string IncrementIpAdress(string ipAdress, int startIndex);
    }
}