using ATM_PROJ;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ATM.TESTS
{
    [TestClass]
    public class ATMTest
    {
        [TestMethod]
        public void Withdraw_BeyondCapacity_ReturnsATMError()
        {
            // Arrange
            var atm = new ATM_PROJ.ATM(300);
            var account = new Account(1234, "500", "0", "12");
            atm.AddAccount(account);

            // Act
            var result = atm.Withdraw(1234, 500);

            // Assert
            Assert.AreEqual("ATM_ERR", result);
        }

        [TestMethod]
        public void Withdraw_WithinCapacity_ReturnsBalance ()
        {
            // Arrange
            var atm = new ATM_PROJ.ATM(300);
            var account = new Account(1234, "500", "0", "12");
            atm.AddAccount(account);

            // Act
            var result = atm.Withdraw(1234, 200);
            var balance = atm.GetBalance(1234);

            // Assert
            Assert.AreEqual(balance.ToString(), result);
        }

        [TestMethod]
        public void Withdraw_FromAccount_ReturnsCorrectBalance()
        {
            // Arrange
            var atm = new ATM_PROJ.ATM(300);
            var account = new Account(1234, "500", "0", "12");
            atm.AddAccount(account);

            // Act
            var result = atm.Withdraw(1234, 200);

            // Assert
            Assert.AreEqual("300", result);
        }

        [TestMethod]
        public void InsufficientAccountFunds_Returns_FUNDSERR()
        {
            // Arrange
            var atm = new ATM_PROJ.ATM(1000);
            var account = new Account(1000, "500", "0", "12");
            atm.AddAccount(account);

            // Act
            var result = atm.Withdraw(1000, 1000);

            // Assert
            Assert.AreEqual("FUNDS_ERR", result);
        }

        [TestMethod]
        public void SufficientATMFunds_HasFunds_Returns_True ()
        {
            // Arrange
            var atm = new ATM_PROJ.ATM(1000);

            // Act
            var result = atm.HasFunds(600);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void InufficientATMFunds_HasFunds_Returns_False()
        {
            // Arrange
            var atm = new ATM_PROJ.ATM(1000);

            // Act
            var result = atm.HasFunds(1200);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GetAccount_ReturnsCorrectAccount()
        {
            // Arrange
            var atm = new ATM_PROJ.ATM(1000);
            var account1 = new Account(1000, "500", "0", "12");
            var account2 = new Account(1234, "0", "0", "12");
            atm.AddAccount(account1);
            atm.AddAccount(account2);

            // Act
            var result1 = atm.GetAccount(1000);
            var result2 = atm.GetAccount(1234);

            // Assert
            Assert.AreEqual(account1, result1);
            Assert.AreEqual(account2, result2);
        }

        [TestMethod]
        public void WhenNoAccountExists_GetAccount_ReturnsNull()
        {
            // Arrange
            var atm = new ATM_PROJ.ATM(1000);

            // Act
            var result = atm.GetAccount(1000);

            // Assert
            Assert.IsNull(result);
        }
    }
}
