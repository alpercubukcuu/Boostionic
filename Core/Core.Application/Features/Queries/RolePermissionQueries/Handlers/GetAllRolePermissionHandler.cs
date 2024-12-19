using AutoMapper;
using Core.Application.Dtos;
using Core.Application.Dtos.ResultDtos;
using Core.Application.Features.Queries.RolePermissionQueries.Queries;
using Core.Application.Interfaces.Dtos;
using Core.Application.Interfaces.Repositories;
using MediatR;

namespace Core.Application.Features.Queries.RolePermissionQueries.Handlers;

public class
    GetAllRolePermissionHandler : IRequestHandler<GetAllRolePermissionQuery, IResultDataDto<List<RolePermissionDto>>>
{
    private readonly IMapper _mapper;
    private readonly IRolePermissionRepository _rolePermissionRepository;

    public GetAllRolePermissionHandler(IMapper mapper, IRolePermissionRepository rolePermissionRepository)
    {
        _mapper = mapper;
        _rolePermissionRepository = rolePermissionRepository;
    }

    public async Task<IResultDataDto<List<RolePermissionDto>>> Handle(GetAllRolePermissionQuery request,
        CancellationToken cancellationToken)
    {
        IResultDataDto<List<RolePermissionDto>> result = new ResultDataDto<List<RolePermissionDto>>();
        try
        {
            var repoResult = _rolePermissionRepository.GetAll(predicate: d => d.IsEnable == true);
            if (repoResult.Count() <= 0)
                result.SetStatus(false).SetErrorMessage("Not Found Data").SetMessage("The list is empty!");
            var map = _mapper.Map<List<RolePermissionDto>>(repoResult);
            result.SetData(map);
            return result;
        }
        catch (Exception exception)
        {
            return result.SetStatus(false).SetErrorMessage(exception.Message);
        }
    }
}