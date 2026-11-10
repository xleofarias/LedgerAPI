namespace LedgerAPI.Tests;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using LedgerAPI.API;

public static class CustomBuildServiceProvider
{
    public static ServiceProvider CreateBuildServiceProvider()
    {
        // Load Env;
        CustomLoadEnv.LoadEnv();

        // Set up the service collection and configure the DbContext for testing
        var services = new ServiceCollection();
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(Environment.GetEnvironmentVariable("ConnectionSqlServerTests") ?? throw new InvalidOperationException("Connection string not found.")));

        // Build the service provider
        var serviceProvider = services.BuildServiceProvider();

        return serviceProvider;
    }
}