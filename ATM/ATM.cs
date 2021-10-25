using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_PROJ
{
    public class ATM : IATM
    {
        private int TotalCash { get; set; }

        private List<IAccount> Accounts = new List<IAccount>() { }; // Assumed that a record of accounts will be kept external like a database.

        public ATM(int amount)
        {
            TotalCash = amount;
        }

        public string Withdraw(int accountNumber, int amount)
        {
            var currentAccount = GetAccount(accountNumber);

            if (this.HasFunds(amount)) 
            {
                if (currentAccount.HasFunds(amount))
                {
                    TotalCash -= amount;
                    currentAccount.Withdraw(amount);
                }
                else
                {
                    return "FUNDS_ERR";
                }
            }
            else
            {
                return "ATM_ERR";
            }


            return currentAccount.Balance.ToString();
        }

        public void AddAccount(IAccount account)
        {
            Accounts.Add(account);
        }

        public bool HasFunds(int amount)
        {
            return (TotalCash - amount) >= 0;
        }

        public IAccount GetAccount(int accountNumber)
        {
            try
            {
                return Accounts
                    .Where(a => a.AccountNumber == accountNumber)
                    .Single();
            }
            catch
            {
                return null;
            }
        }

        public int GetBalance(int accountNumber)
        {
            var account = GetAccount(accountNumber);
            return account.Balance;
        }
    }
}
