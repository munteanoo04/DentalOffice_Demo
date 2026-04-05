using DentalClinic.Application.Contracts.Interfaces;
using MediatR;
namespace DentalClinic.Application.Features.Commands;

public class DeleteDoctorCommandHandler : IRequestHandler<DeleteDoctorCommand>
{
    private readonly IDoctorRepository _repo;
    public DeleteDoctorCommandHandler(IDoctorRepository repo) => _repo = repo;
    public async Task Handle(DeleteDoctorCommand cmd, CancellationToken ct)
        => await _repo.DeleteAsync(cmd.Id, ct);
}