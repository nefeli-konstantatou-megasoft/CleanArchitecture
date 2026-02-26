namespace CleanArchitecture.Domain.Abstractions
{
    public static class GeneralErrors
    {
        public static readonly ErrorMessage NullValueResult = new(1000, "Tried to construct a response object from a null value.");
    }
}
