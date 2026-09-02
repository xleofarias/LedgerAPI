public class Account
{
    public Guid Id { get;} = Guid.NewGuid();
    public string AccountNumber { get; private set; } 
    public required string AccountHolder { get; set; }
    public decimal Balance { get; private set; } = 0;
    public DateTime CreatedAt { get; } = DateTime.UtcNow;
    public string Status { get; private set; } = "Active";

    public Account(string accountNumber)
    {
        AccountNumber = accountNumber;
    }
}