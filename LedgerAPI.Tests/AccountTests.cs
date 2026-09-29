namespace LedgerAPI.Tests;

using Xunit;
using LedgerAPI.API;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using DotNetEnv;

public class AccountTests
{
    [Fact]
    public async Task Account_Can_Be_Created()
    {
        var directory = Directory.GetCurrentDirectory();
        var directoryFather = Path.GetDirectoryName(directory) 
            ?? throw new InvalidOperationException("Could not determine the parent directory.");
        var directoryEnv = Path.Combine(directoryFather, ".env");

        // Load environment variables from .env file
        Env.Load(directoryEnv);

        // Set up the service collection and configure the DbContext for testing
        var services = new ServiceCollection();
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(Environment.GetEnvironmentVariable("ConnectionSqlServerTests") ?? throw new InvalidOperationException("Connection string not found.")));

        // Build the service provider
        var serviceProvider = services.BuildServiceProvider();

        // Id Account to be created
        Guid accountId;

        // Create a scope to resolve the DbContext and Account service
        using (var scope = serviceProvider.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            // Create a new account
            var newAccount = new Account("123ABC-DEF456"){
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
        }
    }
}
