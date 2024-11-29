using Core.Domain.Common;


namespace Core.Domain.Entities
{
    public class BusinessPlace : BaseEntity
    {
        public string JsonData { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid OwnerId { get; set; }
        public OwnersEntity Owner { get; set; }

    }
}
