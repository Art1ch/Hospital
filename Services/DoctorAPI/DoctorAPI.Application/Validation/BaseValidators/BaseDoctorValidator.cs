using DoctorAPI.Application.Constants.Doctor;
using DoctorAPI.Application.Validation.Constants;
using FluentValidation;
using System.Linq.Expressions;
using DoctorAPI.Application.Enums;
using DoctorAPI.Application.Constants.Specialization;
using DoctorAPI.Application.Validation.BaseValidators;

namespace DoctorAPI.Application.Validation.BaseDoctorValidator;

public class BaseDoctorValidator<T> : BasePaginationValidator<T>
{
    protected void ValidateFirstName(Expression<Func<T, string>> expression)
    {
        RuleFor(expression)
            .NotEmpty().WithMessage(ValidationConstants.OnFailedNullValidation)
            .Length(DoctorConstants.MinFirstNameLength, DoctorConstants.MaxFirstNameLength)
            .WithMessage(ValidationConstants.OnFailedFirstNameValidation)
            .Matches("^[a-zA-Zа-яА-Я- ]+$").WithMessage(ValidationConstants.OnFailedRegexValidation);
    }

    protected void ValidateLastName(Expression<Func<T, string>> expression)
    {
        RuleFor(expression)
            .NotEmpty().WithMessage(ValidationConstants.OnFailedNullValidation)
            .Length(DoctorConstants.MinLastNameLength, DoctorConstants.MaxLastNameLength)
            .WithMessage(ValidationConstants.OnFailedLastNameValidation)
            .Matches("^[a-zA-Zа-яА-Я- ]+$").WithMessage(ValidationConstants.OnFailedRegexValidation);
    }

    protected void ValidateMiddleName(Expression<Func<T, string>> expression)
    {
        RuleFor(expression)
            .NotEmpty().WithMessage(ValidationConstants.OnFailedNullValidation)
            .Length(DoctorConstants.MinMiddleNameLength, DoctorConstants.MaxMiddleNameLength)
            .WithMessage(ValidationConstants.OnFailedMiddleNameValidation)
            .Matches("^[a-zA-Zа-яА-Я- ]+$").WithMessage(ValidationConstants.OnFailedRegexValidation);
    }

    protected void ValidateStatus(Expression<Func<T, DoctorStatus>> expression)
    {
        RuleFor(expression)
            .NotEmpty().WithMessage(ValidationConstants.OnFailedNullValidation);
    }

    protected void ValidateBirthDate(Expression<Func<T, DateOnly>> expression)
    {
        RuleFor(expression)
            .NotEmpty().WithMessage(ValidationConstants.OnFailedNullValidation)
            .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today));
    }

    protected void ValidateCareerStartDate(Expression<Func<T, DateOnly>> expression)
    {
        RuleFor(expression)
            .NotEmpty().WithMessage(ValidationConstants.OnFailedNullValidation)
            .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today));
    }

    protected void ValidateSpecializationName(Expression<Func<T, string>> expression)
    {
        RuleFor(expression)
            .NotEmpty().WithMessage(ValidationConstants.OnFailedNullValidation)
            .Length(SpecializationConstants.MinSpecializationNameLength, SpecializationConstants.MaxSpecializationNameLength)
            .WithMessage(ValidationConstants.OnFailedSpecializationNameValidation)
            .Matches("^[a-zA-Zа-яА-Я- ]+$").WithMessage(ValidationConstants.OnFailedRegexValidation);
    }
}
