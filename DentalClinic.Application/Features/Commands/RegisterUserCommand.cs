using MediatR;

namespace DentalClinic.Application.Features.Commands;

public record RegisterUserCommand(
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    string Password
) : IRequest<int>;