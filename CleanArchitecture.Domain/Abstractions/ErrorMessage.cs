namespace CleanArchitecture.Domain.Abstractions
{
    public readonly record struct ErrorMessage(int Code, string Body);
}
