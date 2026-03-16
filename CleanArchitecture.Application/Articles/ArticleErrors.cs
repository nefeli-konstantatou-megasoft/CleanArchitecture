namespace CleanArchitecture.Application.Articles;

public static class ArticleErrors
{
    public static readonly ErrorMessage ArticleNotFound = new(2000, "The specified article was not found");
    public static readonly ErrorMessage ArticleCreationUnauthorized = new(2001, "You do not have sufficient permissions for creating articles");
    public static readonly ErrorMessage ArticleEditUnauthorized = new(2002, "You do not have the required permissions to edit this article");
}
