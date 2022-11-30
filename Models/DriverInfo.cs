namespace CabBookingApp.Models.ViewModels;

public class DriverInfo
{
    [Required] [Key] public int Id { get; set; }
    [Required] public string? LicenceNumber { get; set; }
    [Required] public string AddressLineOneHouseNameOrHouseNo { get; set; }
    [Required] public string AddressLineTwoDistrict { get; set; }
    [Required] public string AddresLineThreeLocality { get; set; }
    [Required] public string AddresLineFourState { get; set; }
    [Required] public int AddresLineFivePin { get; set; }
    [Required] public int AadharNumber { get; set; }
    [Required] public long PhoneNumber { get; set; }
    [Required] public string PanNumber { get; set; }
    [Required] public ApplicationUser ApplicationUsers { get; set; }
    [Required] public string ApplicationUsersId { get; set; }
    public bool IsApprovedToDrive { get; set; } = false;
}