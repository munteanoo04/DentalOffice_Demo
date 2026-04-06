using System.Net.Http.Json;
using DentalClinic.Web.Auth;
using DentalClinic.Web.Models;

namespace DentalClinic.Web.Services;

public class PatientService
{
    private readonly AuthService _auth;
    public PatientService(AuthService auth) => _auth = auth;

    public async Task<List<PatientDto>> GetAllAsync()
    {
        var http = _auth.CreateAuthorizedClient();
        return await http.GetFromJsonAsync<List<PatientDto>>("api/patients")
               ?? new List<PatientDto>();
    }

    public async Task<bool> CreateAsync(CreatePatientRequest req)
    {
        var http = _auth.CreateAuthorizedClient();
        var response = await http.PostAsJsonAsync("api/patients", req);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var http = _auth.CreateAuthorizedClient();
        var response = await http.DeleteAsync($"api/patients/{id}");
        return response.IsSuccessStatusCode;
    }
}