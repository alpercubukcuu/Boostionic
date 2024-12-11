using AutoMapper;
using Core.Application.Dtos;
using Core.Application.Dtos.ResultDtos;
using Core.Application.Features.Queries.ProjectRelationQueries.Queries;
using Core.Application.Interfaces.Dtos;
using Core.Application.Interfaces.Repositories;
using MediatR;

namespace Core.Application.Features.Queries.ProjectRelationQueries.Handlers;

public class
    GetAllProjectRelationRelationHandler : IRequestHandler<GetAllProjectRelationQuery, IResultDataDto<List<ProjectRelationDto>>>
{
    private readonly IMapper _mapper;
    private readonly IProjectRelationRepository _projectRelationRepository;

    public GetAllProjectRelationRelationHandler(IMapper mapper, IProjectRelationRepository ProjectRelationRepository)
    {
        _mapper = mapper;
        _projectRelationRepository = ProjectRelationRepository;
    }

    public async Task<IResultDataDto<List<ProjectRelationDto>>> Handle(GetAllProjectRelationQuery request,
        CancellationToken cancellationToken)
    {
        IResultDataDto<List<ProjectRelationDto>> result = new ResultDataDto<List<ProjectRelationDto>>();
        try
        {
            var repoResult = _projectRelationRepository.GetAll(predicate: d => d.IsEnable == true);
            if (repoResult.Count() <= 0)
                result.SetStatus(false).SetErrorMessage("Not Found Data").SetMessage("The list is empty!");
            var map = _mapper.Map<List<ProjectRelationDto>>(repoResult);
            result.SetData(map);
            return result;
        }
        catch (Exception exception)
        {
            return result.SetStatus(false).SetErrorMessage(exception.Message);
        }
    }
}