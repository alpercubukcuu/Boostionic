using AutoMapper;
using Core.Application.Dtos;
using Core.Domain.Entities;

namespace Core.Application.Mappings;

public class TaskRelationProfile : Profile
{
    public TaskRelationProfile()
    {
        CreateMap<TaskRelation, TaskRelationDto>();
        CreateMap<TaskRelationDto, TaskRelation>();
    }
}