using Core.Application.Dtos.CommonDtos;
using Core.Domain.Entities;


namespace Core.Application.Dtos
{
    public class WorkspaceDto : BaseDto
    {
        public string Name { get; set; }
        public Guid OwnerId { get; set; }
        public OwnerEntityDto Owner { get; set; }
        public ICollection<ProjectDto> Projects { get; set; }
    }
}
