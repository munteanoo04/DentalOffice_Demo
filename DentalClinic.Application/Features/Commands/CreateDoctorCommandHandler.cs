using DentalClinic.Application.Contracts.Interfaces;
using DentalClinic.Domain.Entities;
using MediatR;

namespace DentalClinic.Application.Features.Commands;

public class CreateDoctorCommandHandler : IRequestHandler<CreateDoctorCommand, int>
{
    private readonly IDoctorRepository _repo;

    public CreateDoctorCommandHandler(IDoctorRepository repo)
    {
        _repo = repo;
    }

    public async Task<int> Handle(CreateDoctorCommand cmd, CancellationToken ct)
    {
        var doctor = new Doctor
        {
            FirstName = cmd.FirstName,
            LastName = cmd.LastName,
            Email = cmd.Email,
            PhoneNumber = cmd.PhoneNumber,
            Specialization = cmd.Specialization,
            IsActive = true
        };

        await _repo.AddAsync(doctor, ct);
        return doctor.Id;
    }
}