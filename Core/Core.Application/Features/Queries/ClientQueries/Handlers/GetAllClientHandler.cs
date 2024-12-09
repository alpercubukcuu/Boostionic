using AutoMapper;
using Core.Application.Dtos;
using Core.Application.Dtos.ResultDtos;
using Core.Application.Features.Queries.ClientQueries.Queries;
using Core.Application.Interfaces.Dtos;
using Core.Application.Interfaces.Repositories;
using MediatR;

namespace Core.Application.Features.Queries.ClientQueries.Handlers;

public class GetAllClientHandler : IRequestHandler<GetAllClientQuery, IResultDataDto<List<ClientDto>>>
{
    private readonly IMapper _mapper;
    private readonly IClientRepository _clientRepository;

    public GetAllClientHandler(IMapper mapper, IClientRepository clientRepository)
    {
        _mapper = mapper;
        _clientRepository = clientRepository;
    }

    public async Task<IResultDataDto<List<ClientDto>>> Handle(GetAllClientQuery request,
        CancellationToken cancellationToken)
    {
        IResultDataDto<List<ClientDto>> result = new ResultDataDto<List<ClientDto>>();
        try
        {
            var repoResult = _clientRepository.GetAll(predicate: d => d.IsEnable == true);
            if (repoResult.Count() <= 0)
                result.SetStatus(false).SetErrorMessage("Not Found Data").SetMessage("The list is empty!");
            var map = _mapper.Map<List<ClientDto>>(repoResult);
            result.SetData(map);
            return result;
        }
        catch (Exception exception)
        {
            return result.SetStatus(false).SetErrorMessage(exception.Message);
        }
    }
}