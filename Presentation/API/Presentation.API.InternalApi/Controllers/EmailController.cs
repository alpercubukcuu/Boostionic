using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using Core.Application.Dtos.EmailDtos;

namespace Presentation.API.InternalApi.Controllers
{
    [Route("api/internal/email")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        public EmailController()
        {
            
        }

        [HttpPost("send")]
        public void SendMail([FromBody] EmailRequestDto emailRequest)
        {
            var smtpClient = new SmtpClient(emailRequest.EmailFormat.SmtpServer)
            {
                Port = emailRequest.EmailFormat.SmtpPort,
                Credentials = new NetworkCredential(emailRequest.EmailFormat.UserName, emailRequest.EmailFormat.Password),
                EnableSsl = emailRequest.EmailFormat.EnableSsl
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(emailRequest.EmailFormat.From, emailRequest.EmailFormat.Displayname),
                Subject = emailRequest.Subject,
                Body = emailRequest.Body,
                IsBodyHtml = true
            };

            mailMessage.To.Add(emailRequest.ToEmail);

            smtpClient.Send(mailMessage);
        }
    }
}
