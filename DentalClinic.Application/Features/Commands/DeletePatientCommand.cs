using MediatR;
namespace DentalClinic.Application.Features.Commands;

public record DeletePatientCommand(int Id) : IRequest;