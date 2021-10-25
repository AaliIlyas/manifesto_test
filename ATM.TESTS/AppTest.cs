using ATM_PROJ;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace ATM.TESTS
{
    [TestClass]
    public class AppTest
    {
        // Arrange
        public App Application = new App();
        public IATM Atm = new ATM_PROJ.ATM(5000);
        public IAccount account = new Account(1234, "200", "0", "4312");

        public AppTest()
        {
            Atm.AddAccount(account);
        }

        [TestMethod]
        public void IfAccountDoesNotExist_NewAccountIsCreated_ReturnsNull()
        {
            // Act
            var result = Application.CreateOrVerifyPinAndAccount(Atm, 50001, "123", "123", new string[] { "300", "0" });
            var newAccount = Atm.GetAccount(50001);

            // Assert

            Assert.IsNull(result);
            Assert.IsNotNull(newAccount);
            Assert.AreEqual(300, newAccount.Balance);
        }

        [TestMethod]
        public void IfAccountExists_IncorrectPin_Verify_Returns_ACCOUNT_ERR()
        {
            // Act
            var result = Application.CreateOrVerifyPinAndAccount(Atm, 1234, "200", "000", new string[] { "300", "0" });

            // Assert
            Assert.AreEqual("ACCOUNT_ERR", result);
        }

        [TestMethod]
        public void IfAccountExists_VerifyValidPin_Returns_Null()
        {
            // Act
            var result = Application.CreateOrVerifyPinAndAccount(Atm, 1234, "4312", "0", new string[] { "300", "0" });

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void ValidWithdraw_Returns_CorrectBalance()
        {
            // Act
            var result = Application.ViewBalanceOrWithdraw(Atm, 1234, "W 10");

            // Assert
            Assert.AreEqual("190", result);
        }

        [TestMethod]
        public void Withdrawing_BeyondAccountFunds_ReturnsFundsError()
        {
            // Act
            var result = Application.ViewBalanceOrWithdraw(Atm, 1234, "W 500");

            // Assert
            Assert.AreEqual("FUNDS_ERR", result);
        }

        [TestMethod]
        public void Withdrawing_BeyondATMCapacity_ReturnsATMError()
        {
            // Act
            var result = Application.ViewBalanceOrWithdraw(Atm, 1234, "W 7000");

            // Assert
            Assert.AreEqual("ATM_ERR", result);
        }
    }
}
