using Core.Domain.Common;

namespace Core.Domain.Entities;

public class ProjectTask : OwnersEntity
{
    public string Name { get; set; } // Görev Adı
    public string Description { get; set; } // Görev Açıklaması
    public Guid ProjectStageId { get; set; } // Görevin ait olduğu aşamanın ID'si
    public Guid? AssignedUserId { get; set; } // Görevi atanan kullanıcının ID'si (Opsiyonel)

    public ProjectStage ProjectStage { get; set; } // Görevin ait olduğu aşama
    public User AssignedUser { get; set; } // Görevi üstlenen kullanıcı

    public DateTime StartDate { get; set; } // Görevin başlangıç tarihi
    public DateTime? EndDate { get; set; } // Görevin bitiş tarihi (Opsiyonel)
    public DateTime? DeadlineDateTime { get; set; } // Görevin tamamlanması gereken son tarih (Opsiyonel)
    public byte Status { get; set; } // Görevin durumu (e.g., "Not Started", "In Progress", "Completed")
    public int Progress { get; set; } // Görevin tamamlanma yüzdesi (0-100)

    public byte Priority { get; set; } // Görevin önceliği (örn. 1: Yüksek, 2: Orta, 3: Düşük)
    public bool IsBlocked { get; set; } // Görevin engellenmiş olup olmadığını belirtir
    public string BlockReason { get; set; } // Görev engellendiyse nedenini belirtir (Opsiyonel)

    public TimeSpan EstimatedDuration { get; set; } // Görevin tahmini süresi
    public TimeSpan? ActualDuration { get; set; } // Görevin gerçek süresi (Tamamlandıysa)

}