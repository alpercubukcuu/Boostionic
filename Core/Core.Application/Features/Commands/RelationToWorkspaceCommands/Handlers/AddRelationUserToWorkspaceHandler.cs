using AutoMapper;
using Core.Application.Dtos;
using Core.Application.Dtos.ResultDtos;
using Core.Application.Enums;
using Core.Application.Features.Commands.RelationToWorkspaceCommands.Commands;
using Core.Application.Interfaces.Dtos;
using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities;
using MediatR;

namespace Core.Application.Features.Commands.RelationToWorkspaceCommands.Handlers;

public class AddRelationUserToWorkspaceHandler : BaseCommandHandler,
    IRequestHandler<AddRelationUserToWorkspaceCommand, IResultDataDto<RelationUserToWorkspaceDto>>
{
    private readonly IMapper _mapper;
    private readonly IRelationUserToWorkspaceRepository _relationUserToWorkspaceRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILogRepository _logRepository;

    public AddRelationUserToWorkspaceHandler(IMapper mapper, IRelationUserToWorkspaceRepository relationUserToWorkspaceRepository,
        IUserRepository userRepository, ILogRepository logRepository) : base(userRepository, logRepository)
    {
        _mapper = mapper;
        _relationUserToWorkspaceRepository = relationUserToWorkspaceRepository;
        _logRepository = logRepository;
        _userRepository = userRepository;
    }

    public async Task<IResultDataDto<RelationUserToWorkspaceDto>> Handle(AddRelationUserToWorkspaceCommand request,
        CancellationToken cancellationToken)
    {
        IResultDataDto<RelationUserToWorkspaceDto> resultDataDto = new ResultDataDto<RelationUserToWorkspaceDto>();

        try
        {
            var mapperEntity = _mapper.Map<RelationUserToWorkspace>(request);
            var addResult = await _relationUserToWorkspaceRepository.AddAsync(mapperEntity);
            var resultMapper = _mapper.Map<RelationUserToWorkspaceDto>(addResult);
            resultDataDto.SetStatus().SetMessage("The create was successful").SetData(resultMapper);

            await AddUserLog("RelationUserToWorkspace Create Handler", "RelationUserToWorkspace", mapperEntity.Id, TransactionEnum.Create,
                addResult.Id);
        }
        catch (Exception exception)
        {
            resultDataDto.SetStatus(false).SetErrorMessage(exception.Message);
        }

        return resultDataDto;
    }
}