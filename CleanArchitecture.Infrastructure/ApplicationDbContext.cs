using CleanArchitecture.Domain.Articles;
using CleanArchitecture.Infrastructure.Authentication;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure;

public class ApplicationDbContext : IdentityDbContext<User>
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Article> Articles { get; set; }
}
