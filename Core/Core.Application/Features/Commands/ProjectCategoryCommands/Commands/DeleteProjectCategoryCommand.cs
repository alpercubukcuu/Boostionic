using Core.Application.Dtos;
using Core.Application.Interfaces.Dtos;
using MediatR;

namespace Core.Application.Features.Commands.ProjectCategoryCommands.Commands;

public class DeleteProjectCategoryCommand : ProjectCategoryDto, IRequest<IResultDataDto<bool>>
{
    public Guid Id { get; set; }
}