// Features/Commands/CreateDoctorCommandHandler.cs
using DentalClinic.Application.Contracts.Interfaces;
using DentalClinic.Domain.Entities;
using MediatR;
using System.Numerics;

namespace DentalClinic.Application.Features.Commands;

public class CreateDoctorCommandHandler : IRequestHandler<CreateDoctorCommand, Guid>
{
    private readonly IDoctorRepository _repo;
    public CreateDoctorCommandHandler(IDoctorRepository repo) => _repo = repo;

    public async Task<Guid> Handle(CreateDoctorCommand cmd, CancellationToken ct)
    {
        var doctor = Doctor.Create(cmd.Name, cmd.FirstName, cmd.Email, cmd.PhoneNumber, cmd.Specialization);
        await _repo.AddAsync(doctor, ct);
        return doctor.Id;
    }
}