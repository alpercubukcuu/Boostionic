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
    public class UpdateSetupSettingHandler : BaseCommandHandler, IRequestHandler<UpdateSetupSettingCommand, IResultDataDto<SetupSettingDto>>
    {
        private readonly IMapper _mapper;
        private readonly ILogRepository _logRepository;
        private readonly IUserRepository _userRepository;
        private readonly ISetupSettingRepository _setupSettingRepository;

        public UpdateSetupSettingHandler(IMapper mapper, ILogRepository logRepository, IUserRepository userRepository, ISetupSettingRepository setupSettingRepository) : base(
            userRepository, logRepository)
        {
            _mapper = mapper;
            _logRepository = logRepository;
            _userRepository = userRepository;
            _setupSettingRepository = setupSettingRepository;
        }

        public async Task<IResultDataDto<SetupSettingDto>> Handle(UpdateSetupSettingCommand request,
            CancellationToken cancellationToken)
        {
            IResultDataDto<SetupSettingDto> result = new ResultDataDto<SetupSettingDto>();
            try
            {
                var getData = _setupSettingRepository.GetSingle(predicate: p => p.Id == request.Id);
                if (getData == null)
                    return result.SetStatus(false).SetErrorMessage("Not Found Data")
                        .SetMessage("No content found for the ID value");

                var map = _mapper.Map<SetupSetting>(request);
                map.CreatedDate = getData.CreatedDate;
                map.Id = getData.Id;

                var addResult = await _setupSettingRepository.UpdateAsync(map);
                var resultMap = _mapper.Map<SetupSettingDto>(addResult);

                result.SetStatus().SetMessage("The update process was successful").SetData(resultMap);
                await AddUserLog("Setup Setting Update Handler", "SetupSetting", map.Id, TransactionEnum.Update, getData.Id);
            }
            catch (Exception exception)
            {
                result.SetStatus(false).SetErrorMessage(exception.Message);
            }

            return result;
        }
    }
}