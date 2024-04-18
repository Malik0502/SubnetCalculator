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

            var svc = new SubnetCalculator();

            // Act

            int testResult = svc.StringToBinary("6");

            // Assert
            Assert.AreEqual(110, testResult);
        }

        [TestMethod]
        public void StringToBinary_ReturnFalse()
        {
            // Arrange

            var svc = new SubnetCalculator();

            // Act

            // Assert
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