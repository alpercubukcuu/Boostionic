using Core.Application.Dtos;
using Core.Application.Interfaces.Dtos;
using MediatR;

namespace Core.Application.Features.Commands.ProjectStageCommands.Commands;

public class DeleteProjectStageCommand: ProjectStageDto, IRequest<IResultDataDto<bool>>
{
    public Guid Id { get; set; }
}