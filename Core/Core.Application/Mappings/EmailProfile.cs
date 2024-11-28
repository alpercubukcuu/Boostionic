using AutoMapper;
using Core.Application.Dtos.EmailDtos;
using Core.Domain.Entities;


namespace Core.Application.Mappings
{
    public class EmailProfile : Profile
    {
        public EmailProfile()
        {
            CreateMap<Email, EmailDto>().ReverseMap();
            CreateMap<EmailDto, Email>().ReverseMap();
        }
    }
}
