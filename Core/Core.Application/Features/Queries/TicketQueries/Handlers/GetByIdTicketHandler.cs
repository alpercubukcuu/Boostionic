using AutoMapper;
using Core.Application.Dtos;
using Core.Application.Dtos.ResultDtos;
using Core.Application.Features.Queries.TicketQueries.Queries;
using Core.Application.Interfaces.Dtos;
using Core.Application.Interfaces.Repositories;
using MediatR;

namespace Core.Application.Features.Queries.TicketQueries.Handlers;

public class
    GetByIdTicketHandler : IRequestHandler<GetByIdTicketQuery, IResultDataDto<TicketDto>>
{
    private readonly IMapper _mapper;
    private readonly ITicketRepository _ticketRepository;

    public GetByIdTicketHandler(IMapper mapper, ITicketRepository ticketRepository)
    {
        _mapper = mapper;
        _ticketRepository = ticketRepository;
    }

    public async Task<IResultDataDto<TicketDto>> Handle(GetByIdTicketQuery request,
        CancellationToken cancellationToken)
    {
        IResultDataDto<TicketDto> result = new ResultDataDto<TicketDto>();
        try
        {
            var repositoryResult =
                _ticketRepository.GetSingle(predicate: d => d.IsEnable == true && d.Id == request.Id);
            if (repositoryResult == null)
                result.SetStatus(false).SetErrorMessage("Not Found Data").SetMessage("No data found for the ID value");
            var map = _mapper.Map<TicketDto>(repositoryResult);
            return result;
        }
        catch (Exception exception)
        {
            return result.SetStatus(false).SetErrorMessage(exception.Message);
        }
    }
}