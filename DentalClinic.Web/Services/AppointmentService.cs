using System.Net.Http.Json;
using DentalClinic.Web.Auth;
using DentalClinic.Web.Models;

namespace DentalClinic.Web.Services;

public class AppointmentService
{
    private readonly AuthService _auth;

    public AppointmentService(AuthService auth)
    {
        _auth = auth;
    }

    public async Task<List<AppointmentDto>> GetByPatientAsync(int patientId)
    {
        var http = _auth.CreateAuthorizedClient();
        var result = await http.GetFromJsonAsync<List<AppointmentDto>>(
            $"api/appointments/patient/{patientId}");

        return result ?? new List<AppointmentDto>();
    }

    public async Task<List<AppointmentDto>> GetByDoctorAsync(int doctorId)
    {
        var http = _auth.CreateAuthorizedClient();
        var result = await http.GetFromJsonAsync<List<AppointmentDto>>(
            $"api/appointments/doctor/{doctorId}");

        return result ?? new List<AppointmentDto>();
    }

    public async Task<bool> CreateAsync(CreateAppointmentRequest request)
    {
        var http = _auth.CreateAuthorizedClient();
        var response = await http.PostAsJsonAsync("api/appointments", request);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> CancelAsync(int id)
    {
        var http = _auth.CreateAuthorizedClient();
        var response = await http.DeleteAsync($"api/appointments/{id}");
        return response.IsSuccessStatusCode;
    }
}