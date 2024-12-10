using Core.Application.Dtos;
using Core.Application.Interfaces.Dtos;
using Core.Domain.Entities;
using MediatR;

namespace Core.Application.Features.Commands.ProjectCategoryCommands.Commands;

public class UpdateProjectCategoryCommand : ProjectCategoryDto, IRequest<IResultDataDto<ProjectCategoryDto>>
{
}