# Nudes.EmailSender
EmailSender as the name itself already says, was a lib created by SENAI Informática to lessen the need for configuration writing and email sending methods.

# Installing
###### Using Package Manager Console:
```
Install-Package EmailSender
```

###### You can:
  - Send email passing subject, content and recipient email;
  - Send email with template in content

# Examples
### We'll first show you the basic implementation of email sending!

##### ViewModel
#
```csharp
public class EmailViewModel
{
    public string Subject { get; set; }
    public string Content { get; set; }
    public string Email { get; set; }
}
```


##### POST 
#
```csharp
[HttpPost("email")]
public async Task SendEmailTest([FromBody] EmailViewModel model)
{
    await _testeService.SendEmailTeste(model.Subject, model.Content, model.Email);
}
```

##### Service
#
```csharp
public async Task SendEmailTeste(string subject, string content, string email)
{
    // method used to send emails
    await _emailService.SendEmail(subject, content, email);
}
```

###### *We use a very basic cshtml, just to demonstrate a test implementation of lib*
#
##### cshtml used:
#
```html
<b> @Model.Nome </b>
@for (int i = 0; i < 10; i++)
{
    <b>@Model.Nome @i</b> <br/>
}
```

##### Example of implementation:
#
```csharp
public async Task SendEmail(EmailMessageView message, string email, object model)
{
    var template = GetTemplateHtml();
    message.Content = template;
    
    await _emailService.SendEmailWithBodyTemplate(message.Subject, message.Content,model, email);
}
```

### Startup.cs
##### We must enter smtp settings in startup, smtp settings can be added in Users Secrets.

```csharp
{
  "EmailConfigs": {
    "provider": "smtp.gmail.com",
    "emailFrom": "no-reply@gmail.com",
    "port": 587,
    "networkCredentialEmail": "emailTest@gmail.com",
    "networkCredentialPassword": "password"
  }
}
```

###### Use SmtpEmailServiceConfiguration for your email sending settings 
#
```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
    services.AddTransient<TesteService>();
    services.AddSmtpEmailConfiguration(_ => new SmtpEmailServiceConfiguration
    {
        Host = Configuration["EmailConfigs:Provider"],
        Username = Configuration["EmailConfigs:Username"],
        Password = Configuration["EmailConfigs:Password"],
        Port = Convert.ToInt32(Configuration["EmailConfigs:Port"]),
    });
}