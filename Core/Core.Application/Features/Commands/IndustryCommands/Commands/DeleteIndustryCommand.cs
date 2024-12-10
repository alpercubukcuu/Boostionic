using Core.Application.Dtos;
using Core.Application.Interfaces.Dtos;
using MediatR;

namespace Core.Application.Features.Commands.IndustryCommands.Commands;

public class DeleteIndustryCommand : IndustryDto, IRequest<IResultDataDto<bool>>
{
    public Guid Id { get; set; }
}