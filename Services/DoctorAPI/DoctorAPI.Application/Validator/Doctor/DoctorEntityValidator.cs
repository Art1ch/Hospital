using DoctorAPI.Core.Entities;
using FluentValidation;
using DoctorAPI.Core.Constants.Common;
using DoctorAPI.Core.Constants.Doctor;

namespace DoctorAPI.Application.Validator.Doctor;

public class DoctorEntityValidator<TId1, TId2> :
    AbstractValidator<DoctorEntity<TId1, TId2>>
{
    public DoctorEntityValidator()
    {
        RuleFor(d => d.FirstName)
            .NotEmpty().WithMessage(CommonConstants.OnFailedNullValidation)
            .Length(DoctorConstants.MinFirstNameLength, DoctorConstants.MaxFirstNameLength)
            .WithMessage(DoctorConstants.OnFailedFirstNameValidation)
            .Matches("^[a-zA-Zа-яА-Я- ]+$").WithMessage(CommonConstants.OnFailedRegexValidation);

        RuleFor(d => d.LastName)
            .NotEmpty().WithMessage(CommonConstants.OnFailedNullValidation)
            .Length(DoctorConstants.MinLastNameLength, DoctorConstants.MaxLastNameLength)
            .WithMessage(DoctorConstants.OnFailedLastNameValidation)
            .Matches("^[a-zA-Zа-яА-Я- ]+$").WithMessage(CommonConstants.OnFailedRegexValidation);

        RuleFor(d => d.MiddleName)
            .NotEmpty().WithMessage(CommonConstants.OnFailedNullValidation)
            .Length(DoctorConstants.MinMiddleNameLength, DoctorConstants.MaxMiddleNameLength)
            .WithMessage(DoctorConstants.OnFailedMiddleNameValidation)
            .Matches("^[a-zA-Zа-яА-Я- ]+$").WithMessage(CommonConstants.OnFailedRegexValidation);

        RuleFor(d => d.Status)
            .NotEmpty().WithMessage(CommonConstants.OnFailedNullValidation);

        RuleFor(d => d.BirthDate)
            .NotEmpty().WithMessage(CommonConstants.OnFailedNullValidation)
            .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today));

        RuleFor(d => d.CareerStartDay)
            .NotEmpty().WithMessage(CommonConstants.OnFailedNullValidation)
            .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today));
    }
}
