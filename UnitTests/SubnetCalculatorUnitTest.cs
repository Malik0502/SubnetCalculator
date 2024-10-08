using Service;
using Service.Interfaces;

namespace UnitTests
{
    [TestClass]
    public class SubnetCalculatorUnitTest
    {   
        private IBinaryString InitBinaryString(){
            IBinaryString binaryString = new BinaryStringModificator();
            return binaryString;
        }

        [TestMethod]
        public void StringToBinary_IpAdress_ReturnTrue()
        {
            // Arrange
            IParser parser = new BinaryParser(InitBinaryString());

            string expected = "11000000.10101000.00000001.00000000";
            string input = "192.168.1.0";

            // Act

            string testResult = parser.StringToBinary(input);

            // Assert
            Assert.AreEqual(expected, testResult);
        }

        [TestMethod]
        public void StringToBinary_IpAdress_ReturnFalse()
        {
            // Arrange
            IParser parser = new BinaryParser(InitBinaryString());

            string expected = "11000000.10101000.00000001.00000000";
            string input = "10.168.1.0";

            // Act

            string testResult = parser.StringToBinary(input);

            // Assert
            Assert.AreNotEqual(expected, testResult);
        }

        [TestMethod]
        public void StringToBinary_Subnetmask_ReturnTrue()
        {
            // Arrange

            IParser parser = new BinaryParser(InitBinaryString());
            string expected = "11111111.11111111.11000000.00000000";
            string input = "255.255.192.0";

            // Act

            string testResult = parser.StringToBinary(input);

            // Assert
            Assert.AreEqual(expected, testResult);
        }

        [TestMethod]
        public void StringToBinary_Subnetmask_ReturnFalse()
        {
            // Arrange 

            IParser parser = new BinaryParser(InitBinaryString());
            string expected = "11111111.11111111.11000000.00000000";
            string input = "255.255.255.0";

            // Act

            string testResult = parser.StringToBinary(input);

            // Assert
            Assert.AreNotEqual(expected, input);

        }

        [TestMethod]
        public void BinaryToString_IpAdress_ReturnTrue()
        {
            // Arrange

            IParser parser = new BinaryParser(InitBinaryString());
            string expected = "192.168.1.0";
            string input = "11000000.10101000.00000001.00000000";

            // Act

            string testResult = parser.BinaryToString(input);

            // Assert

            Assert.AreEqual(expected, testResult);
        }

        [TestMethod]
        public void BinaryToString_IpAdress_ReturnFalse()
        {
            // Arrange

            IParser parser = new BinaryParser(InitBinaryString());
            string expected = "192.168.1.0";
            string input = "01111111.10101000.00000001.00000000";

            // Act

            string testResult = parser.BinaryToString(input);

            // Assert

            Assert.AreNotEqual(expected, testResult);
        }

        [TestMethod]
        public void BinaryToString_Subnetmask_ReturnTrue()
        {
            // Arrange

            IParser parser = new BinaryParser(InitBinaryString());
            string expected = "255.192.0.0";
            string input = "11111111.11000000.00000000.00000000";

            // Act

            string testResult = parser.BinaryToString(input);

            // Assert
            Assert.AreEqual(expected, testResult);
        }

        [TestMethod]
        public void BinaryToString_SubnetMask_ReturnFalse()
        {
            // Arrange

            IParser parser = new BinaryParser(InitBinaryString());
            string expected = "255.255.0.0";
            string input = "11111111.11000000.00000000.00000000";

            // Act

            string testResult = parser.BinaryToString(input);

            // Assert
            Assert.AreNotEqual(expected, testResult);
        }

        [TestMethod]
        public void CalcNetworkAdress_ReturnTrue()
        {
            // Arrange

            var helper = new SubnetCalcHelper();
            string expected = "00001000.00001000.00001000.00000000";
            string inputIp = "00001000.00001000.00001000.00001000";
            string inputSubnet = "11111111.11111111.11111111.11110000";

            // Act

            string testResult = helper.CalcNetworkAdressBinary(inputIp, inputSubnet);

            // Assert

            Assert.AreEqual(expected, testResult);
        }

        [TestMethod]
        public void CalcNetworkAdressTest_ReturnTrue()
        {
            // Arrange

            var helper = new SubnetCalcHelper();
            string expected = "10101100.00010000.00000001.10110000";
            string inputIp = "10101100.00010000.00000001.10110000";
            string inputSubnet = "11111111.11111111.11111111.11110000";

            // Act

            string testResult = helper.CalcNetworkAdressBinary(inputIp, inputSubnet);

            // Assert

            Assert.AreEqual(expected, testResult);
        }

        [TestMethod]
        public void CalNetworkAdress_ReturnFalse()
        {

            // Arrange

            var helper = new SubnetCalcHelper();
            string expected = "00001000.00001000.00001000.00100100";
            string inputIp = "00001000.00001000.00001000.00001000";
            string inputSubnet = "11111111.11111111.11111111.11110000";

            // Act

            string testResult = helper.CalcNetworkAdressBinary(inputIp, inputSubnet);

            // Assert

            Assert.AreNotEqual(expected, testResult);
        }

        [TestMethod]
        public void CalcLogarithmus_ReturnFalse()
        {

            // Arrange

            var helper = new SubnetCalcHelper();
            double expected = 3;
            int input = 8;

            // Act

            double testResult = helper.CalcLogarithmus(input);

            // Assert

            Assert.AreEqual(expected, testResult);
        }

        [TestMethod]
        public void CalcEightAvailableSubnets_ReturnTrue()
        {
            // Arrange
            IBinaryString binaryString = InitBinaryString();
            IParser parser = new BinaryParser(binaryString);
            var helper = new SubnetCalcHelper();
            var svc = new SubnetCalculator(parser, helper, binaryString);
            List<string> expected = new List<string>();
            string inputIp = "192.168.1.0";
            string inputSubnet = "255.255.255.192";
            int inputSubnetAmount = 8;

            SubnetEntity inputEntity = new SubnetEntity()
            {
                IPAdress = inputIp,
                SubnetMask = inputSubnet,
                SubnetAmount = inputSubnetAmount,
            };

            expected.Add("192.168.1.0");
            expected.Add("192.168.1.8");
            expected.Add("192.168.1.16");
            expected.Add("192.168.1.24");
            expected.Add("192.168.1.32");
            expected.Add("192.168.1.40");
            expected.Add("192.168.1.48");
            expected.Add("192.168.1.56");

            // Act

            List<string> testResult = svc.CalcAvailableSubnets(inputEntity);

            // Assert

            for (int i = 0; i < testResult.Count; i++)
            {
                Assert.AreEqual(expected[i], parser.BinaryToString(testResult[i]));
            }
        }

        [TestMethod]
        public void CalcThirtyTwoAvailableSubnets_ReturnTrue()
        {
            // Arrange

            IBinaryString binaryString = InitBinaryString();
            IParser parser = new BinaryParser(binaryString);
            var helper = new SubnetCalcHelper();
            var svc = new SubnetCalculator(parser, helper, binaryString);
            List<string> expected = new List<string>();
            string inputIp = "192.168.35.2";
            string inputSubnet = "255.128.0.0";
            int inputSubnetAmount = 32;

            SubnetEntity inputEntity = new SubnetEntity()
            {
                IPAdress = inputIp,
                SubnetMask = inputSubnet,
                SubnetAmount = inputSubnetAmount,
            };

            expected.Add("192.128.0.0");
            expected.Add("192.132.0.0");
            expected.Add("192.136.0.0");
            expected.Add("192.140.0.0");
            expected.Add("192.144.0.0");
            expected.Add("192.148.0.0");
            expected.Add("192.152.0.0");
            expected.Add("192.156.0.0");
            expected.Add("192.160.0.0");
            expected.Add("192.164.0.0");
            expected.Add("192.168.0.0");
            expected.Add("192.172.0.0");
            expected.Add("192.176.0.0");
            expected.Add("192.180.0.0");
            expected.Add("192.184.0.0");
            expected.Add("192.188.0.0");
            expected.Add("192.192.0.0");
            expected.Add("192.196.0.0");
            expected.Add("192.200.0.0");
            expected.Add("192.204.0.0");
            expected.Add("192.208.0.0");
            expected.Add("192.212.0.0");
            expected.Add("192.216.0.0");
            expected.Add("192.220.0.0");
            expected.Add("192.224.0.0");
            expected.Add("192.228.0.0");
            expected.Add("192.232.0.0");
            expected.Add("192.236.0.0");
            expected.Add("192.240.0.0");
            expected.Add("192.244.0.0");
            expected.Add("192.248.0.0");
            expected.Add("192.252.0.0");

            // Act

            List<string> testResult = svc.CalcAvailableSubnets(inputEntity);

            // Assert

            for (int i = 0; i < testResult.Count; i++)
            {
                Assert.AreEqual(expected[i], parser.BinaryToString(testResult[i]));
            }
        }

        [TestMethod]
        public void CalcFourteenAvailableSubnets_ReturnTrue()
        {
            // Arrange

            IBinaryString binaryString = InitBinaryString();
            IParser parser = new BinaryParser(binaryString);
            var helper = new SubnetCalcHelper();
            var svc = new SubnetCalculator(parser, helper, binaryString);
            List<string> expected = new List<string>();
            string inputIp = "192.168.1.100";
            string inputSubnet = "255.252.0.0";
            int inputSubnetAmount = 14;

            SubnetEntity inputEntity = new SubnetEntity()
            {
                IPAdress = inputIp,
                SubnetMask = inputSubnet,
                SubnetAmount = inputSubnetAmount,
            };

            expected.Add("192.168.0.0");
            expected.Add("192.168.64.0");
            expected.Add("192.168.128.0");
            expected.Add("192.168.192.0");
            expected.Add("192.169.0.0");
            expected.Add("192.169.64.0");
            expected.Add("192.169.128.0");
            expected.Add("192.169.192.0");
            expected.Add("192.170.0.0");
            expected.Add("192.170.64.0");
            expected.Add("192.170.128.0");
            expected.Add("192.170.192.0");
            expected.Add("192.171.0.0");
            expected.Add("192.171.64.0");

            // Act

            List<string> testResult = svc.CalcAvailableSubnets(inputEntity);

            // Assert

            for (int i = 0; i < testResult.Count; i++)
            {
                Assert.AreEqual(expected[i], parser.BinaryToString(testResult[i]));
            }
        }

        [TestMethod]
        public void GetMinNeededHosts_ReturnTrue()
        {
            // Arrange

            SubnetCalcHelper helper = new();
            int input = 333;
            int expected = 512;

            // Act
            int testResult = helper.GetMinNeededHosts(input);

            // Assert
            Assert.AreEqual(expected, testResult);
        }

        [TestMethod]
        public void CalcHostbits_ReturnTrue()
        {
            // Arrange
            SubnetCalcHelper helper = new();
            int input = 8;
            int expected = 3;

            // Act
            double testResult = helper.CalcLogarithmus(input);

            // Assert
            Assert.AreEqual(expected, testResult);
        }

        [TestMethod]
        public void CalcNeededHostbits_ReturnTrue()
        {
            // Arrange

            SubnetCalcHelper helper = new();
            int input = 300;
            int expected = 9;

            // Act
            int testResult = helper.CalcNeededHostbits(input);

            // Assert
            Assert.AreEqual(expected, testResult);
        }

        [TestMethod]
        public void CalcSubnetmask_ReturnTrue()
        {
            // Arrange
            SubnetCalcHelper helper = new();

            int input = 8;
            string expected = "11111111.11111111.11111111.00000000";

            // Act
            string testResult = helper.CalcSubnetmask(input);

            // Assert
            Assert.AreEqual(expected, testResult);
        }

        [TestMethod]
        public void CalcAsymSubnet_ReturnTrue()
        {
            // Arrange
            IBinaryString binaryString = InitBinaryString();
            IParser parser = new BinaryParser(binaryString);
            var helper = new SubnetCalcHelper();
            IAsymIpAdressBuilder ipAdressBuilder = new AsymIpAdressBuilder();
            var svc = new AsymSubnetCalculator(helper, parser, binaryString, ipAdressBuilder);
            List<string> expected = new List<string>();
            string inputIp = "172.16.1.0";
            List<int> inputHostAmount = new List<int>
            {
                60,
                50,
                17,
                12
            };

            int inputSubnetAmount = 4;

            AsymSubnetEntity inputEntity = new()
            {
                IPAdress = parser.StringToBinary(inputIp),
                HostAmount = inputHostAmount,
                SubnetAmount = inputSubnetAmount,
            };

            expected.Add("172.16.1.1");
            expected.Add("172.16.1.62");
            expected.Add("172.16.1.65");
            expected.Add("172.16.1.126");
            expected.Add("172.16.1.129");
            expected.Add("172.16.1.158");
            expected.Add("172.16.1.161");
            expected.Add("172.16.1.174");


            // Act

            List<string> testResult = svc.CalcAvailableAsymSubnets(inputEntity);

            // Assert

            for (int i = 0; i < testResult.Count; i++)
            {
                Assert.AreEqual(expected[i], parser.BinaryToString(testResult[i]));
            }
        }

        [TestMethod]
        public void CalcAsymSubnetTwo_ReturnTrue()
        {
            // Arrange
            IBinaryString binaryString = InitBinaryString();
            IParser parser = new BinaryParser(binaryString);
            var helper = new SubnetCalcHelper();
            IAsymIpAdressBuilder ipAdressBuilder = new AsymIpAdressBuilder();
            var svc = new AsymSubnetCalculator(helper, parser, binaryString, ipAdressBuilder);
            List<string> expected = new List<string>();
            string inputIp = "192.168.35.0";
            List<int> inputHostAmount = new List<int>
            {
                60,
                30,
                15,
                12,
                13
            };

            int inputSubnetAmount = 5;

            AsymSubnetEntity inputEntity = new()
            {
                IPAdress = parser.StringToBinary(inputIp),
                HostAmount = inputHostAmount,
                SubnetAmount = inputSubnetAmount,
            };

            expected.Add("192.168.35.1");
            expected.Add("192.168.35.62");
            expected.Add("192.168.35.65");
            expected.Add("192.168.35.94");
            expected.Add("192.168.35.97");
            expected.Add("192.168.35.110");
            expected.Add("192.168.35.113");
            expected.Add("192.168.35.126");
            expected.Add("192.168.35.129");
            expected.Add("192.168.35.142");

            // Act

            List<string> testResult = svc.CalcAvailableAsymSubnets(inputEntity);

            // Assert

            for (int i = 0; i < testResult.Count; i++)
            {
                Assert.AreEqual(expected[i], parser.BinaryToString(testResult[i]));
            }
        }
    }
}