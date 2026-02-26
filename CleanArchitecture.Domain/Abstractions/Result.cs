namespace CleanArchitecture.Domain.Abstractions;

public class Result
{
    public bool Success { get; }

    public bool Failure => !Success;

    public ErrorMessage? Message { get; }

    public static Result Ok() => new(true, null);

    public static Result Error(ErrorMessage errorMessage) => new(false, errorMessage);

    public static implicit operator Result(ErrorMessage errorMessage) => Error(errorMessage);

    protected Result(bool success, ErrorMessage? errorMessage)
    {
        Success = success;
        Message = errorMessage;
    }
}

public sealed class Result<T> : Result
{
    public T? Value { get; }

    public static Result<T> Ok(T value) => new(true, null, value);

    public static new Result<T> Error(ErrorMessage errorMessage) => new(false, errorMessage);

    public static Result<T> FromValue(T? value)
        => value is null ? Error(GeneralErrors.NullValueResult) : Ok(value);

    public static implicit operator Result<T>(ErrorMessage errorMessage) => Error(errorMessage);

    private Result(bool success, ErrorMessage? errorMessage, T? value = default)
        : base(success, errorMessage)
    {
        Value = value;
    }

    public static implicit operator Result<T>(T? value) => FromValue(value);
}
