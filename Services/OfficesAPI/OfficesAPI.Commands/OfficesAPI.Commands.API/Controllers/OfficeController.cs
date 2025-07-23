using MediatR;
using Microsoft.AspNetCore.Mvc;
using OfficesAPI.Commands.Application.Office.Create;
using OfficesAPI.Commands.Application.Office.Delete;
using OfficesAPI.Commands.Application.Office.Update;
using OfficesAPI.Commands.Application.Requests.Office;

namespace OfficesAPI.Commands.API.Controllers;

[ApiController]
[Route("[controller]")]
public class OfficeController : ControllerBase
{
    private readonly ISender _sender;
    public OfficeController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreateOfficeRequest request)
    {
        var command = new CreateOfficeCommand(request);
        await _sender.Send(command);
        return Ok();
    }

    [HttpPatch]
    public async Task<ActionResult> Update([FromBody] UpdateOfficeRequest request)
    {
        var command = new UpdateOfficeCommand(request);
        await _sender.Send(command);
        return Ok();
    }

    [HttpDelete]
    public async Task<ActionResult> Delete([FromQuery] Guid id)
    {
        var command = new DeleteOfficeCommand(id);
        await _sender.Send(command);
        return Ok();
    }
}
