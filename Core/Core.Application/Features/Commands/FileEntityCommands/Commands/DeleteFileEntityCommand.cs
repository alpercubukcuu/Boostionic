using Core.Application.Dtos;
using Core.Application.Interfaces.Dtos;
using MediatR;

namespace Core.Application.Features.Commands.FileEntityCommands.Commands;

public class DeleteFileEntityCommand : FileEntityDto, IRequest<IResultDataDto<bool>>
{
    public Guid Id { get; set; }
}