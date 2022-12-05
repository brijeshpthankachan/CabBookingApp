namespace CabBookingApp.Models.ViewModels;

public class DriverViewModel
{
    [Required] public string LicenceNumber { get; set; }
    [Required] public string HouseNameOrNo { get; set; }
    [Required] public string District { get; set; }
    [Required] public string Locality { get; set; }
    [Required] public string State { get; set; }
    [Required] public int PinCode { get; set; }
    [Required] public int AadharNumber { get; set; }
    [Required] public long PhoneNumber { get; set; }
    [Required] public string RcNumber { get; set; }
    [Required] public string CabType { get; set; }
    [Required] public string CabName { get; set; }
    public ApplicationUser ApplicationUsers { get; set; }
    [Required] public string ApplicationUsersId { get; set; }
    [Required] public string WorkLocation { get; set; }


    public int IsApprovedToDrive { get; set; } = -1;
}