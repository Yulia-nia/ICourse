using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ICourses.Services
{
    public class EmailService
    {
        public async Task SendEmailAsync(string reciever, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Администрация сайта", "mwrld5946@gmail.com"));
            emailMessage.To.Add(new MailboxAddress("", reciever));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 587, false);
                await client.AuthenticateAsync("mwrld5946@gmail.com", "love18Barcelona");
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
       
    }
}
