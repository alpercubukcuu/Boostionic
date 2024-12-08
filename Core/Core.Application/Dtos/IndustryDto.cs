using Core.Application.Dtos.CommonDtos;



namespace Core.Application.Dtos
{
    public class IndustryDto : BaseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Sector { get; set; }
        public int? ParentIndustryId { get; set; }
        public List<ClientDto> Client { get; set; }

        public IndustryDto()
        {
            Client = new List<ClientDto>();
        }
    }
}
