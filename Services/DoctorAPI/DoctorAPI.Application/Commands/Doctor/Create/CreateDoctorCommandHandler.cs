using AutoMapper;
using DoctorAPI.Application.Contracts;
using DoctorAPI.Application.WebRequests.Doctor.Create;
using DoctorAPI.Core.Entities;
using FluentValidation;
using MediatR;
using System.Text;

namespace DoctorAPI.Application.Commands.Doctor.Create;

public class CreateDoctorCommandHandler
    : IRequestHandler<CreateDoctorCommand,
        CreateDoctorResponseDto>
{
    private readonly IDoctorRepository _doctorRepository;
    private readonly IValidator<DoctorEntity> _validator;
    private readonly IMapper _mapper;

    public CreateDoctorCommandHandler(
        IDoctorRepository doctorRepository,
        IValidator<DoctorEntity> validator,
        IMapper mapper)
    {
        _doctorRepository = doctorRepository;
        _validator = validator;
        _mapper = mapper;
    }

    public async Task<CreateDoctorResponseDto> Handle(
        CreateDoctorCommand request, 
        CancellationToken ct)
    {
        var doctorEntity = _mapper.Map<DoctorEntity>(request.Dto);
        var validationResult = await _validator.ValidateAsync(doctorEntity, ct);
        if (!validationResult.IsValid)
        {
            var messages = new StringBuilder();
            foreach(var error in validationResult.Errors)
            {
                messages.AppendLine(error.ErrorMessage);
            }
            throw new Exception(messages.ToString());
        }
        var response = await _doctorRepository.CreateAsync(doctorEntity, ct);
        var result = _mapper.Map<CreateDoctorResponseDto>(response);
        return result;
    }
}
