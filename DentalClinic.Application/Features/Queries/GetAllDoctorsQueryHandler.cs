using DentalClinic.Application.Contracts.DTOs;
using DentalClinic.Application.Contracts.Interfaces;
using MediatR;
using System.Linq;

namespace DentalClinic.Application.Features.Queries;

public class GetAllDoctorsQueryHandler : IRequestHandler<GetAllDoctorsQuery, IEnumerable<DoctorDto>>
{
    private readonly IDoctorRepository _repo;

    public GetAllDoctorsQueryHandler(IDoctorRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<DoctorDto>> Handle(GetAllDoctorsQuery query, CancellationToken ct)
    {
        var doctors = await _repo.GetAllAsync(ct);

        return doctors.Select(d => new DoctorDto(
            d.Id,
            d.FirstName,
            d.LastName,
            d.Email,
            d.PhoneNumber,
            d.Specialization,
            d.IsActive));
    }
}