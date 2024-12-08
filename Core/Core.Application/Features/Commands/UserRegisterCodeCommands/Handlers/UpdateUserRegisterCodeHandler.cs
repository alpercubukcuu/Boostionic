using AutoMapper;
using Core.Application.Dtos;
using Core.Application.Dtos.ResultDtos;
using Core.Application.Enums;
using Core.Application.Features.Commands.UserRegisterCodeCommands.Commands;
using Core.Application.Interfaces.Dtos;
using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities;
using MediatR;

namespace Core.Application.Features.Commands.UserRegisterCodeCommands.Handlers
{
    public class UpdateUserRegisterCodeHandler : BaseCommandHandler, IRequestHandler<UpdateUserRegisterCodeCommand, IResultDataDto<UserRegisterCodeDto>>
    {
        private readonly IMapper _mapper;
        private readonly ILogRepository _logRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserRegisterCodeRepository _userRegisterCodeRepository;
        public UpdateUserRegisterCodeHandler(IMapper mapper, ILogRepository logRepository, IUserRepository userRepository, IUserRegisterCodeRepository userRegisterCodeRepository) : base(userRepository, logRepository)
        {
            _mapper = mapper;
            _logRepository = logRepository;
            _userRepository = userRepository;
            _userRegisterCodeRepository = userRegisterCodeRepository;
        }

        public async Task<IResultDataDto<UserRegisterCodeDto>> Handle(UpdateUserRegisterCodeCommand request, CancellationToken cancellationToken)
        {
            IResultDataDto<UserRegisterCodeDto> result = new ResultDataDto<UserRegisterCodeDto>();
            try
            {
                var getData = _userRegisterCodeRepository.GetSingle(predicate: p => p.Id == request.Id);
                if (getData == null) return result.SetStatus(false).SetErrorMessage("Not Found Data").SetMessage("No content found for the ID value");

                var map = _mapper.Map<UserRegisterCode>(request);
                map.CreatedDate = getData.CreatedDate;
                map.Id = getData.Id;

                var addResult = await _userRegisterCodeRepository.UpdateAsync(map);
                var resultMap = _mapper.Map<UserRegisterCodeDto>(addResult);

                result.SetStatus().SetMessage("The update process was successful").SetData(resultMap);
                await AddUserLog("UserRegisterCode Update Handler", "UserRegisterCode", map.Id, TransactionEnum.Update, getData.Id);
            }
            catch (Exception exception) { result.SetStatus(false).SetErrorMessage(exception.Message); }
            return result;
        }

    }

}
