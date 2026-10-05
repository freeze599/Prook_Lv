using System.ComponentModel.DataAnnotations;

namespace GreenYellowSite.Models;

public class ContactFormModel
{
    [StringLength(120, ErrorMessage = "Pakalpojuma nosaukums ir pārāk garš.")]
    public string? Pakalpojums { get; set; }

    [StringLength(100, ErrorMessage = "Vārdam jābūt ne garākam par 100 rakstzīmēm.")]
    [Required(ErrorMessage = "Lūdzu ievadiet vārdu.")]
    public string Vārds { get; set; } = string.Empty;

    [Required(ErrorMessage = "Lūdzu ievadiet e-pastu.")]
    [StringLength(254, ErrorMessage = "E-pasta adrese ir pārāk gara.")]
    [EmailAddress(ErrorMessage = "Lūdzu ievadiet korektu e-pastu.")]
    public string Epasts { get; set; } = string.Empty;

    [StringLength(40, ErrorMessage = "Tālruņa numurs ir pārāk garš.")]
    [Phone(ErrorMessage = "Lūdzu ievadiet korektu tālruņa numuru.")]
    public string? Tālrunis { get; set; }

    [StringLength(5000, ErrorMessage = "Ziņai jābūt ne garākai par 5000 rakstzīmēm.")]
    [Required(ErrorMessage = "Lūdzu ievadiet ziņu.")]
    public string Ziņa { get; set; } = string.Empty;
}
