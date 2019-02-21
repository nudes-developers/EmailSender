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

        public async Task SendEmail(EmailMessageView message, string email, object model)
        {
            try
            {
                var template = GetTemplateHtml();

                message.Content = template;
                await _emailService.SendEmailWithBodyTemplate(message.Subject, message.Content, model, email);

            }
            catch (Exception e)
            {
                throw;
            }
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
