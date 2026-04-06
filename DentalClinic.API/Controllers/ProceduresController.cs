using DentalClinic.Application.Features.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DentalClinic.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProceduresController : ControllerBase
{
    private readonly IMediator _mediator;
    public ProceduresController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetAllProceduresQuery(), ct);
        return Ok(result);
    }
}