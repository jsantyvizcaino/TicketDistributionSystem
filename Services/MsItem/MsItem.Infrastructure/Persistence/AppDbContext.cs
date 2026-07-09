using Microsoft.EntityFrameworkCore;
using MsItem.Domain.Entities;
using MsItem.Infrastructure.Persistence.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsItem.Infrastructure.Persistence;

/// <summary>
/// Contexto de base de datos para MsItems
/// SQL Server - Code First
/// </summary>
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<WorkItem> WorkItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Aplicar configuraciones
        modelBuilder.ApplyConfiguration(new WorkItemConfiguration());

    }
}
