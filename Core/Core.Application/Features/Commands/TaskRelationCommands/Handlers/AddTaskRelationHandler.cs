using AutoMapper;
using Core.Application.Dtos;
using Core.Application.Dtos.ResultDtos;
using Core.Application.Enums;
using Core.Application.Features.Commands.TaskRelationCommands.Commands;
using Core.Application.Interfaces.Dtos;
using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities;
using MediatR;

namespace Core.Application.Features.Commands.TaskRelationCommands.Handlers;

public class AddTaskRelationHandler : BaseCommandHandler,
    IRequestHandler<AddTaskRelationCommand, IResultDataDto<TaskRelationDto>>
{
    private readonly IMapper _mapper;
    private readonly ITaskRelationRepository _taskRelationRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILogRepository _logRepository;

    public AddTaskRelationHandler(IMapper mapper, ITaskRelationRepository taskRelationRepository,
        IUserRepository userRepository, ILogRepository logRepository) : base(userRepository, logRepository)
    {
        _mapper = mapper;
        _taskRelationRepository = taskRelationRepository;
        _logRepository = logRepository;
        _userRepository = userRepository;
    }

    public async Task<IResultDataDto<TaskRelationDto>> Handle(AddTaskRelationCommand request,
        CancellationToken cancellationToken)
    {
        IResultDataDto<TaskRelationDto> resultDataDto = new ResultDataDto<TaskRelationDto>();

        try
        {
            var mapperEntity = _mapper.Map<TaskRelation>(request);
            var addResult = await _taskRelationRepository.AddAsync(mapperEntity);
            var resultMapper = _mapper.Map<TaskRelationDto>(addResult);
            resultDataDto.SetStatus().SetMessage("The create was successful").SetData(resultMapper);

            await AddUserLog("TaskRelation Create Handler", "TaskRelation", mapperEntity.Id, TransactionEnum.Create,
                addResult.Id);
        }
        catch (Exception exception)
        {
            resultDataDto.SetStatus(false).SetErrorMessage(exception.Message);
        }

        return resultDataDto;
    }
}