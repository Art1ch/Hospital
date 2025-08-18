using MediatR;

namespace AppointmentAPI.Application.Abstractions;

public abstract record BaseRequest<TRequest, TResponse>(TRequest Request) : IRequest<TResponse>;
