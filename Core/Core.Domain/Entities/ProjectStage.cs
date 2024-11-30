using Core.Domain.Common;

namespace Core.Domain.Entities;

public class ProjectStage : BaseEntity
{
    public string Name { get; set; }
    public Guid ProjectId { get; set; }
    public DateTime StartDate { get; set; } 
    public DateTime? EndDate { get; set; } 
    public DateTime DeadlineDateTime { get; set; }
    public byte Status { get; set; } 
    public string Description { get; set; }    
    public Project Project { get; set; }
    public ICollection<ProjectTask> ProjectTasks { get; set; }        
    public bool IsCritical { get; set; } 
    public int Order { get; set; } 
    public int Progress { get; set; }
 
}