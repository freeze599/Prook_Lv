namespace GreenYellowSite.Data;

public static class ServiceGroups
{
    public static string Key(string slug) => slug switch
    {
        "automatiska-laistisanas-sistema" or "velenas-griezeja-noma" => "pagalmam",
        "videonoverosanas-sistemas" or "piekluves-sistemas" or "apsardzes-sistemas" or "ugunsdrosiba" => "drosibai",
        _ => "ertibai"
    };

    public static readonly (string Key, string Title, string Icon, string Description)[] All =
    {
        ("pagalmam", "Pagalmam", "garden", "Automātiskā laistīšana un velēnas griezēja noma teritorijas labiekārtošanai."),
        ("drosibai", "Drošībai", "shield", "Videonovērošana, apsardze, piekļuves kontrole un ugunsdrošības sistēmas."),
        ("ertibai", "Ikdienas ērtībai", "home", "Vārtu automātika, gudrās mājas risinājumi un datu tīklu izbūve.")
    };
}
