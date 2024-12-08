using AutoMapper;
using Core.Application.Dtos;
using Core.Domain.Entities;

namespace Core.Application.Mappings;

public class ProjectStageProfile : Profile
{
    public ProjectStageProfile()
    {
        CreateMap<ProjectStageDto, ProjectStage>();
        CreateMap<ProjectStage, ProjectStageDto>();
    }
}