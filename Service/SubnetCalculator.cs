namespace Service
{
    public class SubnetCalculator
    {
        public void ShowAvailableSubnets(string ipAdress, string subnetmask, int subnetAmount)
        {

        }


        private void CalculateAvailableSubnets(string ipAdress, string subnetmask, int subnetAmount)
        {
            int ipAdressInBinary = StringToBinary(ipAdress);
            int subnetmaskInBinary = StringToBinary(subnetmask);
        }

        private void CalculateAvaialbleAsynchSubnets()
        {

        }

        public int StringToBinary(string stringToConvert)
        {
            
            return 0;
        }

        public string BinaryToString(int binaryToConvert)
        {
            return "";
        }
    }
}