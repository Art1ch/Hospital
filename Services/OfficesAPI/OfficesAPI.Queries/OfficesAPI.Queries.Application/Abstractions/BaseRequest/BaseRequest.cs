using MediatR;

namespace OfficesAPI.Queries.Application.Abstractions.BaseRequest;

public abstract record BaseRequest<TRequest, TResponse>(TRequest Request) : IRequest<TResponse>;
