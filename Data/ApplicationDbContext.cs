
namespace CabBookingApp.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)   
    {
        
    }
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }

}