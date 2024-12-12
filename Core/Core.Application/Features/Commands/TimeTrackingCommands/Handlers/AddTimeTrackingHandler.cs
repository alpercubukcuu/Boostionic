using AutoMapper;
using Core.Application.Dtos;
using Core.Application.Dtos.ResultDtos;
using Core.Application.Enums;
using Core.Application.Features.Commands.TimeTrackingCommands.Commands;
using Core.Application.Interfaces.Dtos;
using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities;
using MediatR;

namespace Core.Application.Features.Commands.TimeTrackingCommands.Handlers;

public class AddTimeTrackingHandler : BaseCommandHandler,
    IRequestHandler<AddTimeTrackingCommand, IResultDataDto<TimeTrackingDto>>
{
    private readonly IMapper _mapper;
    private readonly ITimeTrackingRepository _timeTrackingRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILogRepository _logRepository;

    public AddTimeTrackingHandler(IMapper mapper, ITimeTrackingRepository timeTrackingRepository,
        IUserRepository userRepository, ILogRepository logRepository) : base(userRepository, logRepository)
    {
        _mapper = mapper;
        _timeTrackingRepository = timeTrackingRepository;
        _logRepository = logRepository;
        _userRepository = userRepository;
    }

    public async Task<IResultDataDto<TimeTrackingDto>> Handle(AddTimeTrackingCommand request,
        CancellationToken cancellationToken)
    {
        IResultDataDto<TimeTrackingDto> resultDataDto = new ResultDataDto<TimeTrackingDto>();

        try
        {
            var mapperEntity = _mapper.Map<TimeTracking>(request);
            var addResult = await _timeTrackingRepository.AddAsync(mapperEntity);
            var resultMapper = _mapper.Map<TimeTrackingDto>(addResult);
            resultDataDto.SetStatus().SetMessage("The create was successful").SetData(resultMapper);

            await AddUserLog("TimeTracking Create Handler", "TimeTracking", mapperEntity.Id, TransactionEnum.Create,
                addResult.Id);
        }
        catch (Exception exception)
        {
            resultDataDto.SetStatus(false).SetErrorMessage(exception.Message);
        }

        return resultDataDto;
    }
}