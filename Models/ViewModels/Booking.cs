namespace CabBookingApp.Models.ViewModels;

public class Booking
{
    [Key] public int Id { get; set; }

    [Required] public string? UserId { get; set; }
    [Required] public string DriverName { get; set; }
    [Required] public string? DriverId { get; set; }
    [Required] public string? CabName { get; set; }
    [Required] public string? CabType { get; set; }
    [Required] public string? DriverPhoneNumber { get; set; }
    [Required] public DateTime? BookingTime { get; set; }
    [Required] public string? Source { get; set; }
    [Required] public string? Destination { get; set; }
    public int BookingStatus { get; set; }
}