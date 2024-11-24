using Core.Domain.Common;


namespace Core.Domain.Entities
{
    public class Industry : BaseEntity
    {
        public string Name { get; set; } 
        public string Description { get; set; }         
        public string Sector { get; set; } 
        public int? ParentIndustryId { get; set; }        
        public List<Company> Companies { get; set; }

        public Industry()
        {
            Companies = new List<Company>();
        }
    }
}
