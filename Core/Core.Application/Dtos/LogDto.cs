using Core.Application.Dtos.CommonDtos;
using System.ComponentModel.DataAnnotations.Schema;


namespace Core.Application.Dtos
{
    public partial class LogDto : BaseDto
    {
        public string Title { get; set; }
        public byte TransectionType { get; set; }
        public string Description { get; set; }
        public Guid DataId { get; set; }
        public string Entity { get; set; }
    }
    public partial class LogDto
    {
        public Guid UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public UserDto User { get; set; }
    }
}
