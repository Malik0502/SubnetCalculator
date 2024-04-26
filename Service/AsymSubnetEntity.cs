using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class AsymSubnetEntity
    {
        public string? IPAdress { get; set; }

        public int SubnetAmount { get; set; }

        public List<int> HostAmount { get; set; } = new List<int>();
    }
}
