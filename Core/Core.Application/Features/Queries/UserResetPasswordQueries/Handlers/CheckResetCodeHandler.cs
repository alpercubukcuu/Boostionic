using AutoMapper;
using Core.Application.Dtos.ResultDtos;
using Core.Application.Dtos;
using Core.Application.Interfaces.Dtos;
using Core.Application.Interfaces.Repositories;
using MediatR;
using Core.Application.Features.Queries.UserResetPasswordQueries.Queries;

namespace Core.Application.Features.Queries.UserResetPasswordQueries.Handlers
{
    public class CheckResetCodeHandler : IRequestHandler<CheckResetCodeQuery, IResultDataDto<UserResetPasswordDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUserResetPasswordRepository _userResetPasswordRepository;
        public CheckResetCodeHandler(IMapper mapper, IUserResetPasswordRepository userResetPasswordRepository)
        {
            _mapper = mapper;
            _userResetPasswordRepository = userResetPasswordRepository;
        }

        public async Task<IResultDataDto<UserResetPasswordDto>> Handle(CheckResetCodeQuery request, CancellationToken cancellationToken)
        {
            IResultDataDto<UserResetPasswordDto> result = new ResultDataDto<UserResetPasswordDto>();
            try
            {
                var repoResult = _userResetPasswordRepository.GetSingle(predicate: d => d.IsEnable == true && d.ResetCode == request.ResetCode && d.Id == request.UserId);
                if (repoResult == null) result.SetStatus(false).SetErrorMessage("Not Found Data").SetMessage("Reset Code does not match !");

                bool checkDate = repoResult != null && DateTime.Now <= repoResult.ExpireDate;
                if(!checkDate) result.SetStatus(false).SetErrorMessage("Not Found Data").SetMessage("Code has expired. Please get a new code");

                var map = _mapper.Map<UserResetPasswordDto>(repoResult);
                result.SetData(map);
                return result;
            }
            catch (Exception exception)
            {
                return result.SetStatus(false).SetErrorMessage(exception.Message);
            }

        }
    }
}
