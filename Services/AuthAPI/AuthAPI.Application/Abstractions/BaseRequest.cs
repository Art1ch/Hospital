using MediatR;

namespace AuthAPI.Application.Abstractions;

public abstract record BaseRequest<TRequest, TReponse>(TRequest Request) : IRequest<TReponse>;
