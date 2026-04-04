using System.Net.Http.Json;

namespace DentalClinic.Web.Auth;

public class AuthService
{
    private readonly IHttpClientFactory _factory;
    public string? Token { get; private set; }
    public bool IsLoggedIn => !string.IsNullOrEmpty(Token);

    public AuthService(IHttpClientFactory factory) => _factory = factory;

    public async Task<bool> LoginAsync(string email, string password)
    {
        var http = _factory.CreateClient("auth");

        var response = await http.PostAsJsonAsync(
            "api/auth/login",
            new { email, password });

        if (!response.IsSuccessStatusCode) return false;

        var result = await response.Content.ReadFromJsonAsync<TokenResponse>();
        Token = result?.Token;
        return true;
    }

    public void Logout() => Token = null;
}

public record TokenResponse(string Token);