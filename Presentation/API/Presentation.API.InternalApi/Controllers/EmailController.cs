using Core.Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;

namespace Presentation.API.InternalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        
        [HttpPost]
        public void SendMail(EmailDto emailFormat, string toEmail, string subject, string body) 
        {
            var smtpClient = new SmtpClient(emailFormat.SmtpServer)
            {
                Port = emailFormat.SmtpPort,
                Credentials = new NetworkCredential(emailFormat.UserName, emailFormat.Password),
                EnableSsl = emailFormat.EnableSsl 
            };
           
            var mailMessage = new MailMessage
            {
                From = new MailAddress(emailFormat.From, emailFormat.Displayname),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            mailMessage.To.Add(toEmail);
            
            smtpClient.Send(mailMessage);
        }
    }
}
