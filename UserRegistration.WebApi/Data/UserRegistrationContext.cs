using Microsoft.EntityFrameworkCore;
using UserRegistration.WebApi.Models;

namespace UserRegistration.WebApi.Data
{
    public class UserRegistrationContext : DbContext
    {
        const string dataBaseName = "UserRegistration";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(dataBaseName);
        }

        public DbSet<User> Users { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(e => e.Id);
            modelBuilder.Entity<User>().Property(e => e.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<User>().Property(e => e.Password).IsRequired();
            modelBuilder.Entity<User>().Property(e => e.Login).IsRequired();
            modelBuilder.Entity<User>().HasIndex(nameof(User.Login), nameof(User.Password)).IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
