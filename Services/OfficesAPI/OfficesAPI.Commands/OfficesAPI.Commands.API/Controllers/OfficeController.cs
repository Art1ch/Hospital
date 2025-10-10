using Microsoft.AspNetCore.Mvc;
using OfficesAPI.Commands.Application.Office.Create;
using OfficesAPI.Commands.Application.Office.Delete;
using OfficesAPI.Commands.Application.Office.Update;
using OfficesAPI.Commands.Application.Requests.Office;
using MediatR;
using OfficesAPI.Commands.Application.Requests;
using OfficesAPI.Shared.Responses;
using OfficesAPI.Shared.Requests;
using OfficesAPI.Commands.Application.Queries.Office.GetAll;
using OfficesAPI.Commands.Application.Queries.Office.GetInfo;

namespace OfficesAPI.Commands.API.Controllers;

[ApiController]
[Route("[controller]")]
public class OfficeController(
    ISender sender
) : ControllerBase
{

    [HttpGet]
    public async Task<ActionResult<GetAllOfficesResponse>> GetAll([FromQuery] GetAllOfficesRequest request)
    {
        Console.WriteLine("WRITE-SIDE WORKED!");
        var query = new GetAllOfficesQuery(request);
        var response = await sender.Send(query);
        return Ok(response);
    }

    [HttpGet("info")]
    public async Task<ActionResult<GetOfficeInfoResponse>> GetInfo([FromQuery] Guid id)
    {
        var query = new GetOfficeInfoQuery(id);
        var response = await sender.Send(query);
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromForm] CreateOfficeRequest request)
    {
        var command = new CreateOfficeCommand(request);
        await sender.Send(command);
        return Created();
    }

    [HttpPatch]
    public async Task<ActionResult> Update([FromForm] UpdateOfficeRequest request)
    {
        var command = new UpdateOfficeCommand(request);
        await sender.Send(command);
        return NoContent();
    }

    [HttpDelete]
    public async Task<ActionResult> Delete([FromBody] DeleteOfficeRequest request)
    {
        var command = new DeleteOfficeCommand(request);
        await sender.Send(command);
        return NoContent();
    }
}
