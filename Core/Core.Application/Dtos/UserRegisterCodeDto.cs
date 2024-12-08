using Core.Application.Dtos.CommonDtos;


namespace Core.Application.Dtos
{
    public class UserRegisterCodeDto : BaseDto
    {
        public Guid UserId { get; set; }
        public DateTime ExpireDate { get; set; }
        public string Ip { get; set; }
        public string RegisterCode { get; set; }
        public UserDto User { get; set; }
    }
}
