using Nudes.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample
{
    public class TesteService
    {
        private readonly IEmailService _emailService;

        public TesteService(IEmailService emailService)
        {
            _emailService = emailService;
        }

        /// <summary>
        /// Send a email via SMTP
        /// </summary>
        /// <param name="subject">It's the email subject</param>
        /// <param name="content">It's the body of the email</param>
        /// <param name="email">Recipient</param>
        /// <returns></returns>
        public async Task SendEmailTeste(string subject, string content, string email)
        {
            await _emailService.SendEmail(subject, content, email);
        }
    }
}
