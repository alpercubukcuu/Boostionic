using AutoMapper;
using Core.Application.Dtos;
using Core.Application.Dtos.ResultDtos;
using Core.Application.Enums;
using Core.Application.Features.Commands.RolePermissionCommands.Commands;
using Core.Application.Interfaces.Dtos;
using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities;
using MediatR;

namespace Core.Application.Features.Commands.RolePermissionCommands.Handlers;

public class AddRolePermissionHandler : BaseCommandHandler,
    IRequestHandler<AddRolePermissionCommand, IResultDataDto<RolePermissionDto>>
{
    private readonly IMapper _mapper;
    private readonly IRolePermissionRepository _rolePermissionRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILogRepository _logRepository;

    public AddRolePermissionHandler(IMapper mapper, IRolePermissionRepository rolePermissionRepository,
        IUserRepository userRepository, ILogRepository logRepository) : base(userRepository, logRepository)
    {
        _mapper = mapper;
        _rolePermissionRepository = rolePermissionRepository;
        _logRepository = logRepository;
        _userRepository = userRepository;
    }

    public async Task<IResultDataDto<RolePermissionDto>> Handle(AddRolePermissionCommand request,
        CancellationToken cancellationToken)
    {
        IResultDataDto<RolePermissionDto> resultDataDto = new ResultDataDto<RolePermissionDto>();

        try
        {
            var mapperEntity = _mapper.Map<RolePermission>(request);
            var addResult = await _rolePermissionRepository.AddAsync(mapperEntity);
            var resultMapper = _mapper.Map<RolePermissionDto>(addResult);
            resultDataDto.SetStatus().SetMessage("The create was successful").SetData(resultMapper);

            await AddUserLog("RolePermission Create Handler", "RolePermission", mapperEntity.Id, TransactionEnum.Create,
                addResult.Id);
        }
        catch (Exception exception)
        {
            resultDataDto.SetStatus(false).SetErrorMessage(exception.Message);
        }

        return resultDataDto;
    }
}