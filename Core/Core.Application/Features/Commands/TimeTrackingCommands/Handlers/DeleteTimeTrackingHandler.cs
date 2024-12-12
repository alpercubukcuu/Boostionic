using AutoMapper;
using Core.Application.Dtos.ResultDtos;
using Core.Application.Enums;
using Core.Application.Features.Commands.TimeTrackingCommands.Commands;
using Core.Application.Interfaces.Dtos;
using Core.Application.Interfaces.Repositories;
using MediatR;

namespace Core.Application.Features.Commands.TimeTrackingCommands.Handlers;

public class DeleteTimeTrackingHandler : BaseCommandHandler,
    IRequestHandler<DeleteTimeTrackingCommand, IResultDataDto<bool>>
{
    private readonly IMapper _mapper;
    private readonly ITimeTrackingRepository _timeTrackingRepository;
    private readonly ILogRepository _logRepository;
    private readonly IUserRepository _userRepository;

    public DeleteTimeTrackingHandler(IMapper mapper, ITimeTrackingRepository timeTrackingRepository,
        IUserRepository userRepository, ILogRepository logRepository) : base(userRepository, logRepository)
    {
        _mapper = mapper;
        _timeTrackingRepository = timeTrackingRepository;
        _userRepository = userRepository;
        _logRepository = logRepository;
    }

    public async Task<IResultDataDto<bool>> Handle(DeleteTimeTrackingCommand request,
        CancellationToken cancellationToken)
    {
        IResultDataDto<bool> resultDataDto = new ResultDataDto<bool>();

        try
        {
            var getData = _timeTrackingRepository.GetSingle(predicate: x => x.Id == request.Id);
            if (getData == null)
                resultDataDto.SetStatus(false).SetErrorMessage("Time Tracking not found")
                    .SetMessage("The Time Tracking related to the ID value could not be found.");
            getData.IsEnable = false;
            await _timeTrackingRepository.UpdateAsync(getData);
            await AddUserLog("TimeTracking Delete Handler", "TimeTracking", getData.Id, TransactionEnum.Delete,
                getData.Id);
        }
        catch (Exception exception)
        {
            resultDataDto.SetStatus(false).SetErrorMessage(exception.Message);
        }

        return resultDataDto;
    }
}