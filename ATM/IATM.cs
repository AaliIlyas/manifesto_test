namespace ATM_PROJ
{
    public interface IATM
    {
        void AddAccount(IAccount account);
        IAccount GetAccount(int accountNumber);
        int GetBalance(int accountNumber);
        bool HasFunds(int amount);
        string Withdraw(int accountNumber, int amount);
    }
}