using CleanArchitecture.Domain.Roles;
using CleanArchitecture.Infrastructure.Roles;
using CleanArchitecture.Infrastructure.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace CleanArchitecture.Infrastructure.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("AspNetUsers");

        builder.HasKey(role => role.Id);
        builder.Property(role => role.Id)
            .HasAnnotation("DatabaseGeneratedAttribute", DatabaseGeneratedOption.Identity)
            .HasMaxLength(450);

        builder.HasMany(user => user.Articles)
            .WithOne(article => (User?)article.Author)
            .HasForeignKey(article => article.UserId);
    }
}
