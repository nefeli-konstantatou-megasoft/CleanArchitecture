using CleanArchitecture.Application.Abstractions.RequestHandling;
using CleanArchitecture.Domain.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infrastructure.RequestHandling;

public abstract class TypeErasedRequestHandler<TResponse>
{
    public abstract Task<TResponse> Handle(IRequest<TResponse> request, IServiceProvider serviceProvider, CancellationToken cancellationToken);
}

public class TypedRequestHandler<TRequest, TResponse> : TypeErasedRequestHandler<TResponse>
    where TRequest : IRequest<TResponse>
{
    public override Task<TResponse> Handle(IRequest<TResponse> request, IServiceProvider serviceProvider, CancellationToken cancellationToken)
        => serviceProvider.GetRequiredService<IRequestHandler<TRequest, TResponse>>()
            .Handle((TRequest)request, cancellationToken);
}
