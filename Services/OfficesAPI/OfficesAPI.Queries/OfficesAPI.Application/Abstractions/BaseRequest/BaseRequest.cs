using MediatR;

namespace OfficesAPI.Application.Abstractions.BaseRequest;

public abstract record BaseRequest<TRequest, TResponse>(TRequest Request) : IRequest<TResponse>;
