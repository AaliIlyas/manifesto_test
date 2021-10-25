using ATM_PROJ;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ATM.TESTS
{
    [TestClass]
    public class AccountTest
    {
        [TestMethod]
        public void WhenSufficientFunds_MathodHasFunds_ReturnsTrue()
        {
            // Arrange
            var account = new Account(12345, "400", "100", "4321");

            // Act
            var result = account.HasFunds(20);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void WhenInSufficientFunds_MathodHasFunds_ReturnsFalse()
        {
            // Arrange
            var account = new Account(12345, "400", "100", "4321");

            // Act
            var result = account.HasFunds(600);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CorrectPin_ReturnsNull()
        {
            // Arrange
            var account = new Account(12345, "400", "100", "4321");

            // Act
            var result = account.VerifyPin(4321);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void IncorrectPin_Returns_ACCOUNT_ERR()
        {
            // Arrange
            var account = new Account(12345, "400", "100", "4321");

            // Act
            var result = account.VerifyPin(200);

            // Assert
            Assert.AreEqual("ACCOUNT_ERR", result);
        }

        [TestMethod]
        public void WithdrawAmount_ReturnsCorrectBalance ()
        {
            // Arrange
            var account = new Account(12345, "400", "100", "4321");

            // Act
            var result = account.Withdraw(200);

            // Assert
            Assert.AreEqual("200", result);
        }
    }
}
