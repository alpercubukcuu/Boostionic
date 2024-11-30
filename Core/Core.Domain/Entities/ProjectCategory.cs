using Core.Domain.Common;

namespace Core.Domain.Entities;

public class ProjectCategory : BaseEntity
{
    public string Name { get; set; } 
    public string Description { get; set; } 

    public ICollection<Project> Projects { get; set; } 

    public int DisplayOrder { get; set; }

 
}