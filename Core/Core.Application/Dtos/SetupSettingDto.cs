using Core.Application.Dtos.CommonDtos;


namespace Core.Application.Dtos
{
    public class SetupSettingDto : BaseDto
    {
        public string SettingData { get; set; }
        public Guid UserId { get; set; }
        public UserDto User { get; set; }
    }
}
