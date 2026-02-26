namespace CleanArchitecture.Application.Articles;

public static class ArticleErrors
{
    public static readonly ErrorMessage ArticleNotFound = new(2000, "The specified article was not found");
}
