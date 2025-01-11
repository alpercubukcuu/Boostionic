using Core.Application.Dtos.CommonDtos;
using Core.Domain.Entities;


namespace Core.Application.Dtos
{
    public class WorkspaceDto : BaseDto
    {
        public string Name { get; set; }
        public Guid? OwnerId { get; set; }
        public bool IWorkflows { get; set; } = true;
        public bool IsMedia { get; set; } = false;
        public bool IsGrowth { get; set; } = false;
        public OwnerEntityDto Owner { get; set; }
        public ICollection<ProjectDto> Projects { get; set; }
    }
}
