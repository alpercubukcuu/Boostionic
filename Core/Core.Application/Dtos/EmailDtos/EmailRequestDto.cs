namespace Core.Application.Dtos.EmailDtos
{
    public class EmailRequestDto
    {
        public EmailDto EmailFormat { get; set; }
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
