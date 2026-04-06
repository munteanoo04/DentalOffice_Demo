using DentalClinic.Application.Features.Commands;
using DentalClinic.Application.Features.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DentalClinic.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AppointmentsController : ControllerBase
{
    private readonly IMediator _mediator;
    public AppointmentsController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetAllAppointmentsQuery(), ct);
        return Ok(result);
    }

    [HttpGet("patient/{patientId}")]
    public async Task<IActionResult> GetByPatient(int patientId, CancellationToken ct)
    {
        var result = await _mediator.Send(
            new GetAppointmentsByPatientQuery(patientId), ct);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateAppointmentCommand cmd, CancellationToken ct)
    {
        var id = await _mediator.Send(cmd, ct);
        return Ok(id);
    }

    [HttpPut("{id}/cancel")]
    public async Task<IActionResult> Cancel(int id, CancellationToken ct)
    {
        await _mediator.Send(new CancelAppointmentCommand(id), ct);
        return NoContent();
    }
}