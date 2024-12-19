using AutoMapper;
using Core.Application.Dtos;
using Core.Application.Dtos.ResultDtos;
using Core.Application.Features.Queries.RelationUserToWorkspaceQueries.Queries;
using Core.Application.Interfaces.Dtos;
using Core.Application.Interfaces.Repositories;
using MediatR;

namespace Core.Application.Features.Queries.RelationUserToWorkspaceQueries.Handlers;

public class
    GetAllRelationUserToWorkspaceHandler : IRequestHandler<GetAllRelationUserToWorkspaceQuery, IResultDataDto<List<RelationUserToWorkspaceDto>>>
{
    private readonly IMapper _mapper;
    private readonly IRelationUserToWorkspaceRepository _relationUserToWorkspaceRepository;

    public GetAllRelationUserToWorkspaceHandler(IMapper mapper, IRelationUserToWorkspaceRepository relationUserToWorkspaceRepository)
    {
        _mapper = mapper;
        _relationUserToWorkspaceRepository = relationUserToWorkspaceRepository;
    }

    public async Task<IResultDataDto<List<RelationUserToWorkspaceDto>>> Handle(GetAllRelationUserToWorkspaceQuery request,
        CancellationToken cancellationToken)
    {
        IResultDataDto<List<RelationUserToWorkspaceDto>> result = new ResultDataDto<List<RelationUserToWorkspaceDto>>();
        try
        {
            var repoResult = _relationUserToWorkspaceRepository.GetAll(predicate: d => d.IsEnable == true);
            if (repoResult.Count() <= 0)
                result.SetStatus(false).SetErrorMessage("Not Found Data").SetMessage("The list is empty!");
            var map = _mapper.Map<List<RelationUserToWorkspaceDto>>(repoResult);
            result.SetData(map);
            return result;
        }
        catch (Exception exception)
        {
            return result.SetStatus(false).SetErrorMessage(exception.Message);
        }
    }
}