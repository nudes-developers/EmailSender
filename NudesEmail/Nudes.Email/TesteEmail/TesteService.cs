using Nudes.Email;
using System;
using System.IO;
using System.Threading.Tasks;

namespace TesteEmail
{
    public class TesteService
    {
        private readonly IEmailService _emailService;
        public TesteService(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task SendEmailWithTemplate(EmailMessageView message, string email, object model)
        {
            var template = GetTemplateHtml();

            message.Content = template;
            string strTemplatePath = "Views\\User\\Teste2.cshtml";
            await _emailService.SendEmailWithBodyTemplate(message.Subject, strTemplatePath , model, email);
        }

        public async Task SendEmailTeste(string subject, string content, string email)
        {
            // method used to send emails
            await _emailService.SendEmail(subject, content, email);
        }

        public string GetTemplateHtml()
        {
            string strTemplatePath = "Views\\User\\Teste2.cshtml";
            var strMail = "";
            using (StreamReader objReader = new StreamReader(strTemplatePath))
            {
                // Lê todo o arquivo e o joga em uma variável
                strMail = objReader.ReadToEnd();
            }
            return strMail;
        }
    }
}
