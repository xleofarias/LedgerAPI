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

    [Fact()]
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

    [Fact()]
    public async Task CreateAccount_WithInvalidNumber_ReturnsBadRequest()
    {
        // Arrange
        var payload = new
        {
            AccountHolder = "Test Account",
            AccountNumber = "12345678901234" // Invalid account number (14 digits)
        };

        // Act    
        var response = await _httpClient.PostAsJsonAsync("/api/account", payload, cancellationToken : default);

        // Assert
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact()]
    public async Task CreateAccount_WithDuplicateAccountNumber_ReturnsBadRequest()
    {
        // Arrange
        var accountNumber = GenerateAccountNumber.CreateAccountNumber();
        var payload = new
        {
            AccountHolder = "Test Account",
            AccountNumber = accountNumber
        };

        // Act    
        var response1 = await _httpClient.PostAsJsonAsync("/api/account", payload, cancellationToken : default);
        var response2 = await _httpClient.PostAsJsonAsync("/api/account", payload, cancellationToken : default);

        // Assert
        Assert.Equal(System.Net.HttpStatusCode.Created, response1.StatusCode);
        Assert.Equal(System.Net.HttpStatusCode.Conflict, response2.StatusCode);
    }
}