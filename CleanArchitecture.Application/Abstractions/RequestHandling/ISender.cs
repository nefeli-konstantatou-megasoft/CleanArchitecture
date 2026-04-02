namespace CleanArchitecture.Application.Abstractions.RequestHandling;

public interface ISender
{
    Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default);
}
