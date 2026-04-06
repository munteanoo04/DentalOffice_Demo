using DentalClinic.Application.Contracts.DTOs;
using DentalClinic.Application.Contracts.Interfaces;
using DentalClinic.Domain.Enums;
using MediatR;

namespace DentalClinic.Application.Features.Queries;

public class GetStatisticsQueryHandler
    : IRequestHandler<GetStatisticsQuery, StatisticsDto>
{
    private readonly IAppointmentRepository _repo;
    private readonly IProcedureRepository _procRepo;

    public GetStatisticsQueryHandler(
        IAppointmentRepository repo,
        IProcedureRepository procRepo)
    {
        _repo = repo;
        _procRepo = procRepo;
    }

    public async Task<StatisticsDto> Handle(
        GetStatisticsQuery query, CancellationToken ct)
    {
        var all = await _repo.GetAllAsync(ct);
        var q = all.AsEnumerable();

        if (query.From.HasValue)
            q = q.Where(a => a.ScheduledAt >= query.From.Value);
        if (query.To.HasValue)
            q = q.Where(a => a.ScheduledAt <= query.To.Value.AddDays(1));
        if (query.DoctorId.HasValue)
            q = q.Where(a => a.DoctorId == query.DoctorId.Value);
        if (query.ProcedureId.HasValue)
            q = q.Where(a => a.ProcedureId == query.ProcedureId.Value);

        var list = q.ToList();

        var byDoctor = list
            .GroupBy(a => new
            {
                a.DoctorId,
                Name = $"{a.Doctor.FirstName} {a.Doctor.LastName}"
            })
            .Select(g => new DoctorStatDto(
                g.Key.Name,
                g.Count(),
                g.Count(a => a.Status == AppointmentStatus.Completed),
                g.Count(a => a.Status == AppointmentStatus.Cancelled)))
            .OrderByDescending(d => d.Total)
            .ToList();

        var byProcedure = list
            .GroupBy(a => new
            {
                a.ProcedureId,
                Name = a.Procedure.Name,
                Price = a.Procedure.Price
            })
            .Select(g => new ProcedureStatDto(
                g.Key.Name,
                g.Count(),
                g.Count(a => a.Status == AppointmentStatus.Completed)
                    * g.Key.Price))
            .OrderByDescending(p => p.Total)
            .ToList();

        return new StatisticsDto(
            list.Count,
            list.Count(a => a.Status == AppointmentStatus.Scheduled),
            list.Count(a => a.Status == AppointmentStatus.Completed),
            list.Count(a => a.Status == AppointmentStatus.Cancelled),
            byDoctor,
            byProcedure);
    }
}