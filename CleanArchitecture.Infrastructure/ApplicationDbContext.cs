using CleanArchitecture.Domain.Articles;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Article> Articles { get; set; }
}
