using AutoMapper;
using Core.Application.Dtos;
using Core.Domain.Entities;


namespace Core.Application.Mappings
{
    public class BusinessPlaceProfile : Profile
    {
        public BusinessPlaceProfile()
        {
            CreateMap<BusinessPlace, BusinessPlaceDto>().ReverseMap();
            CreateMap<BusinessPlaceDto, BusinessPlace>().ReverseMap();
        }
       
    }
}
