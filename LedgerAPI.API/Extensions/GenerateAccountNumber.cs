namespace LedgerAPI.API.Extensions;
public static class GenerateAccountNumber
{
    public static string CreateAccountNumber()
    {
        string bloco1 = GenerateRandomInt(3);
        string bloco2 = GenerateRandomString(3);
        string bloco3 = GenerateRandomString(3);
        string bloco4 = GenerateRandomInt(3);
        string traco = "-";
        string result = string.Concat(bloco1, bloco2, traco, bloco3, bloco4);

        return result;
    }

    public static string GenerateRandomString(int length)
    {
        Random random = new Random();
        char[] result = new char[length];
        string allowedChars = "QWERTYUIOPASDFGHJKLZXCVBNM";

        for (int i = 0; i < length; i++)
        {
            int index = random.Next(allowedChars.Length);
            result[i] = allowedChars[index];
        
        }

        return new string(result);
    }

    public static string GenerateRandomInt(int length)
    {
        Random random = new Random();
        char[] result = new char[length];
        string allowedInt = "0123456789";

        for (int i = 0; i < length; i++)
        {
            int index = random.Next(allowedInt.Length);
            result[i] = allowedInt[index];
        
        }

        return new string(result);
    }
}