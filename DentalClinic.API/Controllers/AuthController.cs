using DentalClinic.Application.Contracts.Interfaces;
using DentalClinic.Application.Features.Commands;
using DentalClinic.Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DentalClinic.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly JwtTokenService _jwt;
    private readonly IUserRepository _users;
    private readonly IMediator _mediator;

    public AuthController(
        JwtTokenService jwt,
        IUserRepository users,
        IMediator mediator)
    {
        _jwt = jwt;
        _users = users;
        _mediator = mediator;
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register(
        [FromBody] RegisterUserCommand cmd,
        CancellationToken ct)
    {
        var id = await _mediator.Send(cmd, ct);
        return Ok(new { id, message = "Registration successful." });
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(
        [FromBody] LoginRequest req,
        CancellationToken ct)
    {
        var user = await _users.GetByEmailAsync(req.Email.ToLower(), ct);

        if (user is null || !BCrypt.Net.BCrypt.Verify(req.Password, user.PasswordHash))
            return Unauthorized(new { message = "Invalid email or password." });

        var token = _jwt.GenerateToken(user);
        return Ok(new { token, role = user.Role });
    }
}

public record LoginRequest(string Email, string Password);