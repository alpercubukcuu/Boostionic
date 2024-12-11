using Core.Application.Dtos;
using Core.Application.Interfaces.Dtos;
using MediatR;

namespace Core.Application.Features.Commands.ProjectCommands.Commands;

public class AddProjectCommand : ProjectDto, IRequest<IResultDataDto<ProjectDto>>
{
}