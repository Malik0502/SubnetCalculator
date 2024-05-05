namespace Service
{
    public class AsymSubnetEntity
    {
        public string? IPAdress { get; set; }

        public int SubnetAmount { get; set; }

        public List<int> HostAmount { get; set; } = new List<int>();
    }
}
