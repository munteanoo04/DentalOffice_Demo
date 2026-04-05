using DentalClinic.Application.Contracts.DTOs;
using DentalClinic.Application.Contracts.Interfaces;
using MediatR;


namespace DentalClinic.Application.Features.Queries;

public class GetAllPatientsQueryHandler : IRequestHandler<GetAllPatientsQuery, IEnumerable<PatientDto>>
{
    private readonly IPatientRepository _repo;

    public GetAllPatientsQueryHandler(IPatientRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<PatientDto>> Handle(GetAllPatientsQuery query, CancellationToken ct)
    {
        var patients = await _repo.GetAllAsync(ct);

        return patients.Select(p => new PatientDto(
            p.Id,
            p.FirstName,
            p.LastName,
            p.Email,
            p.PhoneNumber,
            p.Age,
            p.IsActive));
    }
}