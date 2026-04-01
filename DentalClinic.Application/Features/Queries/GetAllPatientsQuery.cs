// Features/Queries/GetAllPatientsQuery.cs
using DentalClinic.Application.Contracts.DTOs;
using MediatR;

namespace DentalClinic.Application.Features.Queries;

public record GetAllPatientsQuery : IRequest<IEnumerable<PatientDto>>;