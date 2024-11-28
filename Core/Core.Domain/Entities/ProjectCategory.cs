using Core.Domain.Common;

namespace Core.Domain.Entities;

public class ProjectCategory : OwnersEntity
{
    public string Name { get; set; } // Kategori Adı (örneğin, "Eğitim", "Sağlık", "Teknoloji")
    public string Description { get; set; } // Kategori Açıklaması

    public ICollection<Project> Projects { get; set; } // Bu kategoriye ait projeler

    public int DisplayOrder { get; set; } // Kategorinin sıralama düzeni
}