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

public class UpdateClientHandler : BaseCommandHandler,
        IRequestHandler<UpdateClientCommand, IResultDataDto<ClientDto>>
    {
        private readonly IMapper _mapper;
        private readonly IClientRepository _clientRepository;
        private readonly ILogRepository _logRepository;
        private readonly IUserRepository _userRepository;

        public UpdateClientHandler(IMapper mapper, IClientRepository clientRepository,
            ILogRepository logRepository, IUserRepository userRepository) : base(userRepository, logRepository)
        {
            _mapper = mapper;
            _clientRepository = clientRepository;
            _logRepository = logRepository;
            _userRepository = userRepository;
        }

        public async Task<IResultDataDto<ClientDto>> Handle(UpdateClientCommand request,
            CancellationToken cancellationToken)
        {
            IResultDataDto<ClientDto> result = new ResultDataDto<ClientDto>();
            try
            {
                var getData = _clientRepository.GetSingle(predicate: p => p.Id == request.Id);
                if (getData == null)
                    return result.SetStatus(false).SetErrorMessage("Not Found Data")
                        .SetMessage("No content found for the ID value");

                var map = _mapper.Map<Client>(request);
                map.CreatedDate = getData.CreatedDate;
                map.Id = getData.Id;

                var addResult = await _clientRepository.UpdateAsync(map);
                var resultMap = _mapper.Map<ClientDto>(addResult);

                result.SetStatus().SetMessage("The update process was successful").SetData(resultMap);
                await AddUserLog("Client Update Handler", "Client", map.Id, TransactionEnum.Update,
                    getData.Id);
            }
            catch (Exception exception)
            {
                result.SetStatus(false).SetErrorMessage(exception.Message);
            }

            return result;
        }
    }