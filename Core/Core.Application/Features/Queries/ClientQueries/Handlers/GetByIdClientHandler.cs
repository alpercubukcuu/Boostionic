using AutoMapper;
using Core.Application.Dtos;
using Core.Application.Dtos.ResultDtos;
using Core.Application.Features.Queries.ClientQueries.Queries;
using Core.Application.Interfaces.Dtos;
using Core.Application.Interfaces.Repositories;
using MediatR;

namespace Core.Application.Features.Queries.ClientQueries.Handlers;

public class GetByIdClientHandler : IRequestHandler<GetByIdClientQuery, IResultDataDto<ClientDto>>
{
    private readonly IMapper _mapper;
    private readonly IClientRepository _clientRepository;

    public GetByIdClientHandler(IMapper mapper, IClientRepository clientRepository)
    {
        _mapper = mapper;
        _clientRepository = clientRepository;
    }

    public async Task<IResultDataDto<ClientDto>> Handle(GetByIdClientQuery request, CancellationToken cancellationToken)
    {
        IResultDataDto<ClientDto> result = new ResultDataDto<ClientDto>();
        try
        {
            var repoResult = _clientRepository.GetSingle(predicate: d => d.IsEnable == true && d.Id == request.Id);
            if (repoResult == null)
                result.SetStatus(false).SetErrorMessage("Not Found Data").SetMessage("No data found for the ID value");
            var map = _mapper.Map<ClientDto>(repoResult);
            result.SetData(map);
            return result;
        }
        catch (Exception exception)
        {
            return result.SetStatus(false).SetErrorMessage(exception.Message);
        }
    }
}