using AutoMapper;
using Core.Application.Dtos;
using Core.Application.Dtos.ResultDtos;
using Core.Application.Features.Queries.TicketQueries.Queries;
using Core.Application.Interfaces.Dtos;
using Core.Application.Interfaces.Repositories;
using MediatR;

namespace Core.Application.Features.Queries.TicketQueries.Handlers;

public class
    GetAllTicketHandler : IRequestHandler<GetAllTicketQuery, IResultDataDto<List<TicketDto>>>
{
    private readonly IMapper _mapper;
    private readonly ITicketRepository _ticketRepository;

    public GetAllTicketHandler(IMapper mapper, ITicketRepository ticketRepository)
    {
        _mapper = mapper;
        _ticketRepository = ticketRepository;
    }

    public async Task<IResultDataDto<List<TicketDto>>> Handle(GetAllTicketQuery request,
        CancellationToken cancellationToken)
    {
        IResultDataDto<List<TicketDto>> result = new ResultDataDto<List<TicketDto>>();
        try
        {
            var repoResult = _ticketRepository.GetAll(predicate: d => d.IsEnable == true);
            if (repoResult.Count() <= 0)
                result.SetStatus(false).SetErrorMessage("Not Found Data").SetMessage("The list is empty!");
            var map = _mapper.Map<List<TicketDto>>(repoResult);
            result.SetData(map);
            return result;
        }
        catch (Exception exception)
        {
            return result.SetStatus(false).SetErrorMessage(exception.Message);
        }
    }
}