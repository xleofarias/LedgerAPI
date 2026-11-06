using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
public class AccountApiTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly HttpClient _httpClient;

    public AccountApiTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _httpClient = factory.CreateClient();
    }

    [Fact]
    public async Task Account_With_Valid_Data_Can_Be_Created()
    {
        // Arrange
        var payload = "{}";

        // Act
        
    
        // Assert
    }
}