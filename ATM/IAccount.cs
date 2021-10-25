namespace ATM_PROJ
{
    public interface IAccount
    {
        int Balance { get; }
        int AccountNumber { get; set; }

        bool HasFunds(int amount);
        string VerifyPin(int pin);
        string Withdraw(int amount);
    }
}