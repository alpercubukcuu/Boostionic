using AutoMapper;
using Core.Application.Dtos;
using Core.Application.Dtos.ResultDtos;
using Core.Application.Enums;
using Core.Application.Features.Commands.SetupSettingCommands.Commands;
using Core.Application.Interfaces.Dtos;
using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities;
using MediatR;


namespace Core.Application.Features.Commands.SetupSettingCommands.Handlers
{
    public class AddSetupSettingHandler : BaseCommandHandler, IRequestHandler<AddSetupSettingCommand, IResultDataDto<SetupSettingDto>>
    {
        private readonly IMapper _mapper;
        private readonly ILogRepository _logRepository;
        private readonly IUserRepository _userRepository;
        public readonly ISetupSettingRepository _setupSettingRepository;

        public AddSetupSettingHandler(IMapper mapper, ILogRepository logRepository, IUserRepository userRepository, ISetupSettingRepository setupSettingRepository) : base(
            userRepository, logRepository)
        {
            _mapper = mapper;
            _logRepository = logRepository;
            _userRepository = userRepository;
            _setupSettingRepository = setupSettingRepository;
        }

        public async Task<IResultDataDto<SetupSettingDto>> Handle(AddSetupSettingCommand request, CancellationToken cancellationToken)
        {
            IResultDataDto<SetupSettingDto> result = new ResultDataDto<SetupSettingDto>();
            try
            {
                var map = _mapper.Map<SetupSetting>(request);

                var addResult = await _setupSettingRepository.AddAsync(map);
                var resultMap = _mapper.Map<SetupSettingDto>(addResult);
                result.SetStatus().SetMessage("The create was successful").SetData(resultMap);

                await AddUserLog("Setup Setting Create Handler", "SetupSetting", map.Id, TransactionEnum.Create, addResult.Id);
            }
            catch (Exception exception)
            {
                result.SetStatus(false).SetErrorMessage(exception.Message);
            }

            return result;
        }
    }
}