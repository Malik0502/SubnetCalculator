namespace Service.Interfaces
{
    public interface IParser
    {
        public string StringToBinary(string stringToConvert);

        public string BinaryToString(string binaryToConvert);

        public char[] StringToCharArray(string stringToConvert);
    }
}
