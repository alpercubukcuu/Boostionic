using AutoMapper;
using Core.Application.Dtos;
using Core.Application.Dtos.ResultDtos;
using Core.Application.Enums;
using Core.Application.Features.Commands.UserRegisterCodeCommands.Commands;
using Core.Application.Interfaces.Dtos;
using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Core.Application.Features.Commands.UserRegisterCodeCommands.Handlers
{
    public class AddUserRegisterCodeHandler : BaseCommandHandler, IRequestHandler<AddUserRegisterCodeCommand, IResultDataDto<UserRegisterCodeDto>>
    {
        private readonly IMapper _mapper;
        private readonly ILogRepository _logRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserRegisterCodeRepository _userRegisterCodeRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AddUserRegisterCodeHandler(
            IMapper mapper,
            ILogRepository logRepository,
            IUserRepository userRepository,
            IHttpContextAccessor httpContextAccessor,
            IUserRegisterCodeRepository userRegisterCodeRepository)
            : base(userRepository, logRepository)
        {
            _mapper = mapper;
            _logRepository = logRepository;
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
            _userRegisterCodeRepository = userRegisterCodeRepository;
        }

        public async Task<IResultDataDto<UserRegisterCodeDto>> Handle(AddUserRegisterCodeCommand request, CancellationToken cancellationToken)
        {
            IResultDataDto<UserRegisterCodeDto> result = new ResultDataDto<UserRegisterCodeDto>();

            try
            {
                if (request.UserId == Guid.Empty)
                {
                    return result.SetStatus(false).SetErrorMessage("UserId is null").SetMessage("Invalid User ID.");
                }


                await _userRegisterCodeRepository.UpdateMultipleEntitiesAsync(x => x.IsEnable == true, entity => { entity.IsEnable = false; });

                UserRegisterCode userRegisterCode = new UserRegisterCode
                {
                    Id = Guid.NewGuid(),
                    ExpireDate = DateTime.Now.AddMinutes(10),
                    CreatedDate = DateTime.Now,
                    IsEnable = true,
                    RegisterCode = new Random().Next(100000, 999999).ToString("D6"),
                    Ip = _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? "::1",
                    UserId = request.UserId
                };

                var addResult = await _userRegisterCodeRepository.AddAsync(userRegisterCode);

                var resultMap = _mapper.Map<UserRegisterCodeDto>(addResult);

                result.SetStatus().SetMessage("UserRegisterCode created successfully").SetData(resultMap);

                await AddUserLog("UserRegisterCode Create Handler", "UserRegisterCode", userRegisterCode.Id, TransactionEnum.Create, userRegisterCode.Id);
            }
            catch (Exception exception)
            {
                result.SetStatus(false).SetErrorMessage(exception.Message).SetMessage("An error occurred while creating UserRegisterCode.");
            }

            return result;
        }
    }
}
