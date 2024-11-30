using Core.Domain.Common;

namespace Core.Domain.Entities;

public class ProjectTask : BaseEntity
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
    public Project Project { get; set; }

   

}