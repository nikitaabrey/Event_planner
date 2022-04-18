using Microsoft.EntityFrameworkCore;
using EventPlanner.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventPlanner.FluentConfigs
{
    public partial class RecurringEventsMap
        : IEntityTypeConfiguration<RecurringEvents>
    {
        public void Configure(EntityTypeBuilder<RecurringEvents> builder)
        {
            // table
            builder.ToTable("RecurringEvents", "dbo");

            // key
            builder.HasKey(t => t.RecurringId);

            // properties
            builder.Property(t => t.RecurringId)
                .IsRequired()
                .HasColumnName("RecurringId")
                .HasColumnType("char(1)")
                .HasMaxLength(1);

            builder.Property(t => t.RecurringDesc)
                .IsRequired()
                .HasColumnName("RecurringDesc")
                .HasColumnType("varchar(30)")
                .HasMaxLength(30);

            // relationships
        }

        public struct Table
        {
            public const string Schema = "dbo";
            public const string Name = "RecurringEvents";
        }

        public struct Columns
        {
            public const string RecurringId = "RecurringId";
            public const string RecurringDesc = "RecurringDesc";
        }
    }
}
