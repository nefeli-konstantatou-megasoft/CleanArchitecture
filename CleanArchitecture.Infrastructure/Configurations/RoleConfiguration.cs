using CleanArchitecture.Domain.Roles;
using CleanArchitecture.Infrastructure.Roles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Configurations;

internal sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("AspNetRoles");

        builder.HasKey(role => role.Id);

        builder.Property(role => role.Id)
            .HasMaxLength(450);

        builder.Property(role => role.Name)
            .IsRequired(false)
            .HasMaxLength(256);

        builder.Property(role => role.NormalizedName)
            .IsRequired(false);

        builder.Property(role => role.ConcurrencyStamp)
            .IsRequired(false)
            .IsFixedLength(false);

        builder.Property(role => role.Permissions)
            .IsRequired(true)
            .HasColumnType("BIGINT")
            .HasConversion<ulong>(
                permissions => (ulong)(RolePermissionFlags)permissions,
                permissions => (RolePermissions)(RolePermissionFlags)permissions);
    }
}
