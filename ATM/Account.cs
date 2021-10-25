using System;

namespace ATM_PROJ
{
    public class Account : IAccount
    {
        public int Balance { get; private set; }
        private int OverDraft { get; set; }
        private int TotalFunds { get; set; }
        public int AccountNumber { get; set; }
        private int Pin { get; set; }

        public Account(int accountNumber, string balance, string overDraft, string pin)
        {
            AccountNumber = accountNumber;
            Balance = int.Parse(balance);
            OverDraft = int.Parse(overDraft);
            Pin = int.Parse(pin);
            TotalFunds = Balance + OverDraft;
        }

        public string Withdraw(int amount)
        {
            Balance -= amount;

            if (Balance < 0)
            {
                OverDraft -= Math.Abs(Balance);
                Balance = 0;
            }

            TotalFunds -= amount;
            return Balance.ToString();
        }

        public bool HasFunds(int amount)
        {
            return (TotalFunds - amount) >= 0;
        }

        public string VerifyPin(int pin)
        {
            if (Pin == pin)
            {
                return null;
            } 
            else
            {
                return "ACCOUNT_ERR";
            }
        }
    }
}