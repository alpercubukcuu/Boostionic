using AutoMapper;
using Core.Application.Dtos;
using Core.Application.Dtos.ResultDtos;
using Core.Application.Enums;
using Core.Application.Features.Commands.UserCommands.Commands;
using Core.Application.Interfaces.Dtos;
using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Core.Application.Features.Commands.UserCommands.Handlers
{
    public class AddUserResetPasswordHandler : BaseCommandHandler, IRequestHandler<AddUserResetPasswordCommand, IResultDataDto<UserResetPasswordDto>>
    {
        private readonly IMapper _mapper;
        private readonly ILogRepository _logRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserResetPasswordRepository _userResetPasswordRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AddUserResetPasswordHandler(
            IMapper mapper,
            ILogRepository logRepository,
            IUserRepository userRepository,
            IUserResetPasswordRepository userResetPasswordRepository,
            IHttpContextAccessor httpContextAccessor)
            : base(userRepository, logRepository)
        {
            _mapper = mapper;
            _logRepository = logRepository;
            _userRepository = userRepository;
            _userResetPasswordRepository = userResetPasswordRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IResultDataDto<UserResetPasswordDto>> Handle(AddUserResetPasswordCommand request, CancellationToken cancellationToken)
        {
            IResultDataDto<UserResetPasswordDto> result = new ResultDataDto<UserResetPasswordDto>();

            try
            {
                if (request.UserId == Guid.Empty)
                {
                    return result.SetStatus(false).SetErrorMessage("UserId is null").SetMessage("Invalid User ID.");
                }


                await _userResetPasswordRepository.UpdateMultipleEntitiesAsync(x => x.IsEnable == true, entity => { entity.IsEnable = false; });

                UserResetPassword userResetPassword = new UserResetPassword
                {
                    Id = Guid.NewGuid(),
                    ExpireDate = DateTime.Now.AddMinutes(10),
                    CreatedDate = DateTime.Now,
                    IsEnable = true,
                    ResetCode = new Random().Next(100000, 999999).ToString("D6"),
                    Ip = _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? "::1",
                    UserId = request.UserId
                };

                var addResult = await _userResetPasswordRepository.AddAsync(userResetPassword);

                var resultMap = _mapper.Map<UserResetPasswordDto>(addResult);

                result.SetStatus().SetMessage("UserResetPassword created successfully").SetData(resultMap);

                await AddUserLog("UserResetPassword Create Handler", "UserResetPassword", userResetPassword.Id, TransectionEnum.Create, userResetPassword.Id);
            }
            catch (Exception exception)
            {
                result.SetStatus(false).SetErrorMessage(exception.Message).SetMessage("An error occurred while creating UserResetPassword.");
            }

            return result;
        }
    }
}
