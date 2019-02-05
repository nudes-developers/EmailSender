using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nudes.Email.Arguments.SmtpEmail;
using Nudes.Email.Infrastructure;
using Nudes.Email.Interfaces;

namespace Nudes.Email.Smtp
{
    public class SmtpEmailService : IEmailService
    {
        private readonly SmtpEmailServiceConfiguration configuration;
        private readonly ILogger logger;

        public SmtpEmailService(SmtpEmailServiceConfiguration configuration, ILogger<SmtpEmailService> logger)
        {
            this.configuration = configuration;
            this.logger = logger;
        }

        public async Task SendEmail(EmailMessageRequest emailMessage, params string[] emails) => await SendEmail(emailMessage.Subject, emailMessage.Content, emails);
        
        public async Task SendEmail(EmailMessageRequest emailMessage, (Stream, string)[] attachments, params string[] emails) => await SendEmail(emailMessage.Subject, emailMessage.Content, attachments, emails);
        
        public async Task SendEmail(string subject, string content, params string[] emails) => await SendEmail(subject, content, new (Stream, String)[0], emails);
        
        public async Task SendEmail(string subject, string content, (Stream, string)[] attachments, params string[] emails)
        {
            if (attachments == null) throw new ArgumentNullException(nameof(attachments));
            try
            {
                using (var client = new SmtpClient())
                {
                    var credential = new NetworkCredential()
                    {
                        UserName = configuration.Username,
                        Password = configuration.Password
                    };

                    client.Credentials = credential;
                    client.Host = configuration.Host;
                    client.Port = configuration.Port;
                    client.EnableSsl = true;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;

                    using (var emailMessage = new MailMessage()
                    {
                        From = new MailAddress(configuration.Username),
                        Subject = subject,
                        Body = content,
                    })
                    {
                        foreach (var attachment in attachments) emailMessage.Attachments.Add(new Attachment(attachment.Item1, attachment.Item2));

                        foreach (var email in emails) emailMessage.To.Add(new MailAddress(email));

                        logger?.LogInformation($"sending email to {String.Join("; ", emailMessage.To.Select(d => d.Address))} with {attachments?.Length ?? 0} attachments");
                        await client.SendMailAsync(emailMessage);
                    }
                }
            }
            catch (SmtpException ex)
            {
                logger.LogCritical("Wasn't posssible sent email because service throws a smtp exception");
                logger.LogCritical("Exception: \n\t" + ex.ToString());
            }
        }
    }
}
