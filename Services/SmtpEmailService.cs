using GreenYellowSite.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace GreenYellowSite.Services;

public class SmtpEmailService : IEmailService
{
    private readonly EmailSettings _settings;

    public SmtpEmailService(IOptions<EmailSettings> settings)
    {
        _settings = settings.Value;
    }

    public async Task SendContactEmailAsync(ContactFormModel model)
    {
        var message = new MimeMessage();

        message.From.Add(new MailboxAddress(_settings.FromName, _settings.FromEmail));
        message.To.Add(MailboxAddress.Parse(_settings.ToEmail));
        message.ReplyTo.Add(MailboxAddress.Parse(model.Email));

        message.Subject = $"Jauns konsultācijas pieteikums no {model.Name}";

        message.Body = new TextPart("plain")
        {
            Text =
$@"Jauns pieteikums no mājaslapas

Vārds: {model.Name}
E-pasts: {model.Email}
Tālrunis: {model.Phone}
Ziņa:
{model.Message}"
        };

        using var client = new SmtpClient();

        await client.ConnectAsync(_settings.Host, _settings.Port, SecureSocketOptions.StartTls);
        await client.AuthenticateAsync(_settings.UserName, _settings.Password);
        await client.SendAsync(message);
        await client.DisconnectAsync(true);
    }
}