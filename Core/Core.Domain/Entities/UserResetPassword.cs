using Core.Domain.Common;


namespace Core.Domain.Entities
{
    public class UserResetPassword : OwnersEntity
    {
        public Guid UserId { get; set; }
        public DateTime ExpireDate { get; set; }
        public string Ip { get; set; }
        public string ResetCode { get; set; }
        public User User { get; set; }
    }
}
