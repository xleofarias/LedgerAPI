using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using LedgerAPI.Tests;
public class AccountApiTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _factory;
    private readonly HttpClient _httpClient;

    public AccountApiTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
        _httpClient = factory.CreateClient();
    }

    [Fact]
    public async Task Account_With_Valid_Data_Can_Be_Created()
    {
        // Arrange
        var payload = new
        {
            AccountHolder = "Test Account",
            AccountNumber = "123ABC-DEF457"
        };

        // Act    
        var response = await _httpClient.PostAsJsonAsync("/api/account", payload, cancellationToken : default);
        
        response.EnsureSuccessStatusCode();

        // Assert
        Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
    }
}