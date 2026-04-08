

using GreenYellowSite.Models;
namespace GreenYellowSite.Services;

public interface IEmailService
{
    Task SendContactEmailAsync(ContactFormModel model);
}
