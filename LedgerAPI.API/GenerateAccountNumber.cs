public class GenerateAccountNumber
{
    public static string Generate()
    {
        Random random = new Random();
        string accountNumber = "";

        for (int i = 0; i < 3; i++)
        {
            accountNumber += random.Next(0, 10).ToString();
        }

        for (int i = 0; i < 3; i++)
        {
            accountNumber += (char)random.Next('A', 'Z' + 1);
        }

        accountNumber += "-";

        for (int i = 0; i < 3; i++)
        {
            accountNumber += (char)random.Next('A', 'Z' + 1);
        }

        for (int i = 0; i < 3; i++)
        {
            accountNumber += random.Next(0, 10).ToString();
        }

        return accountNumber;
    }
}