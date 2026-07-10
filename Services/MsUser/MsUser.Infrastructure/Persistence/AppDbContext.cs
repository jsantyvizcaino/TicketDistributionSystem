using Microsoft.EntityFrameworkCore;
using MsUser.Domain.Entities;
using MsUser.Infrastructure.Persistence.Configurations;
using MsUser.Infrastructure.Persistence.Seed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsUser.Infrastructure.Persistence;

/// <summary>
/// Contexto de base de datos para MsUsers
/// SQL Server - Code First
/// </summary>
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    /// <summary>
    /// Conjunto de usuarios persistidos.
    /// </summary>
    public DbSet<User> Users { get; set; }

    /// <summary>
    /// Aplica las configuraciones de todas las entidades del contexto.
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Aplicar configuraciones
        modelBuilder.ApplyConfiguration(new UserConfiguration());

    }
}
