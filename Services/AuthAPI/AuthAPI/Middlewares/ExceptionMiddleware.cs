using AuthAPI.Application.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace AuthAPI.Middlewares;

internal class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IStringLocalizer _localizer;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(
        RequestDelegate next,
        IStringLocalizer<ExceptionMiddleware> localizer,
        ILogger<ExceptionMiddleware> logger
    )
    {
        _next = next;
        _localizer = localizer;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }

    }

    public async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        int status;
        string errorTitle;
        string detail;

        switch (exception)
        {
            case ValidationException validationException:
                status = StatusCodes.Status400BadRequest;
                errorTitle = _localizer["ValidationError"];
                detail = string.Join(";", validationException.Errors.Select(e =>
                {
                    return _localizer[e.ErrorCode];
                }));
                break;
            case AccountAlreadyExistsException accountAlreadyExistsException:
                status = StatusCodes.Status409Conflict;
                errorTitle = _localizer["AccountAlreadyExists"];
                detail = accountAlreadyExistsException.Message;
                break;
            case TokenIsExpiredException tokenIsExpiredException:
                status = StatusCodes.Status401Unauthorized;
                errorTitle = _localizer["TokenIsExpired"];
                detail = tokenIsExpiredException.Message;
                break;
            case WrongCredentialsGivenException wrongCredentialsGivenException:
                status = StatusCodes.Status401Unauthorized;
                errorTitle = _localizer["WrongCredentials"];
                detail = wrongCredentialsGivenException.Message;
                break;
            default:
                status = StatusCodes.Status500InternalServerError;
                errorTitle = _localizer["ServerError"];
                detail = exception.Message;
                break;
        }

        var problemDetails = new ProblemDetails()
        {
            Status = status,
            Title = errorTitle,
            Detail = detail
        };

        _logger.LogInformation(exception.Message, detail);

        await context.Response.WriteAsJsonAsync(problemDetails);
    }
}
