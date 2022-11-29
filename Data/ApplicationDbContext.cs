
namespace CabBookingApp.Data;

public class ApplicationDbContext :  IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)   
    {
     
        
    }
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public DbSet<DriverInfo> DriverInfos { get; set; }
    public DbSet<CabInfo> CabInfos  { get; set; }

}