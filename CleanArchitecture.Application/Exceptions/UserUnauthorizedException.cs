namespace CleanArchitecture.Application.Exceptions;

public class UserUnauthorizedException : Exception
{
    public UserUnauthorizedException() : base() { }
    public UserUnauthorizedException(string message) : base(message) { }
    public UserUnauthorizedException(string message, Exception innerException) : base(message, innerException) { }
}
