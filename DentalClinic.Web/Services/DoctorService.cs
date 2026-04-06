using System.Net.Http.Json;
using DentalClinic.Web.Auth;
using DentalClinic.Web.Models;

namespace DentalClinic.Web.Services;

public class DoctorService
{
    private readonly AuthService _auth;
    public DoctorService(AuthService auth) => _auth = auth;

    public async Task<List<DoctorDto>> GetAllAsync()
    {
        var http = _auth.CreateAuthorizedClient();
        return await http.GetFromJsonAsync<List<DoctorDto>>("api/doctors")
               ?? new List<DoctorDto>();
    }

    public async Task<bool> CreateAsync(CreateDoctorRequest req)
    {
        var http = _auth.CreateAuthorizedClient();
        var response = await http.PostAsJsonAsync("api/doctors", req);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var http = _auth.CreateAuthorizedClient();
        var response = await http.DeleteAsync($"api/doctors/{id}");
        return response.IsSuccessStatusCode;
    }
}