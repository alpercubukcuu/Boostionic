using AutoMapper;
using Core.Application.Dtos;
using Core.Domain.Entities;


namespace Core.Application.Mappings
{
    public class UserResetPasswordProfile : Profile
    {
        public UserResetPasswordProfile()
        {
            CreateMap<UserResetPassword, UserResetPasswordDto>().ReverseMap();
            CreateMap<UserResetPasswordDto, UserResetPassword>().ReverseMap();
        }
    }
}
