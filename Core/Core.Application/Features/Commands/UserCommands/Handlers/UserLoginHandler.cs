using AutoMapper;
using Core.Application.Dtos.ResultDtos;
using Core.Application.Dtos;
using Core.Application.Enums;
using Core.Application.Features.Commands.UserCommands.Commands;
using Core.Application.Interfaces.Dtos;
using Core.Application.Interfaces.Repositories;
using MediatR;
using Core.Application.Helper;
using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;



namespace Core.Application.Features.Commands.UserCommands.Handlers
{
    public class UserLoginHandler : BaseCommandHandler, IRequestHandler<UserLoginCommand, IResultDataDto<UserDto>>
    {
        private readonly IMapper _mapper;
        private readonly ILogRepository _logRepository;
        private readonly IUserRepository _userRepository;
        private readonly IJwtRepository _jwtRepository;
        public UserLoginHandler(IMapper mapper, ILogRepository logRepository, IUserRepository userRepository, IJwtRepository jwtRepository) : base(userRepository, logRepository)
        {
            _mapper = mapper;
            _logRepository = logRepository;
            _userRepository = userRepository;
            _jwtRepository = jwtRepository;
        }

        public async Task<IResultDataDto<UserDto>> Handle(UserLoginCommand request, CancellationToken cancellationToken)
        {
            IResultDataDto<UserDto> result = new ResultDataDto<UserDto>() { Data = new UserDto() };
            try
            {
                var getData = _userRepository.GetSingle(predicate: p => p.Email == request.Email && p.EmailVerified == true, include: p => p.Include(p => p.Client).Include(p => p.UserRole));

                if (getData == null) return result.SetStatus(false).SetErrorMessage("Not Found Data").SetMessage("Your account was not found! If you think there is a mistake, please contact the support team.");

                if (getData.EmailVerified == false) return result.SetStatus(false).SetErrorMessage("Not Found Data").SetMessage("Your account is not enable. Please contact the support team.");
                
                if (!Cipher.Decrypt(request.Password, getData.PasswordHash))
                {
                    getData.FailedLoginAttempts = (getData.FailedLoginAttempts ?? 0) + 1;
                    getData.LastFailedLoginAttempt = DateTime.Now;

                    if (getData.FailedLoginAttempts >= 5)
                    {
                        getData.IsEnable = false;
                    }
                    await _userRepository.UpdateAsync(getData);

                    var remainingAttempts = 5 - (getData.FailedLoginAttempts ?? 0);
                    return result.SetStatus(false)
                                 .SetErrorMessage($"Incorrect password or email! You have {remainingAttempts} attempts left.");
                }


                getData.FailedLoginAttempts = 0;
                getData.LastLogin = DateTime.Now;

                await _userRepository.UpdateAsync(getData);
                var token = _jwtRepository.GenerateJwtToken(getData);

                result.Data.Token = token;
                result.Data.Id = getData.Id;

                result.SetStatus().SetMessage("The login process was successful").SetData(result.Data);
                await AddUserLog("User Login Handler", "User", getData.Id, TransactionEnum.Update, getData.Id);
            }
            catch (Exception exception) { result.SetStatus(false).SetErrorMessage(exception.Message); }
            return result;
        }

    }
}
