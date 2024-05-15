using Core.Models;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;

namespace Core.Services;

public class EmailService : IEmailService
{
  private readonly SmtpClient smtpClient;

  public EmailService(IOptions<EmailServiceOptions> options)
  {
    smtpClient = new SmtpClient(options.Value.Host, options.Value.Port)
    {
      Credentials = new NetworkCredential(options.Value.Username, options.Value.Password),
      EnableSsl = options.Value.EnableSsl
    };
  }

  public async Task SendEmailAsync(string email, string subject, string htmlMessage)
  {
    using var message = new MailMessage();
    message.To.Add(email);
    message.Subject = subject;
    message.Body = htmlMessage;
    message.IsBodyHtml = true;

    await smtpClient.SendMailAsync(message);
  }
}
