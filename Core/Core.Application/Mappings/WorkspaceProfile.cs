using AutoMapper;
using Core.Application.Dtos;
using Core.Application.Features.Commands.WorkspaceCommands.Commands;
using Core.Domain.Entities;

namespace Core.Application.Mappings;

public class WorkspaceProfile : Profile
{
    public WorkspaceProfile()
    {
        CreateMap<Workspace, WorkspaceDto>();
        CreateMap<WorkspaceDto, Workspace>();


        CreateMap<AddWorkspaceCommand, WorkspaceDto>();
        CreateMap<WorkspaceDto, AddWorkspaceCommand>();
    }
}