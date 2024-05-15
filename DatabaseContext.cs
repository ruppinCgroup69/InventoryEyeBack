
namespace inventoryeyeback;

using Microsoft.EntityFrameworkCore;

public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options) {

    public DbSet<User> Users {get; set; }
        
}