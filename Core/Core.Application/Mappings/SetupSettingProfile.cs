using AutoMapper;
using Core.Application.Dtos;
using Core.Domain.Entities;


namespace Core.Application.Mappings
{
    public class SetupSettingProfile : Profile
    {
        public SetupSettingProfile()
        {
            CreateMap<SetupSetting, SetupSettingDto>().ReverseMap();
            CreateMap<SetupSettingDto, SetupSetting>().ReverseMap();
        }
    }
}
