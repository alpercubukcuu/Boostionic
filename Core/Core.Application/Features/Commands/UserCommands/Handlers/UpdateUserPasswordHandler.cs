using AutoMapper;
using Core.Application.Dtos;
using Core.Application.Dtos.ResultDtos;
using Core.Application.Enums;
using Core.Application.Features.Commands.UserCommands.Commands;
using Core.Application.Interfaces.Dtos;
using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities;
using MediatR;

namespace Core.Application.Features.Commands.BusinessPlaceCommands.Handlers
{
    public class UpdateUserPasswordHandler : BaseCommandHandler, IRequestHandler<UpdateUserPasswordCommand, IResultDataDto<UserDto>>
    {
        private readonly IMapper _mapper;
        private readonly ILogRepository _logRepository;
        private readonly IUserRepository _userRepository;
        public UpdateUserPasswordHandler(IMapper mapper, ILogRepository logRepository, IUserRepository userRepository) : base(userRepository, logRepository)
        {
            _mapper = mapper;
            _logRepository = logRepository;
            _userRepository = userRepository;
        }

        public async Task<IResultDataDto<UserDto>> Handle(UpdateUserPasswordCommand request, CancellationToken cancellationToken)
        {
            IResultDataDto<UserDto> result = new ResultDataDto<UserDto>();
            try
            {
                var getData = _userRepository.GetSingle(predicate: p => p.Id == request.Id);
                if (getData == null) return result.SetStatus(false).SetErrorMessage("Not Found Data").SetMessage("No content found for the ID value");

                getData.PasswordHash = request.ConfirmedPassword;
                getData.UpdatedDate = DateTime.UtcNow;

                var map = _mapper.Map<User>(getData);
             
                var addResult = await _userRepository.UpdateAsync(map);
                var resultMap = _mapper.Map<UserDto>(addResult);

                result.SetStatus().SetMessage("The update process was successful").SetData(resultMap);
                await AddUserLog("User Update Handler", "User", map.Id, TransactionEnum.Update, getData.Id);
            }
            catch (Exception exception) { result.SetStatus(false).SetErrorMessage(exception.Message); }
            return result;
        }

    }

}
