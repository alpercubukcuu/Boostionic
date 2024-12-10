using Core.Application.Dtos;
using Core.Application.Interfaces.Dtos;
using MediatR;

namespace Core.Application.Features.Commands.IndustryCommands.Commands;

public class AddIndustryCommand : IndustryDto, IRequest<IResultDataDto<IndustryDto>>
{
}