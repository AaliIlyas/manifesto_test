using System;
using System.IO;

namespace ATM_PROJ
{
    public class App
    {
        public void Start()
        {
            Console.WriteLine("-- New ATM --");
            var atmAmount = Console.ReadLine(); // READLINE

        newAtm:
            Console.ReadLine();

            IATM atm;
            if (int.TryParse(atmAmount, out var res))
            {
                atm = new ATM(res);
            }
            else
            {
                Console.WriteLine($"Invalid input: '{atmAmount}', please enter only a number");
                goto newAtm;
            }

        UserAccount:
            var accountAndPin = Console.ReadLine().Split(" "); // READLINE

            int accountNumber;
            try
            {
                accountNumber = int.Parse(accountAndPin[0]);
            }
            catch
            {
                Console.WriteLine("Invalid account number. Re-enter account number and pin.");
                goto UserAccount;
            }

            var storedPin = accountAndPin[1];
            var pin = accountAndPin[2];

            var balanceAndOverdraft = Console.ReadLine().Split(" "); // READLINE
            var unverified = CreateOrVerifyPinAndAccount(atm, accountNumber, pin, storedPin, balanceAndOverdraft);

            if (unverified == null)
            {
                while (true) // Allows for multiple queries unless new empty line occurs
                {
                    var option = Console.ReadLine(); // READLINE

                    if (option == "")
                    {
                        break;
                    }

                    var result = ViewBalanceOrWithdraw(atm, accountNumber, option);
                    Console.WriteLine(result);
                }
            }
            else
            {
                Console.WriteLine(unverified);
            }

            goto UserAccount;
        }

        public void Start(string path) // This start function reads from txt file
        {
            var Lines = File.ReadAllLines(path);

            IATM atm = null;
            var flag = true;
            string[] accountAndPin = null;
            string[] balanceAndOverdraft = null;
            var verified = false;

            foreach (var line in Lines)
            {
                if (int.TryParse(line, out var atmAmount))
                {
                    atm = new ATM(atmAmount);
                    flag = false;
                }

                if (flag)
                {
                    flag = true;
                    continue;
                }

                if (line.Split(" ").Length == 3 && atm != null)
                {
                    accountAndPin = line.Split(" ");
                }

                if (line.Split(" ").Length == 2 && atm != null)
                {
                    balanceAndOverdraft = line.Split(" ");
                }

                if (atm != null && accountAndPin != null && balanceAndOverdraft != null)
                {
                    string result = null;
                    try
                    {
                        result = CreateOrVerifyPinAndAccount(atm, int.Parse(accountAndPin[0]), accountAndPin[2], accountAndPin[1], balanceAndOverdraft);

                        if (result == "ACCOUNT_ERR")
                        {
                            throw new Exception();
                        } 
                        else
                        {
                            verified = true;
                        }
                    }
                    catch
                    {
                        if (result != null)
                        {
                            Console.WriteLine(result);
                            accountAndPin = null;
                            balanceAndOverdraft = null;
                        }
                    }
                }

                if (verified)
                {
                    if (line == "B")
                    {
                        var output = ViewBalanceOrWithdraw(atm, int.Parse(accountAndPin[0]), "B");
                        if (output != null)
                        {
                            Console.WriteLine(output);
                        }
                    }

                    if (line.Split(" ")[0] == "W")
                    {
                        var output = ViewBalanceOrWithdraw(atm, int.Parse(accountAndPin[0]), line);
                        if (output != null)
                        {
                            Console.WriteLine(output);
                        }
                    }

                    if (line == "")
                    {
                        verified = false;
                        accountAndPin = null;
                        balanceAndOverdraft = null;
                    }
                }
            }
        }

        public string CreateOrVerifyPinAndAccount(IATM atm, int accountNumber, string pin, string storedPin, string[] balanceAndOverdraft)
        {
            var balance = balanceAndOverdraft[0];
            var overdraft = balanceAndOverdraft[1];

            var account = atm.GetAccount(accountNumber);
            if (account == null)
            {
                if (storedPin == pin)
                {
                    var newAccount = new Account(accountNumber, balance, overdraft, pin);
                    atm.AddAccount(newAccount);
                } 
                else
                {
                    return "ACCOUNT_ERR";
                }
            }
            else
            {
                var verificationOutput = account.VerifyPin(int.Parse(pin));
                if (verificationOutput == "ACCOUNT_ERR")
                {
                    return verificationOutput;
                }
            }
            return null;
        }

        public string ViewBalanceOrWithdraw(IATM atm, int accountNumber, string option)
        {
            string output;
            if (option == "B")
            {
                var currentBalance = atm.GetBalance(accountNumber);
                output = currentBalance.ToString();
            }
            else if (option.Split(" ")[0] == "W")
            {
                try
                {
                    var withdrawal = option.Split(" ")[1];
                    output = atm.Withdraw(accountNumber, int.Parse(withdrawal));
                }
                catch
                {
                    output = $"Value entered is not a valid number, or blank. Correct format example: 'W 10'.";
                }
            }
            else
            {
                output = $"Input '{option}' is not valid. Type 'B' for to view Balance, or W followed by number to withdraw eg: 'W 10'.";
            }

            return output;
        }
    }
}
