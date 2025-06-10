using AutoMapper;
using DoctorAPI.Application.Commands.Doctor.Create;
using DoctorAPI.Application.Contracts;
using DoctorAPI.Application.WebDto_s.Doctor.Update;
using DoctorAPI.Core.Entities;
using FluentValidation;
using MediatR;
using System.Text;

namespace DoctorAPI.Application.Commands.Doctor.Update;

public class UpdateDoctorCommandHandler
    : IRequestHandler<UpdateDoctorCommand, UpdateDoctorResponseDto>
{
    private readonly IDoctorRepository _doctorRepository;
    private readonly IValidator<DoctorEntity> _validator;
    private readonly IMapper _mapper;
    public UpdateDoctorCommandHandler
        (IDoctorRepository doctorRepository,
        IValidator<DoctorEntity> validator,
        IMapper mapper)
    {
        _doctorRepository = doctorRepository;
        _validator = validator;
        _mapper = mapper;
    }
    public async Task<UpdateDoctorResponseDto> Handle(
        UpdateDoctorCommand request,
        CancellationToken ct)
    {
        var doctorEntity = _mapper.Map<DoctorEntity>(request.Dto);
        var validationResult = await _validator.ValidateAsync(doctorEntity);
        if (!validationResult.IsValid)
        {
            var messages = new StringBuilder();
            foreach(var error in validationResult.Errors)
            {
                messages.AppendLine(error.ErrorMessage);
            }
            throw new Exception(messages.ToString());
        }
        var response = await _doctorRepository.UpdateAsync(doctorEntity, ct);
        var result = _mapper.Map<UpdateDoctorResponseDto>(response);
        return result;
    }
}
