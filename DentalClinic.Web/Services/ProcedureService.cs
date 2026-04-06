using System.Net.Http.Json;
using DentalClinic.Web.Auth;
using DentalClinic.Web.Models;

namespace DentalClinic.Web.Services;

public class ProcedureService
{
    private readonly AuthService _auth;
    public ProcedureService(AuthService auth) => _auth = auth;

    public async Task<List<ProcedureDto>> GetAllAsync()
    {
        var http = _auth.CreateAuthorizedClient();
        return await http.GetFromJsonAsync<List<ProcedureDto>>("api/procedures")
               ?? new List<ProcedureDto>();
    }
}