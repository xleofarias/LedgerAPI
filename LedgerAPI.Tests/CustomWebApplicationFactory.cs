using Microsoft.AspNetCore.Mvc.Testing;

namespace LedgerAPI.Tests;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        
    }
    
}