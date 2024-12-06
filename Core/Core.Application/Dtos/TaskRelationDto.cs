using Core.Application.Dtos.CommonDtos;

namespace Core.Application.Dtos;

public class TaskRelationDto : BaseDto
{
    public byte TaskType { get; set; }
    public Guid AssignedUserId { get; set; }
    public UserDto AssignedUser { get; set; }
    public Guid ProjectTaskId { get; set; }
    public ProjectDto ProjectTask { get; set; }
}