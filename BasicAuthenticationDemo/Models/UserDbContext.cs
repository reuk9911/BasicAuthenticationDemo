using Microsoft.EntityFrameworkCore;
using BasicAuthenticationDemo.DTOs;

namespace BasicAuthenticationDemo.Models
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Device> Devices { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<User>().HasData(
        //        new User { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", Password = "password123" },
        //        new User { Id = 2, FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com", Password = "password123" }
        //    );
        //    modelBuilder.Entity<Device>().HasData(
        //        new Device { Id = 1, Type = "Type_1"},
        //        new Device { Id = 2, Type = "Type_1" },
        //        new Device { Id = 3, Type = "Type_2" },
        //        new Device { Id = 4, Type = "Type_2" },
        //        new Device { Id = 5, Type = "Type_2" },
        //        new Device { Id = 6, Type = "Type_2" }
        //    );
        //}
    }
}