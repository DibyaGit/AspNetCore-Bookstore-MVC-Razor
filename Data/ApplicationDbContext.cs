using Microsoft.EntityFrameworkCore;
using BookstoreApp.Models;

namespace BookstoreApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed initial data
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Username = "admin", Password = "password", Role = "Admin" },
                new User { Id = 2, Username = "user", Password = "password", Role = "User" }
            );

            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, Title = "Clean Code", Author = "Robert C. Martin", ISBN = "978-0132350884", Price = 45.00m },
                new Book { Id = 2, Title = "Design Patterns", Author = "Erich Gamma", ISBN = "978-0201633610", Price = 50.00m }
            );
        }
    }
}
