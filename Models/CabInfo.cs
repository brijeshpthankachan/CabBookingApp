namespace CabBookingApp.Models.ViewModels;

public class CabInfo
{
    [Key] public int Id { get; set; }
    [Required] public string VehicleRegistrationNumber { get; set; }
    [Required] public string CabType { get; set; }
    [Required] public DriverInfo DriverInfos { get; set; }
    [Required] public int DriverInfosID { get; set; }
}