using GreenYellowSite.Data;
using GreenYellowSite.Models;
using GreenYellowSite.Services;
using Microsoft.AspNetCore.Mvc;
namespace GreenYellowSite.Controllers
{
    public class SiteController : Controller
    {
        private readonly IEmailService _emailService;

        public SiteController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [Route("services")]
        public IActionResult Services()
        {
            return View(ServiceCatalog.All);
        }

        [Route("services/{slug}")]
        public IActionResult ServiceDetail(string slug)
        {
            var service = ServiceCatalog.GetBySlug(slug);

            if (service is null)
            {
                return NotFound();
            }

            return View(service);
        }

        [HttpGet]
        [Route("contact")]
        public IActionResult Contact(string? service)
        {
            var selectedService = string.IsNullOrWhiteSpace(service) ? null : ServiceCatalog.GetBySlug(service);
            return View(new ContactFormModel { Pakalpojums = selectedService?.Title });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("contact")]
        public async Task<IActionResult> Contact(ContactFormModel model)
        {
            if (!string.IsNullOrWhiteSpace(model.Pakalpojums) && !ServiceCatalog.All.Any(s => s.Title == model.Pakalpojums))
            {
                ModelState.AddModelError(nameof(model.Pakalpojums), "Lūdzu izvēlieties pakalpojumu no saraksta.");
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await _emailService.SendContactEmailAsync(model);
                TempData["SuccessMessage"] = "Paldies! Jūsu ziņa ir nosūtīta.";
                return RedirectToAction(nameof(Contact));
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Neizdevās nosūtīt ziņu. Lūdzu mēģiniet vēlreiz.");
                return View(model);
            }
        }
    
       
    }
}
