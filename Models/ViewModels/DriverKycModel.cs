namespace CabBookingApp.Models.ViewModels;

public class DriverKycModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public long Phone { get; set; }

    [Required] public string VehicleRegistrationNumber { get; set; }
    [Required] public string CabType { get; set; }
     public DriverInfo DriverInfos { get; set; }
     public int DriverInfosID { get; set; }


    [Required] public string LicenceNumber { get; set; }
    [Required] public string Location { get; set; }
    [Required] public string AddressLineOneHouseNameOrHouseNo { get; set; }
    [Required] public string AddressLineTwoDistrict { get; set; }
    [Required] public string AddresLineThreeLocality { get; set; }
    [Required] public string AddresLineFourState { get; set; }
    [Required] public int AddresLineFivePin { get; set; }
    [Required] public int AadharNumber { get; set; }
    [Required] public long PhoneNumber { get; set; }
    [Required] public string PanNumber { get; set; }
    public ApplicationUser? ApplicationUsers { get; set; }
    public string ApplicationUsersId { get; set; }
    public CabInfo CabInfos { get; set; }
    public string CabInfosId { get; set; }
}