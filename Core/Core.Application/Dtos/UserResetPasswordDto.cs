using Core.Application.Dtos.CommonDtos;


namespace Core.Application.Dtos
{
    public class UserResetPasswordDto : BaseDto
    {
        public Guid UserId { get; set; }
        public DateTime ExpireDate { get; set; }
        public string Ip { get; set; }
        public string ResetCode { get; set; }
        public UserDto User { get; set; }
    }
}
