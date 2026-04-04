using System.Net.Http.Headers;
using DentalClinic.Web.Auth;
using DentalClinic.Web.Models;

namespace DentalClinic.Web.Services;

public class PatientService
{
    private readonly HttpClient _http;
    private readonly AuthService _auth;

    public PatientService(HttpClient http, AuthService auth)
    {
        _http = http;
        _auth = auth;
    }

    private void AttachToken() =>
        _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _auth.Token);

    public async Task<List<PatientDto>> GetAllAsync()
    {
        AttachToken();
        return await _http.GetFromJsonAsync<List<PatientDto>>("api/patients")
               ?? new List<PatientDto>();
    }

    public async Task<bool> CreateAsync(CreatePatientRequest req)
    {
        AttachToken();
        var response = await _http.PostAsJsonAsync("api/patients", req);
        return response.IsSuccessStatusCode;
    }
}

public class CreatePatientRequest
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
}