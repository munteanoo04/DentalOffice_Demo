using DentalClinic.Application.Contracts.Interfaces;
using DentalClinic.Domain.Entities;
using MediatR;

namespace DentalClinic.Application.Features.Commands;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, int>
{
    private readonly IUserRepository _repo;
    private readonly IEmailService _email;

    public RegisterUserCommandHandler(
        IUserRepository repo,
        IEmailService email)
    {
        _repo = repo;
        _email = email;
    }

    public async Task<int> Handle(RegisterUserCommand cmd, CancellationToken ct)
    {
        var existing = await _repo.GetByEmailAsync(cmd.Email.ToLower(), ct);
        if (existing is not null)
            throw new Exception($"Email {cmd.Email} is already registered.");

        var hash = BCrypt.Net.BCrypt.HashPassword(cmd.Password);

        var user = User.Create(
            cmd.FirstName,
            cmd.LastName,
            cmd.Email,
            cmd.PhoneNumber,
            hash,
            "Client");

        await _repo.AddAsync(user, ct);

        // Send confirmation email (don't fail if email fails)
        try
        {
            await _email.SendConfirmationEmailAsync(
                user.Email, user.FirstName, ct);
        }
        catch { /* log in production */ }

        return user.Id;
    }
}