using Core.Application.Dtos.CommonDtos;
using Core.Domain.Entities;


namespace Core.Application.Dtos
{
    public class BusinessPlaceDto : BaseDto
    {
        public string JsonData { get; set; }
        
        public Guid UserId { get; set; }
        public UserDto User { get; set; }
        
        public Guid OwnerId { get; set; }
        public OwnerEntityDto OwnerEntity { get; set; }
    }
}