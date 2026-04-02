using CleanArchitecture.Application.Abstractions.RequestHandling;
using CleanArchitecture.Domain.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infrastructure.RequestHandling;

internal class Sender(IServiceProvider provider) : ISender
{
    private readonly IServiceProvider _serviceProvider = provider;

    public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default) 
    {
        var handlerType = typeof(TypedRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));
        var handler = Activator.CreateInstance(handlerType);
        var typeErasedHandler = (TypeErasedRequestHandler<TResponse>)handler!;
        return typeErasedHandler.Handle(request, _serviceProvider, cancellationToken);
    }
}
