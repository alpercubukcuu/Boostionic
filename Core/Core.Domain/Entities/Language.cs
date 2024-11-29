using Core.Domain.Common;
using System.ComponentModel.DataAnnotations;


namespace Core.Domain.Entities
{
    public class Language : BaseEntity
    {
        [StringLength(100)]
        public string Title { get; set; }

        [StringLength(50)]
        public string CountryCode { get; set; }

        [StringLength(30)]
        public string Culture { get; set; }

        [StringLength(50)]
        public string Flag { get; set; }

        public int Sort { get; set; }
        public int? IsRoot { get; set; }
        public int? Currency { get; set; }

        public Guid OwnerId { get; set; }
        public OwnersEntity Owner { get; set; }
    }
}
