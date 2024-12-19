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

public class UpdateTicketHandler : BaseCommandHandler,
    IRequestHandler<UpdateTicketCommand, IResultDataDto<TicketDto>>
{
    private readonly IMapper _mapper;
    private readonly ITicketRepository _ticketRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILogRepository _logRepository;

    public UpdateTicketHandler(IMapper mapper, ITicketRepository ticketRepository,
        IUserRepository userRepository, ILogRepository logRepository) : base(userRepository, logRepository)
    {
        _mapper = mapper;
        _ticketRepository = ticketRepository;
        _userRepository = userRepository;
        _logRepository = logRepository;
    }

    public async Task<IResultDataDto<TicketDto>> Handle(UpdateTicketCommand request,
        CancellationToken cancellationToken)
    {
        IResultDataDto<TicketDto> resultDataDto = new ResultDataDto<TicketDto>();

        try
        {
            var getData = _ticketRepository.GetSingle(predicate: x => x.Id == request.Id);

            if (getData == null)
                resultDataDto.SetStatus(false).SetErrorMessage("Ticket not found")
                    .SetMessage("No Ticket found for the ID value");

            var mappedEntity = _mapper.Map<Ticket>(getData);
            mappedEntity.CreatedDate = getData.CreatedDate;
            mappedEntity.Id = getData.Id;

            var addResult = await _ticketRepository.UpdateAsync(mappedEntity);
            var resultMapper = _mapper.Map<TicketDto>(addResult);

            resultDataDto.SetStatus().SetMessage("The update process was successful").SetData(resultMapper);
            await AddUserLog("Ticket Update Handler", "Ticket", mappedEntity.Id,
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