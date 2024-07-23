using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;


namespace GameHopper.Services;
public class EmailService
{
    private readonly SmtpClient _smtpClient;

    public EmailService()
    {
        _smtpClient = new SmtpClient("smtp.example.com")
        {
            Credentials = new NetworkCredential("username", "password"),
            EnableSsl = true
        };
    }

    public async Task SendEmailAsync(string to, string subject, string body)
    {
        var mailMessage = new MailMessage("noreply@example.com", to, subject, body);
        await _smtpClient.SendMailAsync(mailMessage);
    }
}
