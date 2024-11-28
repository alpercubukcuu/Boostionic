using Core.Domain.Common;

namespace Core.Domain.Entities;

public class ProjectStage : OwnersEntity
{
    public string Name { get; set; }
    public Guid ProjectId { get; set; }
    public DateTime StartDate { get; set; } 
    public DateTime? EndDate { get; set; } 
    public DateTime DeadlineDateTime { get; set; }
    public byte Status { get; set; } // Aşamanın durumu (e.g., "In Progress", "Completed")
    public string Description { get; set; } // Aşamayla ilgili açıklamalar veya notlar
    
    public Project Project { get; set; }
    public ICollection<ProjectTask> ProjectTasks { get; set; }
    
    // Durum Kontrolü için Ek Alanlar
    public bool IsCritical { get; set; } // Kritik bir aşama olup olmadığını belirtir
    public int Order { get; set; } // Aşamanın sırası (örneğin, 1, 2, 3)

    // İlerleme Takibi
    public int Progress { get; set; } // Aşamanın tamamlanma yüzdesi (0-100)
}