using Core.Application.Dtos;
using Core.Application.Interfaces.Dtos;
using MediatR;

namespace Core.Application.Features.Commands.ProjectTaskCommands.Commands;

public class AddProjectTaskCommand : ProjectTaskDto, IRequest<IResultDataDto<ProjectTaskDto>>
{
}