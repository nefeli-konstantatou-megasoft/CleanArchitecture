using CleanArchitecture.Domain.Articles;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Repositories;

public class ArticleRepository : IArticleRepository
{
    private readonly ApplicationDbContext _context;

    public ArticleRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Article>> GetAllArticlesAsync()
        => await _context.Articles
            .Include(article => article.Author)
            .OrderByDescending(article => article.DatePublished)
            .ToListAsync();

    public async Task<List<Article>> GetAllPublishedArticlesAsync()
        => await _context.Articles
            .Include(article => article.Author)
            .OrderByDescending(article => article.DatePublished)
            .Where(article => article.IsPublished)
            .ToListAsync();

    public async Task<List<Article>> GetArticlesByUserId(string userId)
    {
        var result =  await _context.Articles
            .Include(article => article.Author)
            .Where(article => article.UserId == userId)
            .OrderByDescending(article => article.DatePublished)
            .OrderBy(article => article.IsPublished)
            .ToListAsync();
        return result;
    }

    public async Task<Article> CreateArticleAsync(Article article)
    {
        _context.Articles.Add(article);
        await _context.SaveChangesAsync();
        return article;
    }

    public async Task<Article?> GetArticleByIdAsync(int id)
    {
        var article = await _context.Articles.FindAsync(id);
        return article;
    }

    public async Task<Article?> UpdateArticleAsync(Article article)
    {
        var articleToUpdate = await GetArticleByIdAsync(article.Id);
        if (articleToUpdate is null)
            return null;

        articleToUpdate.Title = article.Title;
        articleToUpdate.Content = article.Content;
        articleToUpdate.DatePublished = article.DatePublished;
        articleToUpdate.IsPublished = article.IsPublished;
        articleToUpdate.DateUpdated = DateTime.Now;

        await _context.SaveChangesAsync();
        return articleToUpdate;
    }

    public async Task<bool> UpdateArticlePublishAsync(int id, bool isPublished)
    {
        var articleToUpdate = await GetArticleByIdAsync(id);
        if (articleToUpdate is null)
            return false;

        articleToUpdate.IsPublished = isPublished;

        int result = await _context.SaveChangesAsync();
        return result == 0;
    }

    public async Task<bool> DeleteArticleByIdAsync(int id)
    {
        var article = await GetArticleByIdAsync(id);
        if (article is null)
            return false;

        _context.Articles.Remove(article);
        await _context.SaveChangesAsync();

        return true;
    }
}
