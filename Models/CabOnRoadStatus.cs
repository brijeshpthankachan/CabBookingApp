using System.ComponentModel.DataAnnotations.Schema;

namespace CabBookingApp.Models;

public class CabOnRoadStatus
{
    [Key]
    public int Id { get; set; }

    public string ApplicationUserID { get; set; } 
    public ApplicationUser ApplicationUser { get; set; }
    
    public bool IsOnRoad { get; set; } = false;

    public bool IsDriving { get; set; } = true;
    
}