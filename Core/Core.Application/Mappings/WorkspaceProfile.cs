using AutoMapper;
using Core.Application.Dtos;

namespace Core.Application.Mappings;

public class WorkspaceProfile : Profile
{
    public WorkspaceProfile()
    {
        CreateMap<WorkspaceDto, WorkspaceDto>();
        CreateMap<WorkspaceDto, WorkspaceDto>();
    }
}