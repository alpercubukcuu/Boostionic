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

public class UpdateRolePermissionStageHandler : BaseCommandHandler,
    IRequestHandler<UpdateRolePermissionCommand, IResultDataDto<RolePermissionDto>>
{
    private readonly IMapper _mapper;
    private readonly IRolePermissionRepository _rolePermissionRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILogRepository _logRepository;

    public UpdateRolePermissionStageHandler(IMapper mapper, IRolePermissionRepository rolePermissionRepository,
        IUserRepository userRepository, ILogRepository logRepository) : base(userRepository, logRepository)
    {
        _mapper = mapper;
        _rolePermissionRepository = rolePermissionRepository;
        _userRepository = userRepository;
        _logRepository = logRepository;
    }

    public async Task<IResultDataDto<RolePermissionDto>> Handle(UpdateRolePermissionCommand request,
        CancellationToken cancellationToken)
    {
        IResultDataDto<RolePermissionDto> resultDataDto = new ResultDataDto<RolePermissionDto>();

        try
        {
            var getData = _rolePermissionRepository.GetSingle(predicate: x => x.Id == request.Id);

            if (getData == null)
                resultDataDto.SetStatus(false).SetErrorMessage("Role Permission not found")
                    .SetMessage("No Role Permission found for the ID value");

            var mappedEntity = _mapper.Map<RolePermission>(getData);
            mappedEntity.CreatedDate = getData.CreatedDate;
            mappedEntity.Id = getData.Id;

            var addResult = await _rolePermissionRepository.UpdateAsync(mappedEntity);
            var resultMapper = _mapper.Map<RolePermissionDto>(addResult);

            resultDataDto.SetStatus().SetMessage("The update process was successful").SetData(resultMapper);
            await AddUserLog("RolePermission Update Handler", "RolePermission", mappedEntity.Id,
                TransactionEnum.Update,
                getData.Id);
        }
        catch (Exception exception)
        {
            resultDataDto.SetStatus(false).SetErrorMessage(exception.Message);
        }

        return resultDataDto;
    }
}