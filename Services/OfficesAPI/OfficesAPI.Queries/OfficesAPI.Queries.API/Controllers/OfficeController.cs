using MediatR;
using Microsoft.AspNetCore.Mvc;
using OfficesAPI.Queries.Application.Office.GetAll;
using OfficesAPI.Queries.Application.Queries.Office.GetInfo;
using OfficesAPI.Queries.Application.Requests.Office;
using OfficesAPI.Queries.Application.Responses.Office;

namespace OfficeAPI.Queries.API.Controllers;

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
}
