using Core.Application.Dtos;
using Core.Application.Interfaces.Dtos;
using MediatR;

namespace Core.Application.Features.Commands.FileEntityCommands.Commands;

public class UpdateFileEntityCommand : FileEntityDto, IRequest<IResultDataDto<FileEntityDto>>
{
}