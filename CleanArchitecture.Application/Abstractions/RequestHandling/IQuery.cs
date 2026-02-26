using CleanArchitecture.Domain.Abstractions;
using MediatR;

namespace CleanArchitecture.Application.Abstractions.RequestHandling
{
    public interface IQuery<TResponse> : IRequest<Result<TResponse>>
    {}
}
