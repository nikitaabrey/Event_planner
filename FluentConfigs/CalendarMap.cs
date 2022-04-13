using EventPlanner.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventPlanner.FluentConfigs
{
    public partial class CalendarMap
        : IEntityTypeConfiguration<Calendar>
    {
        public void Configure(EntityTypeBuilder<Calendar> builder)
        {
            // table
            builder.ToTable("Calendar", "dbo");

            // key
            builder.HasKey(t => t.FullDate);

            // properties
            builder.Property(t => t.FullDate)
                .IsRequired()
                .HasColumnName("FullDate")
                .HasColumnType("date");

            builder.Property(t => t.Year)
                .IsRequired()
                .HasColumnName("Year")
                .HasColumnType("int");

            builder.Property(t => t.Month)
                .IsRequired()
                .HasColumnName("Month")
                .HasColumnType("int");

            builder.Property(t => t.Day)
                .IsRequired()
                .HasColumnName("Day")
                .HasColumnType("int");

            builder.Property(t => t.DayOfWeek)
                .HasColumnName("DayOfWeek")
                .HasColumnType("int");

            builder.Property(t => t.WeekOfMonth)
                .HasColumnName("WeekOfMonth")
                .HasColumnType("int");

            builder.Property(t => t.FirstOfWeek)
                .HasColumnName("FirstOfWeek")
                .HasColumnType("date");

            builder.Property(t => t.LastOfWeek)
                .HasColumnName("LastOfWeek")
                .HasColumnType("date");

            builder.Property(t => t.DayOfYear)
                .HasColumnName("DayOfYear")
                .HasColumnType("int");

            // relationships
        }

        public struct Table
        {
            public const string Schema = "dbo";
            public const string Name = "Calendar";
        }

        public struct Columns
        {
            public const string FullDate = "FullDate";
            public const string Year = "Year";
            public const string Month = "Month";
            public const string Day = "Day";
            public const string DayOfWeek = "DayOfWeek";
            public const string WeekOfMonth = "WeekOfMonth";
            public const string FirstOfWeek = "FirstOfWeek";
            public const string LastOfWeek = "LastOfWeek";
            public const string DayOfYear = "DayOfYear";
        }
    }
}
