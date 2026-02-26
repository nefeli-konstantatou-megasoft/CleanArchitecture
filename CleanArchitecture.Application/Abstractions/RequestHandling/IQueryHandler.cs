namespace CleanArchitecture.Application.Abstractions.RequestHandling;

public interface IQueryHandler<TRequest, TResponse> : IRequestHandler<TRequest, Result<TResponse>>
    where TRequest : IQuery<TResponse>
{ }
