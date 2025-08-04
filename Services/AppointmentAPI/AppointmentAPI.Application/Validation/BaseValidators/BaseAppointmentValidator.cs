using AppointmentAPI.Application.Validation.Constants;
using AppointmentAPI.Core.Enums;
using FluentValidation;
using System.Linq.Expressions;

namespace AppointmentAPI.Application.Validation.BaseValidators;

public class BaseAppointmentValidator<T> : AbstractValidator<T>
{
    protected void ValidateAppointmentDate(Expression<Func<T, DateOnly>> expression)
    {
        RuleFor(expression)
            .NotEmpty()
            .WithMessage(ValidationConstants.OnFailedRequiredValidation);

        RuleFor(expression)
            .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today))
            .WithMessage(ValidationConstants.OnFailedDateValidation);
    }

    protected void ValidateAppointmentTime(Expression<Func<T, TimeOnly>> expression)
    {
        RuleFor(expression)
            .NotEmpty()
            .WithMessage(ValidationConstants.OnFailedRequiredValidation);

        RuleFor(expression)
            .InclusiveBetween(AppointmentConstants.StartAppointmentTime, AppointmentConstants.EndAppointmentTime)
            .WithMessage(ValidationConstants.OnFailedTimeValidation);
    }

    protected void ValidateAppointmentStatus(Expression<Func<T, AppointmentStatus>> expression)
    {
        RuleFor(expression)
            .NotEmpty()
            .WithMessage(ValidationConstants.OnFailedRequiredValidation);
    }
}
