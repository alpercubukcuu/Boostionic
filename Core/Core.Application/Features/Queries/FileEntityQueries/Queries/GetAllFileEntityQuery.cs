using Core.Application.Dtos;
using Core.Application.Interfaces.Dtos;
using MediatR;

namespace Core.Application.Features.Queries.FileEntityQueries.Queries;

public class GetAllFileEntityQuery : IRequest<IResultDataDto<List<FileEntityDto>>>;