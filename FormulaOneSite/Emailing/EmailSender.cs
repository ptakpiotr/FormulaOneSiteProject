using FluentEmail.Core;
using FluentEmail.Razor;
using FluentEmail.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace FormulaOneSite.Emailing
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _config;

        public EmailSender(IConfiguration config)
        {
            _config = config;

            var configuration = new SmtpSender(new SmtpClient("smtp.ethereal.email") { 
                EnableSsl=true,
                Port=587,
                Credentials=new NetworkCredential(_config.GetSection("Mailing:Email").Value, config.GetSection("Mailing:Password").Value)
            });
            Email.DefaultSender = configuration;
            Email.DefaultRenderer = new RazorRenderer();
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            await Email.From(_config.GetSection("Mailing:Email").Value).To(email).Subject(subject).Body(htmlMessage,true).SendAsync();
        }
    }
}
