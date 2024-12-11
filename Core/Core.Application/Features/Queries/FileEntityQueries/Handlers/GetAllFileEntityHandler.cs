using AutoMapper;
using Core.Application.Dtos;
using Core.Application.Dtos.ResultDtos;
using Core.Application.Features.Queries.FileEntityQueries.Queries;
using Core.Application.Interfaces.Dtos;
using Core.Application.Interfaces.Repositories;
using MediatR;

namespace Core.Application.Features.Queries.FileEntityQueries.Handlers;

public class GetAllFileEntityHandler : IRequestHandler<GetAllFileEntityQuery, IResultDataDto<List<FileEntityDto>>>
{
    private readonly IMapper _mapper;
    private readonly IFileEntityRepository _fileEntityRepository;

    public GetAllFileEntityHandler(IFileEntityRepository fileEntityRepository, IMapper mapper)
    {
        _fileEntityRepository = fileEntityRepository;
        _mapper = mapper;
    }

    public async Task<IResultDataDto<List<FileEntityDto>>> Handle(GetAllFileEntityQuery request,
        CancellationToken cancellationToken)
    {
        IResultDataDto<List<FileEntityDto>> resultDataDto = new ResultDataDto<List<FileEntityDto>>();

        try
        {
            var repositoryResult = _fileEntityRepository.GetAll(predicate: d => d.IsEnable == true);

            if (repositoryResult.Count() <= 0)
                resultDataDto.SetStatus(false).SetErrorMessage("Not Found Files").SetMessage("The list is empty!");

            var mappedResult = _mapper.Map<List<FileEntityDto>>(repositoryResult);
            resultDataDto.SetData(mappedResult);

            return resultDataDto;
        }
        catch (Exception exception)
        {
            return resultDataDto.SetStatus(false).SetErrorMessage(exception.Message);
        }
    }
}