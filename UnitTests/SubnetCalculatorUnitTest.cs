using Service;
using System.Numerics;

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

            string testResult = svc.StringToBinary(input);

            // Assert
            Assert.AreEqual(expected, testResult);
        }

        [TestMethod]
        public void StringToBinary_Subnetmask_ReturnTrue()
        {
            // Arrange

            var svc = new SubnetCalculator();
            string expected = "11111111.11111111.11000000.00000000";
            string input = "255.255.192.0";

            // Act

            string testResult = svc.StringToBinary(input);

            // Assert
            Assert.AreEqual(expected, testResult);
        }

        [TestMethod]
        public void BinaryToString_ReturnTrue()
        {
            // Arrange

            var svc = new SubnetCalculator();

            // Act

            // Assert
        }

        [TestMethod]
        public void BinaryToString_ReturnFalse()
        {
            // Arrange

            var svc = new SubnetCalculator();

            // Act

            // Assert
        }
    }
}