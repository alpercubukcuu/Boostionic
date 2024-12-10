using AutoMapper;
using Core.Application.Dtos;
using Core.Application.Dtos.ResultDtos;
using Core.Application.Enums;
using Core.Application.Features.Commands.ClientCommands.Commands;
using Core.Application.Interfaces.Dtos;
using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities;
using MediatR;

namespace Core.Application.Features.Commands.ClientCommands.Handlers;

public class AddClientHandler : BaseCommandHandler,
    IRequestHandler<AddClientCommand, IResultDataDto<ClientDto>>
{
    private readonly IMapper _mapper;
    private readonly IClientRepository _clientRepository;
    private readonly ILogRepository _logRepository;
    private readonly IUserRepository _userRepository;

    public AddClientHandler(IMapper mapper, IClientRepository clientRepository,
        ILogRepository logRepository, IUserRepository userRepository) : base(userRepository, logRepository)
    {
        _mapper = mapper;
        _clientRepository = clientRepository;
        _logRepository = logRepository;
        _userRepository = userRepository;
    }

    public async Task<IResultDataDto<ClientDto>> Handle(AddClientCommand request,
        CancellationToken cancellationToken)
    {
        IResultDataDto<ClientDto> resultDataDto = new ResultDataDto<ClientDto>();
        try
        {
            var mapperEntity = _mapper.Map<Client>(request);
            var addResult = await _clientRepository.AddAsync(mapperEntity);
            var resultMapper = _mapper.Map<ClientDto>(addResult);
            resultDataDto.SetStatus().SetMessage("The create was successful").SetData(resultMapper);

            await AddUserLog("Client Create Handler", "Client", mapperEntity.Id, TransactionEnum.Create,
                addResult.Id);
        }
        catch (Exception exception)
        {
            resultDataDto.SetStatus(false).SetErrorMessage(exception.Message);
        }

        return resultDataDto;
    }
}