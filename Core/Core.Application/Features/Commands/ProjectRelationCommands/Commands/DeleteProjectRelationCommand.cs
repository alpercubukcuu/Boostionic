using Core.Application.Dtos;
using Core.Application.Interfaces.Dtos;
using MediatR;

namespace Core.Application.Features.Commands.ProjectRelationCommands.Commands;

public class DeleteProjectRelationCommand: ProjectRelationDto, IRequest<IResultDataDto<bool>>
{
    public Guid Id { get; set; }
}