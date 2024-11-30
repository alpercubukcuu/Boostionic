using Core.Application.Dtos.CommonDtos;


namespace Core.Application.Dtos
{
    public class ProjectTaskDto : BaseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? DeadlineDateTime { get; set; }
        public byte Status { get; set; }
        public int Progress { get; set; }

        public byte Priority { get; set; }
        public bool IsBlocked { get; set; }
        public string BlockReason { get; set; }

        public TimeSpan EstimatedDuration { get; set; }
        public TimeSpan? ActualDuration { get; set; }

        public Guid ProjectId { get; set; }
        public ProjectDto Project { get; set; }
    }
}
