namespace LedgerAPI.Tests;

using Xunit;
using LedgerAPI.API;
using LedgerAPI.API.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

public class AccountTests
{
    [Fact]
    public async Task Account_Can_Be_Created()
    {
        var serviceProvider = CustomBuildServiceProvider.CreateBuildServiceProvider();

        // Id Account to be created
        Guid accountId;
        string accountNumber;
        decimal balance;
        string status;

        // Create a scope to resolve the DbContext and Account service
        using (var scope = serviceProvider.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            
            // Create a new account
            var newAccount = new Account(GenerateAccountNumber.CreateAccountNumber()){
                AccountHolder = "Test Account"
            };

            // Store the account ID for later retrieval
            accountId = newAccount.Id;
            accountNumber = newAccount.AccountNumber;
            balance = newAccount.Balance;
            status = newAccount.Status;

            // Add the account to the DbContext and save changes
            dbContext.Accounts.Add(newAccount);
            await dbContext.SaveChangesAsync();
        }

        // Create a new scope to retrieve the account
        using (var scope = serviceProvider.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            // Retrieve an account by account ID
            var accountIdToRetrieve = accountId;

            var retrievedAccount = await dbContext.Accounts.FirstOrDefaultAsync(a => a.Id == accountIdToRetrieve);

            // Assert that the account was retrieved successfully
            Assert.NotNull(retrievedAccount);
            Assert.Equal(accountIdToRetrieve, retrievedAccount.Id);
            Assert.Equal(accountNumber, retrievedAccount.AccountNumber);
            Assert.Equal(balance, retrievedAccount.Balance);
            Assert.Equal(status, retrievedAccount.Status);
        }
    }


    [Fact()]
    public async Task Account_WhenCreated_HasZeroBalance() {

        var serviceProvider = CustomBuildServiceProvider.CreateBuildServiceProvider();

        // Id Account to be created
        Guid accountId;
        decimal balance = 0;

        // Create a scope to resolve the DbContext and Account service
        using (var scope = serviceProvider.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            
            // Create a new account
            var newAccount = new Account(GenerateAccountNumber.CreateAccountNumber()){
                AccountHolder = "Test Account"
            };

            // Store the account ID for later retrieval
            accountId = newAccount.Id;

            // Add the account to the DbContext and save changes
            dbContext.Accounts.Add(newAccount);
            await dbContext.SaveChangesAsync();
        }

        // Create a new scope to retrieve the account
        using (var scope = serviceProvider.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            // Retrieve an account by account ID
            var accountIdToRetrieve = accountId;

            var retrievedAccount = await dbContext.Accounts.FirstOrDefaultAsync(a => a.Id == accountIdToRetrieve);

            // Assert that the account was retrieved successfully
            Assert.NotNull(retrievedAccount);
            Assert.Equal(accountIdToRetrieve, retrievedAccount.Id);
            Assert.Equal(balance, retrievedAccount.Balance);
        }
    }

    [Fact]
    public async Task Account_WhenCreated_HasActiveStatus() {

        var serviceProvider = CustomBuildServiceProvider.CreateBuildServiceProvider();

        // Id Account to be created
        Guid accountId;
        string status = "Active";

        // Create a scope to resolve the DbContext and Account service
        using (var scope = serviceProvider.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            
            // Create a new account
            var newAccount = new Account(GenerateAccountNumber.CreateAccountNumber()){
                AccountHolder = "Test Account"
            };

            // Store the account ID for later retrieval
            accountId = newAccount.Id;

            // Add the account to the DbContext and save changes
            dbContext.Accounts.Add(newAccount);
            await dbContext.SaveChangesAsync();
        }

        // Create a new scope to retrieve the account
        using (var scope = serviceProvider.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            // Retrieve an account by account ID
            var accountIdToRetrieve = accountId;

            var retrievedAccount = await dbContext.Accounts.FirstOrDefaultAsync(a => a.Id == accountIdToRetrieve);

            // Assert that the account was retrieved successfully
            Assert.NotNull(retrievedAccount);
            Assert.Equal(accountIdToRetrieve, retrievedAccount.Id);
            Assert.Equal(status, retrievedAccount.Status);
        }
    }
}
