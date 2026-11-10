using System.Net.Http.Json;
using LedgerAPI.Tests;
using LedgerAPI.API.Extensions;
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
    public async Task CreateAccount_WithValidPayload_ReturnsCreated()
    {
        // Arrange
        var accountNumber = GenerateAccountNumber.CreateAccountNumber();
        var payload = new
        {
            AccountHolder = "Test Account",
            AccountNumber = accountNumber
        };

        // Act    
        var response = await _httpClient.PostAsJsonAsync("/api/account", payload, cancellationToken : default);

        // Assert
        Assert.Equal(System.Net.HttpStatusCode.Created, response.StatusCode);
    }
}