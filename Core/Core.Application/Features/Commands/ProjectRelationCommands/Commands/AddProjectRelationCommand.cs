using Core.Application.Dtos;
using Core.Application.Interfaces.Dtos;
using MediatR;

namespace Core.Application.Features.Commands.ProjectRelationCommands.Commands;

public class AddProjectRelationCommand : ProjectRelationDto, IRequest<IResultDataDto<ProjectRelationDto>>
{
}