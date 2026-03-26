namespace CleanArchitecture.Domain.Abstractions;

public static class GeneralErrors
{
    public static readonly ErrorMessage NullValueResult = new(1000, "Tried to construct a response object from a null value.");
    public static readonly ErrorMessage UnexpectedFailure = new(1001, "Something unexpected happened. Please contract an administrator.");
}
