using AutoMapper;
using Core.Application.Dtos;
using Core.Domain.Entities;


namespace Core.Application.Mappings
{
    public class IndustryProfile : Profile
    {
        public IndustryProfile()
        {
            CreateMap<Industry, IndustryDto>().ReverseMap();
            CreateMap<IndustryDto, Industry>().ReverseMap();
        }
    }
}
