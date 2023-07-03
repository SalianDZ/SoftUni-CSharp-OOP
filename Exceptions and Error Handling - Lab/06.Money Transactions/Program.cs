string[] tokens = Console.ReadLine().Split(",");
Dictionary<int, double> bankAccounts = new Dictionary<int, double>();

for (int i = 0; i < tokens.Length; i++)
{
    string[] innerTokens = tokens[i].Split("-");
    int bankAcccount = int.Parse(innerTokens[0]);
    double bankBalance = double.Parse(innerTokens[1]);

    bankAccounts[bankAcccount] = bankBalance;
}

while (true)
{
    string[] command = Console.ReadLine().Split();
    if (command[0] == "End")
    {
        break;
    }

    int accountNumber = int.Parse(command[1]);
    double sum = double.Parse(command[2]);

    try
	{
        if (command[0] == "Deposit")
        {
            if (bankAccounts.ContainsKey(accountNumber))
            {
                bankAccounts[accountNumber] += sum;
                Console.WriteLine($"Account {accountNumber} has new balance: {bankAccounts[accountNumber]:f2}");
                Console.WriteLine($"Enter another command");
            }
            else
            {
                throw new ArgumentException("Invalid account!");
            }
        }
        else if (command[0] == "Withdraw")
        {
            if (bankAccounts.ContainsKey(accountNumber))
            {
                if (bankAccounts[accountNumber] < sum)
                {
                    throw new ArgumentException("Insufficient balance!");
                }
                else
                {
                    bankAccounts[accountNumber] -= sum;
                    Console.WriteLine($"Account {accountNumber} has new balance: {bankAccounts[accountNumber]:f2}");
                    Console.WriteLine($"Enter another command");
                }
            }
            else
            {
                throw new ArgumentException("Invalid account!");
            }
        }
        else
        {
            throw new ArgumentException("Invalid command!");
        }
    }
	catch (ArgumentException ex)
	{
        Console.WriteLine(ex.Message);
        Console.WriteLine("Enter another command");
	}
}