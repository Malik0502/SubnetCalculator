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
    }
}