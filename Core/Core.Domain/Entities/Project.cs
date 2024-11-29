using Core.Domain.Common;

namespace Core.Domain.Entities;

public class Project : BaseEntity
{
    public string ProjectName { get; set; }
    public DateTime DeadlineDateTime { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public byte ProjectStatus { get; set; } 
    public string Description { get; set; }

    public Guid ClientId { get; set; }
    public Client Client { get; set; }
    
    public ICollection<ProjectStage> ProjectStages { get; set; }
    public ICollection<ProjectTask> ProjectTasks { get; set; }
    public ICollection<ProjectCategory> ProjectCategories { get; set; }

    public Guid OwnerId { get; set; }
    public OwnersEntity Owner { get; set; }
}