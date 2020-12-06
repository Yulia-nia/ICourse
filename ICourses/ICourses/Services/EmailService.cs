using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;

namespace ICourses.Services
{
    public class EmailService
    {
        public async Task SendEmailAsync(string to_address, string body, string subject)
        {
            MailMessage message = new MailMessage();
            // message.IsBodyHtml = true;
            message.From = new MailAddress("sylianetttogggg@gmail.com", subject);
            message.To.Add(to_address);
            message.Subject = "Test";
            message.Body = body;
            using (SmtpClient client = new SmtpClient("smtp.gmail.com"))
            {
                client.Credentials = new NetworkCredential("sylianetttogggg@gmail.com", "Surface153");
                client.Port = 587;
                client.EnableSsl = true;
                client.Send(message);
            }
        }

        //public async Task SendEmailAsync(string email, string subject, string message)
        //{
        //    var emailMessage = new MimeMessage();

        //    emailMessage.From.Add(new MailboxAddress("Администрация сайта", "login@yandex.ru"));
        //    emailMessage.To.Add(new MailboxAddress("", email));
        //    emailMessage.Subject = subject;
        //    emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
        //    {
        //        Text = message
        //    };

        //    using (var client = new SmtpClient())
        //    {
        //        client.CheckCertificateRevocation = false;
        //        client.Connect("smtp.gmail.com", 587, true);
        //        await client.AuthenticateAsync("login@yandex.ru", "password");
        //        await client.SendAsync(emailMessage);

        //        await client.DisconnectAsync(true);
        //    }
        //}
    }
}
