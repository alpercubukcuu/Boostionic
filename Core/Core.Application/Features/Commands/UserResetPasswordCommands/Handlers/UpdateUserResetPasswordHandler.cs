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
    public class UpdateUserResetPasswordHandler : BaseCommandHandler, IRequestHandler<UpdateUserResetPasswordCommand, IResultDataDto<UserResetPasswordDto>>
    {
        private readonly IMapper _mapper;
        private readonly ILogRepository _logRepository;
        private readonly IUserRepository _userRepository;
        public readonly IUserResetPasswordRepository _userResetPasswordRepository;
        public UpdateUserResetPasswordHandler(IMapper mapper, ILogRepository logRepository, IUserRepository userRepository, IUserResetPasswordRepository userResetPasswordRepository) : base(userRepository, logRepository)
        {
            _mapper = mapper;
            _logRepository = logRepository;
            _userRepository = userRepository;
            _userResetPasswordRepository = userResetPasswordRepository;
        }

        public async Task<IResultDataDto<UserResetPasswordDto>> Handle(UpdateUserResetPasswordCommand request, CancellationToken cancellationToken)
        {
            IResultDataDto<UserResetPasswordDto> result = new ResultDataDto<UserResetPasswordDto>();
            try
            {
                var getData = _userResetPasswordRepository.GetSingle(predicate: p => p.Id == request.Id);
                if (getData == null) return result.SetStatus(false).SetErrorMessage("Not Found Data").SetMessage("No content found for the ID value");

                var map = _mapper.Map<UserResetPassword>(request);
                map.CreatedDate = getData.CreatedDate;
                map.Id = getData.Id;

                var addResult = await _userResetPasswordRepository.UpdateAsync(map);
                var resultMap = _mapper.Map<UserResetPasswordDto>(addResult);

                result.SetStatus().SetMessage("The update process was successful").SetData(resultMap);
                await AddUserLog("UserResetPassword Update Handler", "UserResetPassword", map.Id, TransectionEnum.Update, getData.Id);
            }
            catch (Exception exception) { result.SetStatus(false).SetErrorMessage(exception.Message); }
            return result;
        }

    }

}
