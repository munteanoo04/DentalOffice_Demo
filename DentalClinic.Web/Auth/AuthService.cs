using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace DentalClinic.Web.Auth;

public class AuthService
{
    private readonly IHttpClientFactory _factory;

    public string? Token { get; private set; }
    public string? Role { get; private set; }

    public bool IsLoggedIn => !string.IsNullOrWhiteSpace(Token);
    public bool IsAdmin => Role == "Admin";

    public AuthService(IHttpClientFactory factory)
    {
        _factory = factory;
    }

    public async Task<bool> RegisterAsync(
        string firstName,
        string lastName,
        string email,
        string phone,
        string password)
    {
        var http = _factory.CreateClient("api");

        var response = await http.PostAsJsonAsync("api/auth/register", new
        {
            firstName,
            lastName,
            email,
            phoneNumber = phone,
            password
        });

        return response.IsSuccessStatusCode;
    }

    public async Task<bool> LoginAsync(string email, string password)
    {
        var http = _factory.CreateClient("api");

        var response = await http.PostAsJsonAsync("api/auth/login", new
        {
            email,
            password
        });

        if (!response.IsSuccessStatusCode)
            return false;

        var result = await response.Content.ReadFromJsonAsync<LoginResponse>();

        Token = result?.Token;
        Role = result?.Role;

        return !string.IsNullOrWhiteSpace(Token);
    }

    public HttpClient CreateAuthorizedClient()
    {
        var client = _factory.CreateClient("api");

        if (!string.IsNullOrWhiteSpace(Token))
        {
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Token);
        }

        return client;
    }

    public void Logout()
    {
        Token = null;
        Role = null;
    }
}

public record LoginResponse(string Token, string Role);