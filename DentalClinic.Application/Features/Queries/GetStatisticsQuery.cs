using DentalClinic.Application.Contracts.DTOs;
using MediatR;

namespace DentalClinic.Application.Features.Queries;

public record GetStatisticsQuery(
    DateTime? From,
    DateTime? To,
    int? DoctorId,
    int? ProcedureId
) : IRequest<StatisticsDto>;