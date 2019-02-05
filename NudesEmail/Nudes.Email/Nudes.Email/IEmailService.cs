using System;
using System.IO;
using System.Threading.Tasks;

namespace Nudes.Email
{
    public interface IEmailService
    {
        Task SendEmail(EmailMessageRequest emailMessage, params string[] emails);
        Task SendEmail(EmailMessageRequest emailMessage, (Stream, String)[] attachments, params string[] emails);
        Task SendEmail(string subject, string content, params string[] emails);
        Task SendEmail(string subject, string content, (Stream, String)[] attachments, params string[] emails);
    }
}
