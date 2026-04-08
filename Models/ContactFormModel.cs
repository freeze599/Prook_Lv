using System.ComponentModel.DataAnnotations;

namespace GreenYellowSite.Models;

public class ContactFormModel
{
    [Required(ErrorMessage = "Lūdzu ievadiet vārdu.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Lūdzu ievadiet e-pastu.")]
    [EmailAddress(ErrorMessage = "Lūdzu ievadiet korektu e-pastu.")]
    public string Email { get; set; } = string.Empty;

    [Phone(ErrorMessage = "Lūdzu ievadiet korektu tālruņa numuru.")]
    public string? Phone { get; set; }

    [Required(ErrorMessage = "Lūdzu ievadiet ziņu.")]
    public string Message { get; set; } = string.Empty;
}
