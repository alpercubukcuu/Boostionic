using Core.Application.Dtos.CommonDtos;

namespace Core.Application.Dtos;

public class TimeTrackingDto : BaseDto
{
    public Guid UserId { get; set; }
    public UserDto User { get; set; }
    public Guid ProjectTaskId { get; set; }
    public ProjectDto ProjectTask { get; set; }
    public int Duration { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}