# ATM App

To run this app open the sln file in Visual Studio and Run, or in the command line from this directory run `dotnet run ATM.sln`.

This app is entirely based on the cli. All inputs are done through the cli, and the responses show on the next line.

In my version when an account is created it is stored so that it may be re-accessed if given the same account number and correct pin.

## How to use this

The format is as follows:

Line 1: Enter the amount the ATM should hold.
Line 2: Intentionally leave this line blank and press `Enter`.

Line 3: Here input the Account number, Test pin, and Actual pin. Once the account is already created, you will be able to access it again anytime during the lifetime of the app. Just re-enter the account number, any number can go in the test pin slot, and finally the Pin set from earlier.
`123456 2233 2233`

Line 4: Enter the Balance and Overdraft amounts. If your re-accessing an existing account, what you enter here does not matter.

Line 5: Enter the option. B to view balance, W to withdraw.

After Line 5, you can view balance or withdraw again, to exit this account simply press enter without typing anything. You should now be able to enter an account number again to access a another account.

You are able to switch between using the console app and inputing a text file. To use a text file, place the file into the Inputs folder. Now navigate using the command line to bin/debug/ and run the command `dotnet run ATM.exe Inputs/[your file name].txt`.

Example:

```
-- New ATM --
8000

12345678 1234 1234
500 100
B
> 500
W 100
> 400

87654321 4321 4321
100 0
W 10
> 90

87654321 0 4321 (Accesses the same account as above, given pin is valid, third input)
0 0
W 10
> 80

10000000 123 123
0 0
W 10
> FUNDS_ERR
B
> 0

87654321 1111 1111 (Incorrect pin for the same account made earlier)
0 0
> ACCOUNT_ERR
```


### Update: 27/10/2021
I have also added the ability for it to read from txt files. You can modify the source in program.cs. `app.Start()` overload with no inputs will bring the console app. No pre-existing code was altered.

### Update: 30/10/2021
I have made the path to the txt files local so that it works on other machines and I have removed unused usings. No pre-existing code was altered.
