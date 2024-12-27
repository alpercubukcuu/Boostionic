using AutoMapper;
using Core.Application.Dtos;
using Core.Application.Dtos.LoginDtos;
using Core.Application.Features.Commands.UserCommands.Commands;
using Core.Domain.Entities;


namespace Core.Application.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<UserDto, User>().ReverseMap();

            CreateMap<UserDto, UpdateUserCommand>().ReverseMap();
            CreateMap<UpdateUserCommand, UserDto>().ReverseMap();

            CreateMap<UserDto, AddUserCommand>().ReverseMap();
            CreateMap<AddUserCommand, UserDto>().ReverseMap();

            CreateMap<LoginDto, UserLoginCommand>().ReverseMap();
            CreateMap<UserLoginCommand, LoginDto>().ReverseMap();

            CreateMap<AddUserCommand, RegisterDto>().ReverseMap();
            CreateMap<RegisterDto, AddUserCommand>().ReverseMap();


            CreateMap<RegisterUserCommand, RegisterDto>().ReverseMap();
            CreateMap<RegisterDto, RegisterUserCommand>().ReverseMap();

        }
    }
}
