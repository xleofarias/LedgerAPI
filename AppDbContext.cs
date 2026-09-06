using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<Account> Accounts { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<Account>().HasKey(a => a.Id);
        modelBuilder.Entity<Account>().Property(a => a.Balance).HasPrecision(18, 2);
        modelBuilder.Entity<Account>().Property(a => a.AccountNumber).HasMaxLength(10);
        modelBuilder.Entity<Account>().Property(a => a.Status).HasMaxLength(10);
        modelBuilder.Entity<Account>().Property(a => a.AccountHolder).HasMaxLength(100);
    }
}
