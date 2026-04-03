using System.Net.Http.Headers;
using System.Net.Http.Json;
using DentalClinic.Web.Auth;
using DentalClinic.Web.Models;

namespace DentalClinic.Web.Services;

public class AppointmentService
{
    private readonly HttpClient _http;
    private readonly AuthService _auth;

    public AppointmentService(HttpClient http, AuthService auth)
    {
        _http = http;
        _auth = auth;
    }

    private void AttachToken() =>
        _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _auth.Token);

    public async Task<List<AppointmentDto>> GetByPatientAsync(int patientId)
    {
        AttachToken();
        return await _http.GetFromJsonAsync<List<AppointmentDto>>(
                   $"api/appointments/patient/{patientId}")
               ?? new List<AppointmentDto>();
    }

    public async Task<bool> CreateAsync(CreateAppointmentRequest req)
    {
        AttachToken();
        var response = await _http.PostAsJsonAsync("api/appointments", req);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> CancelAsync(int id)
    {
        AttachToken();
        var response = await _http.PutAsync(
            $"api/appointments/{id}/cancel", null);
        return response.IsSuccessStatusCode;
    }
}

public class CreateAppointmentRequest
{
    public int PatientId { get; set; }
    public int DoctorId { get; set; }
    public int ProcedureId { get; set; }
    public DateTime ScheduledAt { get; set; }
    public string TreatmentType { get; set; } = string.Empty;
    public string? Notes { get; set; }
}