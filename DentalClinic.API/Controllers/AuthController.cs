using DentalClinic.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DentalClinic.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly JwtTokenService _jwt;
    public AuthController(JwtTokenService jwt) => _jwt = jwt;

    [HttpPost("login")]
    [AllowAnonymous]
    public IActionResult Login([FromBody] LoginRequest req)
    {
        // TODO: replace with real user validation from DB
        if (req.Email == "admin@clinic.com" && req.Password == "secret")
        {
            var token = _jwt.GenerateToken("1", req.Email, "Admin");
            return Ok(new { token });
        }

        return Unauthorized();
    }
}

public record LoginRequest(string Email, string Password);