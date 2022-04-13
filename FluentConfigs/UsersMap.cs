using Microsoft.EntityFrameworkCore;
using EventPlanner.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventPlanner.FluentConfigs
{
    public partial class UsersMap
        : IEntityTypeConfiguration<Users>
    {
        public void Configure(EntityTypeBuilder<Users> builder)
        {
            // table
            builder.ToTable("Users", "dbo");

            // key
            builder.HasKey(t => t.UserId);

            // properties
            builder.Property(t => t.UserId)
                .IsRequired()
                .HasColumnName("UserId")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.FirstName)
                .IsRequired()
                .HasColumnName("FirstName")
                .HasColumnType("varchar(31)")
                .HasMaxLength(31);

            builder.Property(t => t.LastName)
                .IsRequired()
                .HasColumnName("LastName")
                .HasColumnType("varchar(31)")
                .HasMaxLength(31);

            builder.Property(t => t.Email)
                .IsRequired()
                .HasColumnName("Email")
                .HasColumnType("varchar(200)")
                .HasMaxLength(200);

            // relationships
        }

        public struct Table
        {
            public const string Schema = "dbo";
            public const string Name = "Users";
        }

        public struct Columns
        {
            public const string UserId = "UserId";
            public const string FirstName = "FirstName";
            public const string LastName = "LastName";
            public const string Email = "Email";
        }
    }
}
