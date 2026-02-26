using CleanArchitecture.Domain.Abstractions;
using MediatR;

namespace CleanArchitecture.Application.Abstractions.RequestHandling
{
    public interface ICommandHandler<in TRequest, TResponse> : IRequestHandler<TRequest, Result<TResponse>>
        where TRequest : ICommand<TResponse>
    {}

    public interface ICommandHandler<in TRequest> : IRequestHandler<TRequest, Result>
        where TRequest : ICommand
    {}
}
