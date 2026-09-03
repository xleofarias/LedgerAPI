using System.Text.RegularExpressions;

public class Account
{
    public Guid Id { get;} = Guid.NewGuid();
    public string AccountNumber { get; private set; } 
    public string? AccountHolder { get; set; }
    public decimal Balance { get; private set; } = 0;
    public string Status { get; private set; } = "Active";

    public Account(string accountNumber)
    {
        string pattern = "^[0-9]{3}[A-Z]{3}[-]{1}[A-Z]{3}[0-9]{3}$";

        Regex rg = new Regex(pattern);

        if(String.IsNullOrWhiteSpace(accountNumber)){
            throw new ArgumentException("É necessário preencher com valores");
        }

        if(!rg.IsMatch(accountNumber)){
            throw new ArgumentException("Número de conta inválido");
        }

        AccountNumber = accountNumber;
    }
}
