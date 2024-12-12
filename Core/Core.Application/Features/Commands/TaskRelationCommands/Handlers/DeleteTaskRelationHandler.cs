using AutoMapper;
using Core.Application.Dtos.ResultDtos;
using Core.Application.Enums;
using Core.Application.Features.Commands.TaskRelationCommands.Commands;
using Core.Application.Interfaces.Dtos;
using Core.Application.Interfaces.Repositories;
using MediatR;

namespace Core.Application.Features.Commands.TaskRelationCommands.Handlers;

public class DeleteTaskRelationHandler : BaseCommandHandler,
    IRequestHandler<DeleteTaskRelationCommand, IResultDataDto<bool>>
{
    private readonly IMapper _mapper;
    private readonly ITaskRelationRepository _taskRelationRepository;
    private readonly ILogRepository _logRepository;
    private readonly IUserRepository _userRepository;

    public DeleteTaskRelationHandler(IMapper mapper, ITaskRelationRepository taskRelationRepository,
        IUserRepository userRepository, ILogRepository logRepository) : base(userRepository, logRepository)
    {
        _mapper = mapper;
        _taskRelationRepository = taskRelationRepository;
        _userRepository = userRepository;
        _logRepository = logRepository;
    }

    public async Task<IResultDataDto<bool>> Handle(DeleteTaskRelationCommand request,
        CancellationToken cancellationToken)
    {
        IResultDataDto<bool> resultDataDto = new ResultDataDto<bool>();

        try
        {
            var getData = _taskRelationRepository.GetSingle(predicate: x => x.Id == request.Id);
            if (getData == null)
                resultDataDto.SetStatus(false).SetErrorMessage("Task Relation not found")
                    .SetMessage("The Task Relation related to the ID value could not be found.");
            getData.IsEnable = false;
            await _taskRelationRepository.UpdateAsync(getData);
            await AddUserLog("TaskRelation Delete Handler", "TaskRelation", getData.Id, TransactionEnum.Delete,
                getData.Id);
        }
        catch (Exception exception)
        {
            resultDataDto.SetStatus(false).SetErrorMessage(exception.Message);
        }

        return resultDataDto;
    }
}