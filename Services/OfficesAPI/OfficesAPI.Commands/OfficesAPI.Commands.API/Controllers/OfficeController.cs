using Microsoft.AspNetCore.Mvc;
using OfficesAPI.Commands.Application.Office.Create;
using OfficesAPI.Commands.Application.Office.Delete;
using OfficesAPI.Commands.Application.Office.Update;
using OfficesAPI.Commands.Application.Requests.Office;
using MediatR;

namespace OfficesAPI.Commands.API.Controllers;

[ApiController]
[Route("[controller]")]
public class OfficeController(
    ISender sender
) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreateOfficeRequest request)
    {
        var command = new CreateOfficeCommand(request);
        await sender.Send(command);
        return Created();
    }

    [HttpPatch]
    public async Task<ActionResult> Update([FromBody] UpdateOfficeRequest request)
    {
        var command = new UpdateOfficeCommand(request);
        await sender.Send(command);
        return NoContent();
    }

    [HttpDelete]
    public async Task<ActionResult> Delete([FromQuery] Guid id)
    {
        var command = new DeleteOfficeCommand(id);
        await sender.Send(command);
        return NoContent();
    }
}
