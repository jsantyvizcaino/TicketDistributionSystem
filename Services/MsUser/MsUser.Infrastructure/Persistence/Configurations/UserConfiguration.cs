using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MsUser.Domain.Entities;

namespace MsUser.Infrastructure.Persistence.Configurations;

/// <summary>
/// Configuración de Fluent API para la entidad <see cref="User"/>:
/// clave primaria, restricciones de longitud, valores por defecto e índices.
/// </summary>
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    /// <summary>
    /// Configura el mapeo de <see cref="User"/> a la tabla de base de datos.
    /// </summary>
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Username)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(u => u.FullName)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(u => u.IsActive)
            .HasDefaultValue(true);

        builder.Property(u => u.CreatedAt)
            .HasDefaultValueSql("GETUTCDATE()");

        // Índices
        builder.HasIndex(u => u.Username)
            .IsUnique()
            .HasDatabaseName("IX_User_Username");

        builder.HasIndex(u => u.IsActive)
            .HasDatabaseName("IX_User_IsActive");
    }
}
