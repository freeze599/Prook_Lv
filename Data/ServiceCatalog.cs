using GreenYellowSite.Models;

namespace GreenYellowSite.Data;

public static class ServiceCatalog
{
    public static readonly List<Service> All = new()
{
    new Service
    {
        Slug = "automatiska-laistisanas-sistema",
        Title = "Zālāja automātiskā laistīšana",
        ShortDescription = "Automatizēta laistīšana ērtai un vienmērīgai teritorijas kopšanai.",
        Description = "Uzstādām automātiskās laistīšanas sistēmas privātmājām un komerciālām teritorijām, nodrošinot efektīvu ūdens izmantošanu un veselīgu zālāju.",
        Icon = "💧"
    },
    new Service
    {
        Slug = "velenas-griezeja-noma",
        Title = "Velēnas griezēja noma",
        ShortDescription = "Aprīkojuma noma teritorijas labiekārtošanas darbiem.",
        Description = "Piedāvājam velēnas griezēja nomu ērtākai un efektīvākai zāliena atjaunošanai, labiekārtošanai un sagatavošanas darbiem.",
        Icon = "🛠️"
    },
    new Service
    {
        Slug = "gudras-majas",
        Title = "Gudrās mājas",
        ShortDescription = "Gudri un automatizēti risinājumi ikdienas ērtībai.",
        Description = "Integrējam gudrās mājas risinājumus apgaismojumam, klimatam, drošībai un citām funkcijām, padarot vidi ērtāku un modernāku.",
        Icon = "🏠"
    },
    new Service
    {
        Slug = "videonoverosanas-sistemas",
        Title = "Videonovērošanas sistēmas",
        ShortDescription = "Kvalitatīvas kameras un attālināta piekļuve video novērošanai.",
        Description = "Uzstādām video novērošanas sistēmas privātmājām, birojiem, noliktavām un citām teritorijām, nodrošinot pārskatāmu un drošu kontroli.",
        Icon = "📹"
    },
    new Service
    {
        Slug = "piekluves-sistemas",
        Title = "Piekļuves sistēmas",
        ShortDescription = "Durvju, vārtu un telpu piekļuves kontrole ērtākai drošībai.",
        Description = "Ieviešam piekļuves sistēmas birojiem, dzīvojamām ēkām un uzņēmumiem, lai kontrolētu personu plūsmu un piekļuves tiesības.",
        Icon = "🚪"
    },
    new Service
    {
        Slug = "vartu-automatika",
        Title = "Vārtu automātika",
        ShortDescription = "Automatizēti risinājumi pagalma un teritorijas vārtiem.",
        Description = "Uzstādām vārtu automātiku dažādiem objektiem, nodrošinot ērtu lietošanu, drošību un ilgmūžību.",
        Icon = "🚘"
    },
    new Service
    {
        Slug = "apsardzes-sistemas",
        Title = "Apsardzes sistēmu risinājumi",
        ShortDescription = "Mūsdienīgas apsardzes sistēmas mājām, uzņēmumiem un teritorijām.",
        Description = "Projektējam un uzstādām apsardzes sistēmas, kas palīdz pasargāt īpašumu un nodrošina ātru reaģēšanu uz drošības riskiem.",
        Icon = "🛡️"
    },
    new Service
    {
        Slug = "datu-tiklu-izbuve",
        Title = "Datu tīklu izbūve",
        ShortDescription = "Stabili un pārdomāti tīkla risinājumi dažādiem objektiem.",
        Description = "Veicam datu tīklu projektēšanu un izbūvi, nodrošinot drošu savienojumu, kvalitatīvu infrastruktūru un ērtu paplašināšanu nākotnē.",
        Icon = "🌐"
    },
    new Service
    {
        Slug = "ugunsdrosiba",
        Title = "Ugunsdrošība",
        ShortDescription = "Ugunsdrošības risinājumi un signalizācijas sistēmas.",
        Description = "Piedāvājam ugunsdrošības sistēmu uzstādīšanu un pielāgošanu, lai savlaicīgi konstatētu riskus un uzlabotu objekta drošību.",
        Icon = "🔥"
    }
};

    public static Service? GetBySlug(string slug)
    {
        return All.FirstOrDefault(x => x.Slug == slug);
    }
}