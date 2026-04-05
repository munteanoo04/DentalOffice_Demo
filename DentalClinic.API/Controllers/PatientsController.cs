using DentalClinic.Application.Features.Commands;
using DentalClinic.Application.Features.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DentalClinic.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PatientsController : ControllerBase
{
    private readonly IMediator _mediator;
    public PatientsController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetAllPatientsQuery(), ct);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreatePatientCommand cmd, CancellationToken ct)
    {
        var id = await _mediator.Send(cmd, ct);
        return Ok(id);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        await _mediator.Send(new DeletePatientCommand(id), ct);
        return NoContent();
    }
}