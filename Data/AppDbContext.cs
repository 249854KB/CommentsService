using CommentsService.Models;
using Microsoft.EntityFrameworkCore;

namespace CommentsService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {

        }
        public DbSet<Forum> Forums{ get; set; }
        public DbSet<Comment> Comments{ get; set;}
         public DbSet<User> Users{ get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
            .Entity<User>()
            .HasMany(u => u.Forums)
            .WithOne(u=> u.User!)
            .HasForeignKey(u=>u.UserId);

            
            modelBuilder
            .Entity<Forum>()
            .HasMany(f => f.Comments)
            .WithOne(f=> f.Forum!)
            .HasForeignKey(f=>f.ForumId);

            modelBuilder
            .Entity<Comment>()
            .HasOne(f=>f.Forum)
            .WithMany(f=>f.Comments)
            .HasForeignKey(f=>f.ForumId);
        }
    }
}
