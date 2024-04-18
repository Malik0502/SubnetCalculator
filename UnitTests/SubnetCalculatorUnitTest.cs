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

            var svc = new SubnetCalculator().BinaryToString(00000000000000000000000000000000);

            // Act

            // Assert
        }

        [TestMethod]
        public void BinaryToString_ReturnFalse()
        {
            // Arrange

            var svc = new SubnetCalculator().BinaryToString(00000000000000000000000000000000);

            // Act

            // Assert
        }
    }
}