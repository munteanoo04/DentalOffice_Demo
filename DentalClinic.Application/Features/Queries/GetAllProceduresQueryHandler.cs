using DentalClinic.Application.Contracts.DTOs;
using DentalClinic.Application.Contracts.Interfaces;
using MediatR;
namespace DentalClinic.Application.Features.Queries;

public record GetAllProceduresQuery : IRequest<IEnumerable<ProcedureDto>>;


public class GetAllProceduresQueryHandler
    : IRequestHandler<GetAllProceduresQuery, IEnumerable<ProcedureDto>>
{
    private readonly IProcedureRepository _repo;
    public GetAllProceduresQueryHandler(IProcedureRepository repo) => _repo = repo;

    public async Task<IEnumerable<ProcedureDto>> Handle(
        GetAllProceduresQuery query, CancellationToken ct)
    {
        var procedures = await _repo.GetAllAsync(ct);
        return procedures.Select(p => new ProcedureDto(
            p.Id, p.Name, p.Description,
            p.Price, p.DurationMinutes,
            p.Category.ToString(),
            p.RequiresAnesthesia,
            p.IsActive));
    }
}