using DoctorAPI.Application.Commands.Doctor.Create;
using DoctorAPI.Application.Queries.Doctor.GetAll;
using DoctorAPI.Application.Queries.Doctor.GetById;
using DoctorAPI.Application.Queries.Doctor.GetBySpecialization;
using DoctorAPI.Application.Queries.Doctor.GetByStatus;
using DoctorAPI.Application.WebDto_s.Doctor.Delete;
using DoctorAPI.Application.WebDto_s.Doctor.GetAll;
using DoctorAPI.Application.WebDto_s.Doctor.GetById;
using DoctorAPI.Application.WebDto_s.Doctor.GetBySpecialization;
using DoctorAPI.Application.WebDto_s.Doctor.GetByStatus;
using DoctorAPI.Application.WebDto_s.Doctor.Update;
using DoctorAPI.Application.WebRequests.Doctor.Create;
using DoctorAPI.Application.WebRequests.Doctor.Update;
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
    public async Task<ActionResult<GetAllDoctorsResponseDto>> GetAll(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var request = new GetAllDoctorsRequestDto(page, pageSize);
        var query = new GetAllDoctorsQuery(request);
        var result = await _sender.Send(query);
        return Ok(result);
    }

    [HttpGet("id")]
    public async Task<ActionResult<GetByIdDoctorResponseDto>> GetById(
        [FromQuery] Guid id)
    {
        var request = new GetByIdDoctorRequestDto(id);
        var query = new GetByIdDoctorQuery(request);
        var result = await _sender.Send(query);
        return Ok(result);
    }

    [HttpGet("status")]
    public async Task<ActionResult<GetByStatusDoctorsResponseDto>> GetByStatus(
        [FromQuery] StatusEnum status)
    {
        var request = new GetByStatusDoctorsRequestDto(status);
        var query = new GetByStatusDoctorsQuery(request);
        var result = await _sender.Send(query);
        return Ok(result);
    }

    [HttpGet("specialization")]
    public async Task<ActionResult<GetBySpecializationDoctorResponseDto>> GetBySpec(
        [FromQuery] int specializationId)
    {
        var request = new GetBySpecializationDoctorRequestDto(specializationId);
        var query = new GetBySpecializationDoctorQuery(request);
        var result = await _sender.Send(query);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<GetAllDoctorsResponseDto>> Create(
        [FromBody] CreateDoctorRequestDto doctor)
    {
        var command = new CreateDoctorCommand(doctor);
        var result = await _sender.Send(command);
        return Ok(result);
    }

    [HttpPatch]
    public async Task<ActionResult<UpdateDoctorResponseDto>> Update(
        [FromBody] UpdateDoctorRequestDto request)
    {
        var command = new UpdateDoctorCommand(request);
        var result = await _sender.Send(command);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<ActionResult<DeleteDoctorResponseDto>> Delete(
        [FromBody] UpdateDoctorRequestDto request)
    {
        var command = new UpdateDoctorCommand(request);
        var result = await _sender.Send(command);
        return Ok(result);
    }
}
