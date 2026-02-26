namespace CleanArchitecture.Application.Abstractions.RequestHandling;

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{ }

public interface ICommand : IRequest<Result>
{ }
