namespace CabBookingApp.Models.ViewModels;

public class RegisterViewModel
{
    public string? FirstName { get; set; }

    [Required]
    [Display(Name = "LastName")]
    public string? LastName { get; set; }

    [Required]
    [StringLength(50)]
    [Display(Name = "Email")]
    public string? Email { get; set; }

    [Required]
    [StringLength(25)]
    [Display(Name = "Password")]
    public string? Password { get; set; }

    [Required]
    [StringLength(25)]
    [Compare(nameof(Password))]
    [Display(Name = "Confirm Password")]
    public string? ConfirmPassword { get; set; }

    [Required] public bool TermsAndConditions { get; set; }
}