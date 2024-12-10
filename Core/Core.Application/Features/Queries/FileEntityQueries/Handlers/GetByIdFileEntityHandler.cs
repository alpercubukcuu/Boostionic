using AutoMapper;
using Core.Application.Dtos;
using Core.Application.Dtos.ResultDtos;
using Core.Application.Features.Queries.FileEntityQueries.Queries;
using Core.Application.Interfaces.Dtos;
using Core.Application.Interfaces.Repositories;
using MediatR;

namespace Core.Application.Features.Queries.FileEntityQueries.Handlers;

public class GetByIdFileEntityHandler : IRequestHandler<GetByIdFileEntityQuery, IResultDataDto<FileEntityDto>>
{
    private readonly IMapper _mapper;
    private readonly IFileEntityRepository _fileEntityRepository;

    public GetByIdFileEntityHandler(IMapper mapper, IFileEntityRepository fileEntityRepository)
    {
        _mapper = mapper;
        _fileEntityRepository = fileEntityRepository;
    }

    public async Task<IResultDataDto<FileEntityDto>> Handle(GetByIdFileEntityQuery request,
        CancellationToken cancellationToken)
    {
        IResultDataDto<FileEntityDto> resultDataDto = new ResultDataDto<FileEntityDto>();

        try
        {
            var repositoryResult =
                _fileEntityRepository.GetSingle(predicate: x => x.IsEnable == true && x.Id == request.Id);

            if (repositoryResult == null)
                resultDataDto.SetStatus(false).SetErrorMessage("Not Found Data")
                    .SetMessage("No data found for the ID value");

            var mappedEntity = _mapper.Map<FileEntityDto>(repositoryResult);
            resultDataDto.SetData(mappedEntity);
            return resultDataDto;
        }
        catch (Exception exception)
        {
            return resultDataDto.SetStatus(false).SetErrorMessage(exception.Message);
        }
    }
}