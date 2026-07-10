using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MsItem.Domain.Entities;

namespace MsItem.Infrastructure.Persistence.Configurations;

public class WorkItemConfiguration : IEntityTypeConfiguration<WorkItem>
{
    public void Configure(EntityTypeBuilder<WorkItem> builder)
    {
        builder.HasKey(w => w.Id);

        builder.Property(w => w.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(w => w.Description)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Property(w => w.AssignedUsername)
            .HasMaxLength(100);

        builder.Property(w => w.Relevance)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(w => w.Status)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(w => w.CreatedAt)
            .HasDefaultValueSql("GETUTCDATE()");

        // Índices
        builder.HasIndex(w => w.AssignedUsername)
            .HasDatabaseName("IX_WorkItem_AssignedUsername");

        builder.HasIndex(w => w.Status)
            .HasDatabaseName("IX_WorkItem_Status");
    }
}
