using AuthAPI.Application.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace AuthAPI.Middlewares;

internal class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
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
                errorTitle = "Validation Error";
                detail = string.Join(";", validationException.Errors.Select(e => e.ErrorMessage));
                break;
            case AccountAlreadyExistsException accountAlreadyExistsException:
                status = StatusCodes.Status409Conflict;
                errorTitle = "Account already exists";
                detail = accountAlreadyExistsException.Message;
                break;
            case TokenIsExpiredException tokenIsExpiredException:
                status = StatusCodes.Status401Unauthorized;
                errorTitle = "Token is expired";
                detail = tokenIsExpiredException.Message;
                break;
            case WrongCredentialsGivenException wrongCredentialsGivenException:
                status = StatusCodes.Status401Unauthorized;
                errorTitle = "Wrong credentials given";
                detail = wrongCredentialsGivenException.Message;
                break;
            default:
                status = StatusCodes.Status500InternalServerError;
                errorTitle = "Server error";
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
