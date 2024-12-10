using Core.Application.Dtos;
using Core.Application.Interfaces.Dtos;
using MediatR;

namespace Core.Application.Features.Queries.FileEntityQueries.Queries;

public class GetByIdFileEntityQuery : IRequest<IResultDataDto<FileEntityDto>>
{
    public Guid Id { get; set; }
}