using Core.Application.Dtos;
using Core.Application.Interfaces.Dtos;
using MediatR;

namespace Core.Application.Features.Commands.ProjectCategoryCommands.Commands;

public class AddProjectCategoryCommand : ProjectCategoryDto, IRequest<IResultDataDto<ProjectCategoryDto>>
{
}