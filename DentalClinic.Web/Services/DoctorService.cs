using System.Net.Http.Headers;
using System.Net.Http.Json;
using DentalClinic.Web.Auth;
using DentalClinic.Web.Models;

namespace DentalClinic.Web.Services;

public class DoctorService
{
    private readonly HttpClient _http;
    private readonly AuthService _auth;

    public DoctorService(HttpClient http, AuthService auth)
    {
        _http = http;
        _auth = auth;
    }

    private void AttachToken() =>
        _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _auth.Token);

    public async Task<List<DoctorDto>> GetAllAsync()
    {
        AttachToken();
        return await _http.GetFromJsonAsync<List<DoctorDto>>("api/doctors")
               ?? new List<DoctorDto>();
    }

    public async Task<bool> CreateAsync(CreateDoctorRequest req)
    {
        AttachToken();
        var response = await _http.PostAsJsonAsync("api/doctors", req);
        return response.IsSuccessStatusCode;
    }
}

public class CreateDoctorRequest
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Specialization { get; set; } = string.Empty;
}