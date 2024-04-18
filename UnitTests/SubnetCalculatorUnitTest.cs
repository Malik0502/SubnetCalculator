using Service;

namespace UnitTests
{
    [TestClass]
    public class SubnetCalculatorUnitTest
    {
        [TestMethod]
        public void StringToBinary_ReturnTrue()
        {
            // Arrange

            var svc = new SubnetCalculator().StringToBinary("192.168.1.0");

            // Act

            // Assert
        }

        [TestMethod]
        public void StringToBinary_ReturnFalse()
        {
            // Arrange

            var svc = new SubnetCalculator().StringToBinary("192.168.1.0");

            // Act

            // Assert
        }

        [TestMethod]
        public void BinaryToString_ReturnTrue()
        {
            // Arrange

            // Act

            // Assert
        }

        [TestMethod]
        public void BinaryToString_ReturnFalse()
        {
            // Arrange

            // Act

            // Assert
        }
    }
}