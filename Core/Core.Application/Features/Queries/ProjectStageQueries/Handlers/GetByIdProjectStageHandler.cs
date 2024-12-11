using AutoMapper;
using Core.Application.Dtos;
using Core.Application.Dtos.ResultDtos;
using Core.Application.Features.Queries.ProjectStageQueries.Queries;
using Core.Application.Interfaces.Dtos;
using Core.Application.Interfaces.Repositories;
using MediatR;

namespace Core.Application.Features.Queries.ProjectStageQueries.Handlers;

public class
    GetByIdProjectStageHandler : IRequestHandler<GetByIdProjectStageQuery, IResultDataDto<ProjectStageDto>>
{
    private readonly IMapper _mapper;
    private readonly IProjectStageRepository _projectStageRepository;

    public GetByIdProjectStageHandler(IMapper mapper, IProjectStageRepository projectStageRepository)
    {
        _mapper = mapper;
        _projectStageRepository = projectStageRepository;
    }

    public async Task<IResultDataDto<ProjectStageDto>> Handle(GetByIdProjectStageQuery request,
        CancellationToken cancellationToken)
    {
        IResultDataDto<ProjectStageDto> result = new ResultDataDto<ProjectStageDto>();
        try
        {
            var repositoryResult =
                _projectStageRepository.GetSingle(predicate: d => d.IsEnable == true && d.Id == request.Id);
            if (repositoryResult == null)
                result.SetStatus(false).SetErrorMessage("Not Found Data").SetMessage("No data found for the ID value");
            var map = _mapper.Map<ProjectStageDto>(repositoryResult);
            return result;
        }
        catch (Exception exception)
        {
            return result.SetStatus(false).SetErrorMessage(exception.Message);
        }
    }
}