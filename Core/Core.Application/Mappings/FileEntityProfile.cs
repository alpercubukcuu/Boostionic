using AutoMapper;
using Core.Application.Dtos;
using Core.Domain.Entities;

namespace Core.Application.Mappings;

public class FileEntityProfile : Profile
{
    public FileEntityProfile()
    {
        CreateMap<FileEntity, FileEntityDto>().ReverseMap();
        CreateMap<FileEntityDto, FileEntity>().ReverseMap();
    }
}