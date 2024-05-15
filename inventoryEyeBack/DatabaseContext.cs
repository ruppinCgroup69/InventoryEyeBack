
namespace inventoryeyeback;

using Microsoft.EntityFrameworkCore;

public class DatabaseContext : DbContext {


    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {

    }

    public DbSet<User>? Users { get; set; }
    public DbSet<Post>? posts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configuring the primary key for the Post entity
        modelBuilder.Entity<Post>()
            .HasKey(p => p.PostId);

        // Configuring the relationship between the Post and User entities
        modelBuilder.Entity<Post>()
            .HasOne(p => p.User)          // Each Post has one User
            .WithMany()       // Each User has many Posts
            .HasForeignKey(p => p.UserId); // Foreign key in Post entity is UserId

        base.OnModelCreating(modelBuilder);
    
    
    }


}





