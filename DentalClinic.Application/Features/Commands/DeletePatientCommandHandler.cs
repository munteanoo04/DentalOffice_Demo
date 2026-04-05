using DentalClinic.Application.Contracts.Interfaces;
using MediatR;
namespace DentalClinic.Application.Features.Commands;

public class DeletePatientCommandHandler : IRequestHandler<DeletePatientCommand>
{
    private readonly IPatientRepository _repo;
    public DeletePatientCommandHandler(IPatientRepository repo) => _repo = repo;
    public async Task Handle(DeletePatientCommand cmd, CancellationToken ct)
        => await _repo.DeleteAsync(cmd.Id, ct);
}