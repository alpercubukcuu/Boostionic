using Core.Application.Dtos.CommonDtos;

namespace Core.Application.Dtos;

public class ProjectRelationDto : BaseDto
{
    public string ProjectType { get; set; }
    public Guid AssignedUserId { get; set; }
    public UserDto AssignedUser { get; set; }
    public Guid ProjectId { get; set; }
    public ProjectDto Project { get; set; }
}