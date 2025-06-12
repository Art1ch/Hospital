using DoctorAPI.Application.Commands.Doctor.Create;
using DoctorAPI.Application.Commands.Doctor.Delete;
using DoctorAPI.Application.Queries.Doctor.GetAll;
using DoctorAPI.Application.Queries.Doctor.GetById;
using DoctorAPI.Application.Queries.Doctor.GetBySpecialization;
using DoctorAPI.Application.Queries.Doctor.GetByStatus;
using DoctorAPI.Application.Requests.Doctor;
using DoctorAPI.Application.Responses.Doctor;
using DoctorAPI.Core.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DoctorAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class DoctorController : ControllerBase
{
    private readonly ISender _sender;

    public DoctorController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<ActionResult<GetAllDoctorsResponse>> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var query = new GetAllDoctorsQuery(page, pageSize);
        var response = await _sender.Send(query);
        return Ok(response);
    }

    [HttpGet("id")]
    public async Task<ActionResult<GetByIdDoctorResponse>> GetById([FromQuery] Guid id)
    {
        var query = new GetByIdDoctorQuery(id);
        var response = await _sender.Send(query);
        return Ok(response);
    }

    [HttpGet("status")]
    public async Task<ActionResult<GetByStatusDoctorsResponse>> GetByStatus([FromQuery] DoctorStatus status)
    {
        var query = new GetByStatusDoctorsQuery(status);
        var response = await _sender.Send(query);
        return Ok(response);
    }

    [HttpGet("specialization")]
    public async Task<ActionResult<GetBySpecializationDoctorResponse>> GetBySpec([FromQuery] int specializationId)
    {
        var query = new GetBySpecializationDoctorQuery(specializationId);
        var response = await _sender.Send(query);
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<GetAllDoctorsResponse>> Create([FromBody] CreateDoctorRequest doctor)
    {
        var command = new CreateDoctorCommand(doctor);
        await _sender.Send(command);
        return Ok();
    }

    [HttpPatch]
    public async Task<ActionResult> Update([FromBody] UpdateDoctorRequest request)
    {
        var command = new UpdateDoctorCommand(request);
        await _sender.Send(command);
        return Ok();
    }

    [HttpDelete]
    public async Task<ActionResult> Delete([FromQuery] Guid id)
    {
        var command = new DeleteDoctorCommand(id);
        await _sender.Send(command);
        return Ok();
    }
}
