namespace Service.Interfaces
{
    public  interface IInformation
    {
        public void ShowAvailableSubnets(SubnetEntity inputEntity);

        public bool ValidateUserInput(SubnetEntity inputEntity);

        public void ShowAvailableAsymSubnets(AsymSubnetEntity inputEntity);
    }
}
