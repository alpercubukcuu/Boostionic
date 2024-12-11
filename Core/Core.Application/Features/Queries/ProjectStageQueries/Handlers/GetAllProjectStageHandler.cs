using AutoMapper;
using Core.Application.Dtos;
using Core.Application.Dtos.ResultDtos;
using Core.Application.Features.Queries.ProjectStageQueries.Queries;
using Core.Application.Interfaces.Dtos;
using Core.Application.Interfaces.Repositories;
using MediatR;

namespace Core.Application.Features.Queries.ProjectStageQueries.Handlers;

public class
    GetAllProjectStageHandler : IRequestHandler<GetAllProjectStageQuery, IResultDataDto<List<ProjectStageDto>>>
{
    private readonly IMapper _mapper;
    private readonly IProjectStageRepository _projectStageRepository;

    public GetAllProjectStageHandler(IMapper mapper, IProjectStageRepository projectStageRepository)
    {
        _mapper = mapper;
        _projectStageRepository = projectStageRepository;
    }

    public async Task<IResultDataDto<List<ProjectStageDto>>> Handle(GetAllProjectStageQuery request,
        CancellationToken cancellationToken)
    {
        IResultDataDto<List<ProjectStageDto>> result = new ResultDataDto<List<ProjectStageDto>>();
        try
        {
            var repoResult = _projectStageRepository.GetAll(predicate: d => d.IsEnable == true);
            if (repoResult.Count() <= 0)
                result.SetStatus(false).SetErrorMessage("Not Found Data").SetMessage("The list is empty!");
            var map = _mapper.Map<List<ProjectStageDto>>(repoResult);
            result.SetData(map);
            return result;
        }
        catch (Exception exception)
        {
            return result.SetStatus(false).SetErrorMessage(exception.Message);
        }
    }
}