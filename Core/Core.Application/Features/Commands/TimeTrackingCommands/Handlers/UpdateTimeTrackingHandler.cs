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

public class UpdateTimeTrackingHandler : BaseCommandHandler,
    IRequestHandler<UpdateTimeTrackingCommand, IResultDataDto<TimeTrackingDto>>
{
    private readonly IMapper _mapper;
    private readonly ITimeTrackingRepository _timeTrackingRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILogRepository _logRepository;

    public UpdateTimeTrackingHandler(IMapper mapper, ITimeTrackingRepository timeTrackingRepository,
        IUserRepository userRepository, ILogRepository logRepository) : base(userRepository, logRepository)
    {
        _mapper = mapper;
        _timeTrackingRepository = timeTrackingRepository;
        _userRepository = userRepository;
        _logRepository = logRepository;
    }

    public async Task<IResultDataDto<TimeTrackingDto>> Handle(UpdateTimeTrackingCommand request,
        CancellationToken cancellationToken)
    {
        IResultDataDto<TimeTrackingDto> resultDataDto = new ResultDataDto<TimeTrackingDto>();

        try
        {
            var getData = _timeTrackingRepository.GetSingle(predicate: x => x.Id == request.Id);

            if (getData == null)
                resultDataDto.SetStatus(false).SetErrorMessage("Time Tracking not found")
                    .SetMessage("No Time Tracking found for the ID value");

            var mappedEntity = _mapper.Map<TimeTracking>(getData);
            mappedEntity.CreatedDate = getData.CreatedDate;
            mappedEntity.Id = getData.Id;

            var addResult = await _timeTrackingRepository.UpdateAsync(mappedEntity);
            var resultMapper = _mapper.Map<TimeTrackingDto>(addResult);

            resultDataDto.SetStatus().SetMessage("The update process was successful").SetData(resultMapper);
            await AddUserLog("TimeTracking Update Handler", "TimeTracking", mappedEntity.Id,
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