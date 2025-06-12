using Microsoft.EntityFrameworkCore;

namespace AppLogin.Api
{
    public class AppDBContext : DbContext
    {
        public DbSet<Models.User> User { get; set; }

        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.User>().ToTable("Users");
            modelBuilder.Entity<Models.User>().HasKey(u => u.Id);
            modelBuilder.Entity<Models.User>().Property(u => u.Username).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Models.User>().Property(u => u.Email).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Models.User>().Property(u => u.Password).IsRequired().HasMaxLength(100);
        }
    }
}