using AutoMapper;
using Core.Application.Dtos;
using Core.Domain.Entities;

namespace Core.Application.Mappings;

public class ProjectRelationProfile : Profile
{
    public ProjectRelationProfile()
    {
        CreateMap<ProjectRelation, ProjectRelationDto>();
        CreateMap<ProjectRelationDto, ProjectRelation>();
    }
}