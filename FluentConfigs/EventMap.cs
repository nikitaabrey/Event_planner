using System;
using System.Collections.Generic;
using EventPlanner.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;



namespace EventPlanner.FluentConfigs
{
    public partial class EventMap
        : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            // table
            builder.ToTable("Event", "dbo");

            // key
            builder.HasKey(t => t.EventId);

            // properties
            builder.Property(t => t.EventId)
                .IsRequired()
                .HasColumnName("EventId")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.UserId)
                .IsRequired()
                .HasColumnName("UserId")
                .HasColumnType("int");

            builder.Property(t => t.RecurringId)
                .IsRequired()
                .HasColumnName("RecurringId")
                .HasColumnType("char(1)")
                .HasMaxLength(1);

            builder.Property(t => t.StartDate)
                .IsRequired()
                .HasColumnName("StartDate")
                .HasColumnType("date");

            builder.Property(t => t.EndDate)
                .IsRequired()
                .HasColumnName("EndDate")
                .HasColumnType("date");

            builder.Property(t => t.EventName)
                .IsRequired()
                .HasColumnName("EventName")
                .HasColumnType("varchar(31)")
                .HasMaxLength(31);

            builder.Property(t => t.StartTime)
                .HasColumnName("StartTime")
                .HasColumnType("time");

            builder.Property(t => t.EndTime)
                .HasColumnName("EndTime")
                .HasColumnType("time");

            builder.Property(t => t.EventDesc)
                .IsRequired()
                .HasColumnName("EventDesc")
                .HasColumnType("varchar(500)")
                .HasMaxLength(500);

            builder.Property(t => t.IsFullDay)
                .IsRequired()
                .HasColumnName("IsFullDay")
                .HasColumnType("bit");

            // relationships
            builder.HasOne(t => t.EndDateCalendar)
                .WithMany(t => t.EndDateEvents)
                .HasForeignKey(d => d.EndDate)
                .HasConstraintName("FK__Event__EndDate__7DB89C09");

            builder.HasOne(t => t.RecurringEvents)
                .WithMany(t => t.Events)
                .HasForeignKey(d => d.RecurringId)
                .HasConstraintName("FK__Event__Recurring__7BD05397");

            builder.HasOne(t => t.StartDateCalendar)
                .WithMany(t => t.StartDateEvents)
                .HasForeignKey(d => d.StartDate)
                .HasConstraintName("FK__Event__StartDate__7CC477D0");

            builder.HasOne(t => t.Users)
                .WithMany(t => t.Events)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Event__UserId__7ADC2F5E");

        }

        public struct Table
        {
            public const string Schema = "dbo";
            public const string Name = "Event";
        }

        public struct Columns
        {
            public const string EventId = "EventId";
            public const string UserId = "UserId";
            public const string RecurringId = "RecurringId";
            public const string StartDate = "StartDate";
            public const string EndDate = "EndDate";
            public const string EventName = "EventName";
            public const string StartTime = "StartTime";
            public const string EndTime = "EndTime";
            public const string EventDesc = "EventDesc";
            public const string IsFullDay = "IsFullDay";
        }
        
    }
}
