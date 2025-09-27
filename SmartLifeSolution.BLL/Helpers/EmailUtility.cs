using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SmartLifeSolution.BLL.Helpers
{
    public class EmailMessage
    {
        public EmailAddress From { get; set; }
        public List<EmailAddress> To { get; set; }
        public string Subject { get; set; }
        public string Text { get; set; }
        public string Category { get; set; }
    }

    public class EmailAddress
    {
        public string Email { get; set; }
        public string Name { get; set; }
    }

    public class EmailUtility
    {
        public async Task SendEmailAsync(string subject, string toEmail, string strMsg)
        {
            try
            {
                string fromEmail = "rizwanmz1000@gmail.com";
                string password = "lsotkbmhggzxjujv"; // Use App Password if 2FA is enabled
 
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(fromEmail);
                mail.To.Add(toEmail);
                mail.Subject = subject;
                mail.Body = strMsg;
                mail.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new NetworkCredential(fromEmail, password);
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(mail);
            }
            catch(Exception ex)
            {

            }

        }
    }
}
