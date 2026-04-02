using CleanArchitecture.Application.Abstractions.RequestHandling;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CleanArchitecture.Infrastructure.RequestHandling;

public static class Mediator
{
    private record RequestHandlerPair(Type InterfaceType, Type ImplementationType);

    public static IServiceCollection AddMediator(this IServiceCollection services, Assembly assembly)
    {
        services.AddScoped<ISender, Sender>();

        var requestHandlerType = typeof(IRequestHandler<,>);

        var handlerTypes = assembly.GetTypes()
            .Where(type => !type.IsAbstract && !type.IsInterface)
            .SelectMany(type => type.GetInterfaces()
                .Where(interfaceType => interfaceType.IsGenericType &&
                    requestHandlerType == interfaceType.GetGenericTypeDefinition())
                .Select(interfaceType => new RequestHandlerPair(interfaceType, type)));

        foreach (var handlerType in handlerTypes)
        {
            services.AddTransient(handlerType.InterfaceType, handlerType.ImplementationType);
        }

        return services;
    }
}
