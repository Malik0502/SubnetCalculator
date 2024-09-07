namespace Service.Interfaces{
    public interface IAsymIpAdressBuilder{
        string BuildIpAdress(string subnet, int amountOnesMask, char[] networkAdressAsChars, int i, int hostbit);
    }
}