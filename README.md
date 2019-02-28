# Nudes.EmailSender
EmailSender as the name itself already says, was a lib created by SENAI Informática to lessen the need for configuration writing and email sending methods.

# Installing
###### Using Package Manager Console:
#
```
Install-Package EmailSender
```

###### You can:
  - Send email passing subject, content and recipient email;
  - Send email with template in content

# Examples
### We'll first show you the basic implementation of email sending!

#####     Method
#
```csharp
/// <summary>
/// Send a email via SMTP
/// </summary>
/// <param name="subject">It's the email subject</param>
/// <param name="content">It's the body of the email</param>
/// <param name="email">Recipient</param>
/// <returns></returns>
public async Task SendEmailTeste(string subject, string content, string email)
{
    // method used to send emails
	// 
    await _emailService.SendEmail(subject, content, email);
}
```


### Configuration
##### We must enter smtp settings in startup
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
```

