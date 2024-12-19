using AutoMapper;
using Core.Application.Dtos;
using Core.Application.Dtos.ResultDtos;
using Core.Application.Enums;
using Core.Application.Features.Commands.UserRoleCommands.Commands;
using Core.Application.Interfaces.Dtos;
using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities;
using MediatR;

namespace Core.Application.Features.Commands.UserRoleCommands.Handlers;

public class UpdateUserRoleHandler : BaseCommandHandler,
    IRequestHandler<UpdateUserRoleCommand, IResultDataDto<UserRoleDto>>
{
    private readonly IMapper _mapper;
    private readonly IUserRoleRepository _userRoleRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILogRepository _logRepository;

    public UpdateUserRoleHandler(IMapper mapper, IUserRoleRepository userRoleRepository,
        IUserRepository userRepository, ILogRepository logRepository) : base(userRepository, logRepository)
    {
        _mapper = mapper;
        _userRoleRepository = userRoleRepository;
        _userRepository = userRepository;
        _logRepository = logRepository;
    }

    public async Task<IResultDataDto<UserRoleDto>> Handle(UpdateUserRoleCommand request,
        CancellationToken cancellationToken)
    {
        IResultDataDto<UserRoleDto> resultDataDto = new ResultDataDto<UserRoleDto>();

        try
        {
            var getData = _userRoleRepository.GetSingle(predicate: x => x.Id == request.Id);

            if (getData == null)
                resultDataDto.SetStatus(false).SetErrorMessage("User Role not found")
                    .SetMessage("No User Role found for the ID value");

            var mappedEntity = _mapper.Map<UserRole>(getData);
            mappedEntity.CreatedDate = getData.CreatedDate;
            mappedEntity.Id = getData.Id;

            var addResult = await _userRoleRepository.UpdateAsync(mappedEntity);
            var resultMapper = _mapper.Map<UserRoleDto>(addResult);

            resultDataDto.SetStatus().SetMessage("The update process was successful").SetData(resultMapper);
            await AddUserLog("UserRole Update Handler", "UserRole", mappedEntity.Id,
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