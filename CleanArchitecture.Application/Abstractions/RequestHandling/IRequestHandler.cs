namespace CleanArchitecture.Application.Abstractions.RequestHandling;

// The return type generic argument here *could* be covariant if C# had covariant task interface or allowed covariant classes
public interface IRequestHandler<in TRequest, /*out*/ TResponse>
    where TRequest : IRequest<TResponse>
{
    Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
}
