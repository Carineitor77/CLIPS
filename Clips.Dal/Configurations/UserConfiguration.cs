using Clips.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clips.Dal.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(user => user.Id);
            builder.HasIndex(user => user.Email).IsUnique();

            builder.Property(user => user.Name).IsRequired().HasMaxLength(25);
            builder.Property(user => user.Email).IsRequired().HasMaxLength(35);
            builder.Property(user => user.Age).IsRequired();
            builder.Property(user => user.Password).IsRequired();
            builder.Property(user => user.Phone).IsRequired().HasMaxLength(10);

            builder.HasOne(user => user.Role)
                   .WithMany(role => role.Users)
                   .HasForeignKey(user => user.RoleId);
        }
    }
}