using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Nudes.Email.Smtp
{
    public class SmtpEmailService : IEmailService
    {
        private readonly SmtpEmailServiceConfiguration configuration;

        public SmtpEmailService(SmtpEmailServiceConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task SendEmail(EmailMessageView emailMessage, params string[] emails) => await SendEmail(emailMessage.Subject, emailMessage.Content, emails);

        public async Task SendEmail(EmailMessageView emailMessage, (Stream, string)[] attachments, params string[] emails) => await SendEmail(emailMessage.Subject, emailMessage.Content, attachments, emails);

        public async Task SendEmail(string subject, string content, params string[] emails) => await SendEmail(subject, content, new (Stream, String)[0], emails);

        public async Task SendEmail(string subject, string content, (Stream, string)[] attachments, params string[] emails)
        {
            if (attachments == null) throw new ArgumentNullException(nameof(attachments));
            using (var client = CreateClient())
            {
                using (var emailMessage = new MailMessage()
                {
                    From = new MailAddress(configuration.Username),
                    Subject = subject,
                    Body = content,
                    IsBodyHtml = true,
                })
                {
                    foreach (var attachment in attachments) emailMessage.Attachments.Add(new Attachment(attachment.Item1, attachment.Item2));

                    foreach (var email in emails) emailMessage.To.Add(new MailAddress(email));

                    await client.SendMailAsync(emailMessage);
                }
            }
        }

        protected virtual SmtpClient CreateClient()
        {
            return new SmtpClient()
            {
                Host = configuration.Host,
                Port = configuration.Port,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential()
                {
                    UserName = configuration.Username,
                    Password = configuration.Password
                }
            };
        }
    }
}
