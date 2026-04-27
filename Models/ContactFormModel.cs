using System.ComponentModel.DataAnnotations;

namespace GreenYellowSite.Models;

public class ContactFormModel
{
    [Required(ErrorMessage = "Lūdzu ievadiet vārdu.")]
    public string Vārds { get; set; } = string.Empty;

    [Required(ErrorMessage = "Lūdzu ievadiet e-pastu.")]
    [EmailAddress(ErrorMessage = "Lūdzu ievadiet korektu e-pastu.")]
    public string Epasts { get; set; } = string.Empty;

    [Phone(ErrorMessage = "Lūdzu ievadiet korektu tālruņa numuru.")]
    public string? Tālrunis { get; set; }

    [Required(ErrorMessage = "Lūdzu ievadiet ziņu.")]
    public string Ziņa { get; set; } = string.Empty;
}
