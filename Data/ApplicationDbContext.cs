
namespace CabBookingApp.Data;

public class ApplicationDbContext :  IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)   
    {
     
        
    }
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }

}