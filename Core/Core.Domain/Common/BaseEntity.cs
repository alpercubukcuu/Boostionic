using Core.Domain.Interface.Entity;
using System.ComponentModel.DataAnnotations;


namespace Core.Domain.Common
{
    public class BaseEntity : IBaseEntity
    {
        [Required]
        [Key]
        public Guid Id { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; } 

        [Required]
        public DateTime UpdatedDate { get; set; }

        [Required]
        public bool IsEnable { get; set; } = true;
    }
}
