using Core.Domain.Common;


namespace Core.Domain.Entities
{
    public class UserRegisterCode : BaseEntity
    {
        public Guid UserId { get; set; }
        public DateTime ExpireDate { get; set; }
        public string Ip { get; set; }
        public string RegisterCode { get; set; }
        public User User { get; set; }
    }
}
