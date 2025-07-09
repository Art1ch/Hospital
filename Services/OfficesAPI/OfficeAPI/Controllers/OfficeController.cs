using MediatR;
using Microsoft.AspNetCore.Mvc;
using OfficesAPI.Application.Commands.Office.Create;
using OfficesAPI.Application.Commands.Office.Delete;
using OfficesAPI.Application.Commands.Office.Update;
using OfficesAPI.Application.Queries.Office.GetAll;
using OfficesAPI.Application.Queries.Office.GetInfo;
using OfficesAPI.Application.Requests.Office;
using OfficesAPI.Application.Responses.Office;

namespace OfficeAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class OfficeController : ControllerBase
{
    private readonly ISender _sender;
    private const int CacheDurationSeconds = 300;
    public OfficeController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<ActionResult<GetAllOfficesResponse>> GetAll([FromQuery] GetAllOfficesRequest request)
    {
        var query = new GetAllOfficesQuery(request);
        var response = await _sender.Send(query);
        return Ok(response);
    }

    [ResponseCache(Duration = CacheDurationSeconds, Location = ResponseCacheLocation.Any)]
    [HttpGet("info")]
    public async Task<ActionResult<GetOfficeInfoResponse>> GetInfo([FromQuery] Guid id)
    {
        var query = new GetOfficeInfoQuery(id);
        var response = await _sender.Send(query);
        return Ok(response);
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
