using Clips.Models;
using Microsoft.EntityFrameworkCore;

namespace Clips.Dal.SeedData
{
    public static class SeedData
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            // Set of Users
            modelBuilder.Entity<User>().HasData(
                new User()
                {
                    Id = 1,
                    Name = "Anatoly",
                    Email = "anatoly@email.com",
                    Age = 18,
                    Password = "1HwyNX^D5lt1&xe3",
                    Phone = "0953346432",
                    RoleId = 1
                },
                new User()
                {
                    Id = 2,
                    Name = "Bob",
                    Email = "bob@email.com",
                    Age = 42,
                    Password = "90OWn8pd=2c5ba_m",
                    Phone = "0973365864",
                    RoleId = 1
                }
            );

            // Set of Roles
            modelBuilder.Entity<Role>().HasData(
                new Role()
                {
                    Id = 1,
                    Name = "User"
                }
            );
        }
    }
}
