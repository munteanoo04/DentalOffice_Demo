using DentalClinic.Application.Contracts.Interfaces;
using DentalClinic.Domain.Enums;
using MediatR;

namespace DentalClinic.Application.Features.Commands;

public class CancelAppointmentCommandHandler : IRequestHandler<CancelAppointmentCommand>
{
    private readonly IAppointmentRepository _repo;

    public CancelAppointmentCommandHandler(IAppointmentRepository repo)
    {
        _repo = repo;
    }

    public async Task Handle(CancelAppointmentCommand cmd, CancellationToken ct)
    {
        var appointment = await _repo.GetByIdAsync(cmd.AppointmentId, ct)
            ?? throw new Exception($"Appointment {cmd.AppointmentId} not found.");

        appointment.Status = AppointmentStatus.Cancelled;

        await _repo.UpdateAsync(appointment, ct);
    }
}