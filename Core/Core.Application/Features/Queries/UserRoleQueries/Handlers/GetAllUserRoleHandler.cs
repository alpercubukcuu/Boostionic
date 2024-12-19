using AutoMapper;
using Core.Application.Dtos;
using Core.Application.Dtos.ResultDtos;
using Core.Application.Features.Queries.UserRoleQueries.Queries;
using Core.Application.Interfaces.Dtos;
using Core.Application.Interfaces.Repositories;
using MediatR;

namespace Core.Application.Features.Queries.UserRoleQueries.Handlers;

public class
    GetAllUserRoleHandler : IRequestHandler<GetAllUserRoleQuery, IResultDataDto<List<UserRoleDto>>>
{
    private readonly IMapper _mapper;
    private readonly IUserRoleRepository _userRoleRepository;

    public GetAllUserRoleHandler(IMapper mapper, IUserRoleRepository userRoleRepository)
    {
        _mapper = mapper;
        _userRoleRepository = userRoleRepository;
    }

    public async Task<IResultDataDto<List<UserRoleDto>>> Handle(GetAllUserRoleQuery request,
        CancellationToken cancellationToken)
    {
        IResultDataDto<List<UserRoleDto>> result = new ResultDataDto<List<UserRoleDto>>();
        try
        {
            var repoResult = _userRoleRepository.GetAll(predicate: d => d.IsEnable == true);
            if (repoResult.Count() <= 0)
                result.SetStatus(false).SetErrorMessage("Not Found Data").SetMessage("The list is empty!");
            var map = _mapper.Map<List<UserRoleDto>>(repoResult);
            result.SetData(map);
            return result;
        }
        catch (Exception exception)
        {
            return result.SetStatus(false).SetErrorMessage(exception.Message);
        }
    }
}