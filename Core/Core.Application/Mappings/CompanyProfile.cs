using AutoMapper;
using Core.Application.Dtos;
using Core.Domain.Entities;


namespace Core.Application.Mappings
{
    public class CompanyProfile : Profile
    {
        public CompanyProfile()
        {
            CreateMap<Client, CompanyDto>().ReverseMap();
            CreateMap<CompanyDto, Client>().ReverseMap();
        }
    }
}
