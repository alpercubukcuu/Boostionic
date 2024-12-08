using Core.Domain.Common;


namespace Core.Domain.Entities
{
    public class Ticket : BaseEntity
    {
        public string  Title { get; set; }
        public string  Subject { get; set; }
        public byte  Priority  { get; set; }
        public string Message { get; set; }
        
        public Guid UserId { get; set; }
        public User User { get; set; }

    }
}
