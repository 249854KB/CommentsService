using ForumsService.Models;
using Microsoft.EntityFrameworkCore;

namespace ForumsService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {

        }
        public DbSet<User> Users{ get; set; }
        public DbSet<Forum> Forums{ get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
            .Entity<User>()
            .HasMany(u => u.Forums)
            .WithOne(u=> u.User!)
            .HasForeignKey(u=>u.UserId);

            modelBuilder
            .Entity<Forum>()
            .HasOne(u=>u.User)
            .WithMany(u=>u.Forums)
            .HasForeignKey(u=>u.UserId);
        }
    }
}
