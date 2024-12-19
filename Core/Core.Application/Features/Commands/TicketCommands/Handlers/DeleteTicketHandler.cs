using AutoMapper;
using Core.Application.Dtos.ResultDtos;
using Core.Application.Enums;
using Core.Application.Features.Commands.TicketCommands.Commands;
using Core.Application.Interfaces.Dtos;
using Core.Application.Interfaces.Repositories;
using MediatR;

namespace Core.Application.Features.Commands.TicketCommands.Handlers;

public class DeleteTicketHandler : BaseCommandHandler,
    IRequestHandler<DeleteTicketCommand, IResultDataDto<bool>>
{
    private readonly IMapper _mapper;
    private readonly ITicketRepository _ticketRepository;
    private readonly ILogRepository _logRepository;
    private readonly IUserRepository _userRepository;

    public DeleteTicketHandler(IMapper mapper, ITicketRepository ticketRepository,
        IUserRepository userRepository, ILogRepository logRepository) : base(userRepository, logRepository)
    {
        _mapper = mapper;
        _ticketRepository = ticketRepository;
        _userRepository = userRepository;
        _logRepository = logRepository;
    }

    public async Task<IResultDataDto<bool>> Handle(DeleteTicketCommand request,
        CancellationToken cancellationToken)
    {
        IResultDataDto<bool> resultDataDto = new ResultDataDto<bool>();

        try
        {
            var getData = _ticketRepository.GetSingle(predicate: x => x.Id == request.Id);
            if (getData == null)
                resultDataDto.SetStatus(false).SetErrorMessage("Ticket not found")
                    .SetMessage("The Ticket related to the ID value could not be found.");
            getData.IsEnable = false;
            await _ticketRepository.UpdateAsync(getData);
            await AddUserLog("Ticket Delete Handler", "Ticket", getData.Id, TransactionEnum.Delete,
                getData.Id);
        }
        catch (Exception exception)
        {
            resultDataDto.SetStatus(false).SetErrorMessage(exception.Message);
        }

        return resultDataDto;
    }
}