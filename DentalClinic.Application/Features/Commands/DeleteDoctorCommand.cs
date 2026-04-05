using MediatR;
namespace DentalClinic.Application.Features.Commands;

public record DeleteDoctorCommand(int Id) : IRequest;