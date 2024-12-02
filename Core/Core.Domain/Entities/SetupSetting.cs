using Core.Domain.Common;


namespace Core.Domain.Entities
{
    public class SetupSetting : BaseEntity
    {
        public string SettingData { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
