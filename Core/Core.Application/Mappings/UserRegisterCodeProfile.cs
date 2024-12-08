using AutoMapper;
using Core.Application.Dtos;
using Core.Domain.Entities;


namespace Core.Application.Mappings
{
    public class UserRegisterCodeProfile : Profile
    {
        public UserRegisterCodeProfile()
        {
            CreateMap<UserRegisterCode, UserRegisterCodeDto>().ReverseMap();
            CreateMap<UserRegisterCodeDto, UserRegisterCode>().ReverseMap();
        }
    }
}
