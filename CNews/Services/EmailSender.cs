using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace CNews.Services
{
    public class EmailSender : IEmailSender
    {
        public EmailSender(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            //return;

            try
            {
                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(Configuration["Admin:Email"], Configuration["Admin:Names"])
                };
                mail.To.Add(new MailAddress(toEmail));
                //mail.CC.Add(new MailAddress(_emailSettings.CcEmail));

                mail.Subject = subject;
                mail.Body = message;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                using (SmtpClient smtp = new SmtpClient(Configuration["Smtp:Server"], int.Parse(Configuration["Smtp:Port"])))
                {
                    smtp.EnableSsl = true;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(Configuration["Smtp:Username"], Configuration["Smtp:Password"]);
                    await smtp.SendMailAsync(mail);
                    return;
                }


            }
            catch (Exception)
            {
            }
        }

        public Task Execute(string toEmail, string subject, string message)
        {
            Execute(subject, message, toEmail).Wait();
            return Task.FromResult(0);
        }

    }
}
