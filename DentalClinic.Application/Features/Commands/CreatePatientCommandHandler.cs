using DentalClinic.Application.Contracts.Interfaces;
using DentalClinic.Domain.Entities;
using MediatR;

namespace DentalClinic.Application.Features.Commands;

public class CreatePatientCommandHandler : IRequestHandler<CreatePatientCommand, int>
{
    private readonly IPatientRepository _repo;

    public CreatePatientCommandHandler(IPatientRepository repo)
    {
        _repo = repo;
    }

    public async Task<int> Handle(CreatePatientCommand cmd, CancellationToken ct)
    {
        var existing = await _repo.GetByEmailAsync(cmd.Email, ct);

        if (existing is not null)
            throw new Exception($"Patient with email {cmd.Email} already exists.");

        var patient = new Patient
        {
            FirstName = cmd.FirstName,
            LastName = cmd.LastName,
            Email = cmd.Email,
            PhoneNumber = cmd.PhoneNumber,
            DateOfBirth = cmd.DateOfBirth,
            IsActive = true
        };

        await _repo.AddAsync(patient, ct);
        return patient.Id;
    }
}