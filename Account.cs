public class Account
{
    public Guid Id { get;} = Guid.NewGuid();
    public int AccountNumber { get; set; } 
    public required string AccountHolder { get; set; }
    public decimal Balance { get; private set; } = 0;
    public DateTime CreatedAt { get; } = DateTime.UtcNow;
    public string Status { get; set; } = "Active";
    public void Deposit(decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Deposit amount must be positive.");
        }

        Balance += amount;
    }

    public void Withdraw(decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Withdrawal amount must be positive.");
        }

        if (amount > Balance)
        {
            throw new InvalidOperationException("Insufficient funds.");
        }

        Balance -= amount;
    }
}