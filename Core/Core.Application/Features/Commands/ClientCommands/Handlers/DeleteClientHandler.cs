using AutoMapper;
using Core.Application.Dtos;
using Core.Application.Dtos.ResultDtos;
using Core.Application.Enums;
using Core.Application.Features.Commands.ClientCommands.Commands;
using Core.Application.Interfaces.Dtos;
using Core.Application.Interfaces.Repositories;
using MediatR;

namespace Core.Application.Features.Commands.ClientCommands.Handlers;

public class DeleteClientHandler : BaseCommandHandler,
    IRequestHandler<DeleteClientCommand, IResultDataDto<bool>>
{
    private readonly IMapper _mapper;
    private readonly IClientRepository _clientRepository;
    private readonly ILogRepository _logRepository;
    private readonly IUserRepository _userRepository;

    public DeleteClientHandler(IMapper mapper, IClientRepository clientRepository,
        ILogRepository logRepository, IUserRepository userRepository) : base(userRepository, logRepository)
    {
        _mapper = mapper;
        _clientRepository = clientRepository;
        _logRepository = logRepository;
        _userRepository = userRepository;
    }

    public async Task<IResultDataDto<bool>> Handle(DeleteClientCommand request,
        CancellationToken cancellationToken)
    {
        IResultDataDto<bool> result = new ResultDataDto<bool>();
        try
        {
            var getData = _clientRepository.GetSingle(predicate: d => d.Id == request.Id);
            if (getData == null)
                return result.SetStatus(false).SetErrorMessage("Not Found Data")
                    .SetMessage("The content related to the ID value could not be found.");
            getData.IsEnable = false;
            await _clientRepository.UpdateAsync(getData);
            await AddUserLog("Client Delete Handler", "Client", getData.Id, TransactionEnum.Delete,
                getData.Id);
        }
        catch (Exception exception)
        {
            result.SetStatus(false).SetErrorMessage(exception.Message);
        }

        return result;
    }
}