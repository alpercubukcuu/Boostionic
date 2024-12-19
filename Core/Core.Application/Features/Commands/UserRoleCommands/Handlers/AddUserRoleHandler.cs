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

public class AddUserRoleHandler : BaseCommandHandler,
    IRequestHandler<AddUserRoleCommand, IResultDataDto<UserRoleDto>>
{
    private readonly IMapper _mapper;
    private readonly IUserRoleRepository _userRoleRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILogRepository _logRepository;

    public AddUserRoleHandler(IMapper mapper, IUserRoleRepository userRoleRepository,
        IUserRepository userRepository, ILogRepository logRepository) : base(userRepository, logRepository)
    {
        _mapper = mapper;
        _userRoleRepository = userRoleRepository;
        _logRepository = logRepository;
        _userRepository = userRepository;
    }

    public async Task<IResultDataDto<UserRoleDto>> Handle(AddUserRoleCommand request,
        CancellationToken cancellationToken)
    {
        IResultDataDto<UserRoleDto> resultDataDto = new ResultDataDto<UserRoleDto>();

        try
        {
            var mapperEntity = _mapper.Map<UserRole>(request);
            var addResult = await _userRoleRepository.AddAsync(mapperEntity);
            var resultMapper = _mapper.Map<UserRoleDto>(addResult);
            resultDataDto.SetStatus().SetMessage("The create was successful").SetData(resultMapper);

            await AddUserLog("UserRole Create Handler", "UserRole", mapperEntity.Id, TransactionEnum.Create,
                addResult.Id);
        }
        catch (Exception exception)
        {
            resultDataDto.SetStatus(false).SetErrorMessage(exception.Message);
        }

        return resultDataDto;
    }
}