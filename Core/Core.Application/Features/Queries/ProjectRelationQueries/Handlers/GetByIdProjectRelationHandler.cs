using AutoMapper;
using Core.Application.Dtos;
using Core.Application.Dtos.ResultDtos;
using Core.Application.Features.Queries.ProjectRelationQueries.Queries;
using Core.Application.Interfaces.Dtos;
using Core.Application.Interfaces.Repositories;
using MediatR;

namespace Core.Application.Features.Queries.ProjectRelationQueries.Handlers;

public class
    GetByIdProjectRelationRelationHandler : IRequestHandler<GetByIdProjectRelationQuery, IResultDataDto<ProjectRelationDto>>
{
    private readonly IMapper _mapper;
    private readonly IProjectRelationRepository _projectRelationRepository;

    public GetByIdProjectRelationRelationHandler(IMapper mapper, IProjectRelationRepository ProjectRelationRepository)
    {
        _mapper = mapper;
        _projectRelationRepository = ProjectRelationRepository;
    }

    public async Task<IResultDataDto<ProjectRelationDto>> Handle(GetByIdProjectRelationQuery request,
        CancellationToken cancellationToken)
    {
        IResultDataDto<ProjectRelationDto> result = new ResultDataDto<ProjectRelationDto>();
        try
        {
            var repositoryResult =
                _projectRelationRepository.GetSingle(predicate: d => d.IsEnable == true && d.Id == request.Id);
            if (repositoryResult == null)
                result.SetStatus(false).SetErrorMessage("Not Found Data").SetMessage("No data found for the ID value");
            var map = _mapper.Map<ProjectRelationDto>(repositoryResult);
            return result;
        }
        catch (Exception exception)
        {
            return result.SetStatus(false).SetErrorMessage(exception.Message);
        }
    }
}