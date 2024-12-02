using AutoMapper;
using Core.Application.Dtos.ResultDtos;
using Core.Application.Enums;
using Core.Application.Features.Commands.SetupSettingCommands.Commands;
using Core.Application.Interfaces.Dtos;
using Core.Application.Interfaces.Repositories;
using MediatR;


namespace Core.Application.Features.Commands.SetupSettingCommands.Handlers
{
    public class DeleteSetupSettingHandler : BaseCommandHandler, IRequestHandler<DeleteSetupSettingCommand, IResultDataDto<bool>>
    {
        private readonly IMapper _mapper;
        private readonly ILogRepository _logRepository;
        private readonly IUserRepository _userRepository;
        private readonly ISetupSettingRepository _setupSettingRepository;

        public DeleteSetupSettingHandler(IMapper mapper, ILogRepository logRepository, IUserRepository userRepository, ISetupSettingRepository setupSettingRepository) : base(
            userRepository, logRepository)
        {
            _mapper = mapper;
            _logRepository = logRepository;
            _userRepository = userRepository;
            _setupSettingRepository = setupSettingRepository;
        }

        public async Task<IResultDataDto<bool>> Handle(DeleteSetupSettingCommand request, CancellationToken cancellationToken)
        {
            IResultDataDto<bool> result = new ResultDataDto<bool>();
            try
            {
                var getData = _setupSettingRepository.GetSingle(predicate: d => d.Id == request.Id);
                if (getData == null) return result.SetStatus(false).SetErrorMessage("Not Found Data").SetMessage("No content found for the ID value");
                getData.IsEnable = false;
                await _setupSettingRepository.UpdateAsync(getData);
                await AddUserLog("Setup Setting Delete Handler", "SetupSetting", getData.Id, TransactionEnum.Delete,
                    getData.Id);
            }
            catch (Exception exception)
            {
                result.SetStatus(false).SetErrorMessage(exception.Message);
            }

            return result;
        }
    }
}