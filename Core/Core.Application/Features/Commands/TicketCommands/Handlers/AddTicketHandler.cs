using AutoMapper;
using Core.Application.Dtos;
using Core.Application.Dtos.ResultDtos;
using Core.Application.Enums;
using Core.Application.Features.Commands.TicketCommands.Commands;
using Core.Application.Interfaces.Dtos;
using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities;
using MediatR;

namespace Core.Application.Features.Commands.TicketCommands.Handlers;

public class AddTicketHandler : BaseCommandHandler,
    IRequestHandler<AddTicketCommand, IResultDataDto<TicketDto>>
{
    private readonly IMapper _mapper;
    private readonly ITicketRepository _ticketRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILogRepository _logRepository;

    public AddTicketHandler(IMapper mapper, ITicketRepository ticketRepository,
        IUserRepository userRepository, ILogRepository logRepository) : base(userRepository, logRepository)
    {
        _mapper = mapper;
        _ticketRepository = ticketRepository;
        _logRepository = logRepository;
        _userRepository = userRepository;
    }

    public async Task<IResultDataDto<TicketDto>> Handle(AddTicketCommand request,
        CancellationToken cancellationToken)
    {
        IResultDataDto<TicketDto> resultDataDto = new ResultDataDto<TicketDto>();

        try
        {
            var mapperEntity = _mapper.Map<Ticket>(request);
            var addResult = await _ticketRepository.AddAsync(mapperEntity);
            var resultMapper = _mapper.Map<TicketDto>(addResult);
            resultDataDto.SetStatus().SetMessage("The create was successful").SetData(resultMapper);

            await AddUserLog("Ticket Create Handler", "Ticket", mapperEntity.Id, TransactionEnum.Create,
                addResult.Id);
        }
        catch (Exception exception)
        {
            resultDataDto.SetStatus(false).SetErrorMessage(exception.Message);
        }

        return resultDataDto;
    }
}