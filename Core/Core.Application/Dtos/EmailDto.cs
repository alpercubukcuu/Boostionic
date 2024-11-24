using Core.Application.Dtos.CommonDtos;


namespace Core.Application.Dtos
{
    public class EmailDto : BaseDto
    {
        public string HtmlBody { get; set; }
        public string Displayname { get; set; }
        public string Subject { get; set; }
        public string SmtpServer { get; set; }
        public string From { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int SmtpPort { get; set; }
        public int EmailType { get; set; }
        public bool EnableSsl { get; set; }
    }
}
