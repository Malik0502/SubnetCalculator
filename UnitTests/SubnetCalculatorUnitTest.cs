using Service;

namespace UnitTests
{
    [TestClass]
    public class SubnetCalculatorUnitTest
    {
        [TestMethod]
        public void StringToBinary_IpAdress_ReturnTrue()
        {
            // Arrange

            var svc = new SubnetCalculator();
            string expected = "11000000.10101000.00000001.00000000";
            string input = "192.168.1.0";

            // Act

            string testResult = svc.StringToBinaryString(input);

            // Assert
            Assert.AreEqual(expected, testResult);
        }

        [TestMethod]
        public void StringToBinary_IpAdress_ReturnFalse()
        {
            // Arrange

            var svc = new SubnetCalculator();
            string expected = "11000000.10101000.00000001.00000000";
            string input = "10.168.1.0";

            // Act

            string testResult = svc.StringToBinaryString(input);

            // Assert
            Assert.AreNotEqual(expected, testResult);
        }

        [TestMethod]
        public void StringToBinary_Subnetmask_ReturnTrue()
        {
            // Arrange

            var svc = new SubnetCalculator();
            string expected = "11111111.11111111.11000000.00000000";
            string input = "255.255.192.0";

            // Act

            string testResult = svc.StringToBinaryString(input);

            // Assert
            Assert.AreEqual(expected, testResult);
        }

        [TestMethod]
        public void StringToBinary_Subnetmask_ReturnFalse()
        {
            // Arrange 

            var svc = new SubnetCalculator();
            string expected = "11111111.11111111.11000000.00000000";
            string input = "255.255.255.0";

            // Act

            string testResult = svc.StringToBinaryString(input);

            // Assert
            Assert.AreNotEqual(expected, input);

        }

        [TestMethod]
        public void BinaryToString_IpAdress_ReturnTrue()
        {
            // Arrange

            var svc = new SubnetCalculator();
            string expected = "192.168.1.0";
            string input = "11000000.10101000.00000001.00000000";

            // Act

            string testResult = svc.BinaryToString(input);

            // Assert

            Assert.AreEqual(expected, testResult);
        }

        [TestMethod]
        public void BinaryToString_IpAdress_ReturnFalse()
        {
            // Arrange

            var svc = new SubnetCalculator();
            string expected = "192.168.1.0";
            string input = "01111111.10101000.00000001.00000000";

            // Act

            string testResult = svc.BinaryToString(input);

            // Assert

            Assert.AreNotEqual(expected, testResult);
        }

        [TestMethod]
        public void BinaryToString_Subnetmask_ReturnTrue()
        {
            // Arrange

            var svc = new SubnetCalculator();
            string expected = "255.192.0.0";
            string input = "11111111.11000000.00000000.00000000";

            // Act

            string testResult = svc.BinaryToString(input);

            // Assert
            Assert.AreEqual(expected, testResult);
        }

        [TestMethod]
        public void BinaryToString_SubnetMask_ReturnFalse()
        {
            // Arrange

            var svc = new SubnetCalculator();
            string expected = "255.255.0.0";
            string input = "11111111.11000000.00000000.00000000";

            // Act

            string testResult = svc.BinaryToString(input);

            // Assert
            Assert.AreNotEqual(expected, testResult);
        }

        [TestMethod]
        public void CalcNetworkAdress_ReturnTrue()
        {
            // Arrange
            var svc = new SubnetCalculator();
            string expected = "00001000.00001000.00001000.00000000";
            string inputIp = "00001000.00001000.00001000.00001000";
            string inputSubnet = "11111111.11111111.11111111.11110000";

            // Act

            string testResult = svc.CalcNetworkAdressBinary(inputIp, inputSubnet);

            // Assert

            Assert.AreEqual(expected, testResult);
        }

        [TestMethod]
        public void CalNetworkAdress_ReturnFalse()
        {

            // Arrange
            var svc = new SubnetCalculator();
            string expected = "00001000.00001000.00001000.00100100";
            string inputIp = "00001000.00001000.00001000.00001000";
            string inputSubnet = "11111111.11111111.11111111.11110000";

            // Act

            string testResult = svc.CalcNetworkAdressBinary(inputIp, inputSubnet);

            // Assert

            Assert.AreNotEqual(expected, testResult);
        }

        [TestMethod]
        public void CalcLogarithmus_ReturnFalse(){
            // Arrange
            var svc = new SubnetCalculator();
            double expected = 3;
            int input = 8;

            // Act

            double testResult = svc.CalcLogarithmus(input);

            // Assert

            Assert.AreEqual(expected, testResult);
        }
    }
}