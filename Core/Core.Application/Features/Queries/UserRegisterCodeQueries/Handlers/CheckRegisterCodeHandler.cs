using AutoMapper;
using Core.Application.Dtos.ResultDtos;
using Core.Application.Dtos;
using Core.Application.Interfaces.Dtos;
using Core.Application.Interfaces.Repositories;
using MediatR;
using Core.Application.Features.Queries.UserRegisterCodeQueries.Queries;

namespace Core.Application.Features.Queries.UserRegisterCodeQueries.Handlers
{
    public class CheckResetCodeHandler : IRequestHandler<CheckRegisterCodeQuery, IResultDataDto<UserRegisterCodeDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUserRegisterCodeRepository _userRegisterCodeRepository;
        public CheckResetCodeHandler(IMapper mapper, IUserRegisterCodeRepository userRegisterCodeRepository)
        {
            _mapper = mapper;
            _userRegisterCodeRepository = userRegisterCodeRepository;
        }

        public async Task<IResultDataDto<UserRegisterCodeDto>> Handle(CheckRegisterCodeQuery request, CancellationToken cancellationToken)
        {
            IResultDataDto<UserRegisterCodeDto> result = new ResultDataDto<UserRegisterCodeDto>();
            try
            {
                var repoResult = _userRegisterCodeRepository.GetSingle(predicate: d => d.IsEnable == true && d.RegisterCode == request.RegisterCode && d.UserId == request.UserId);
                if (repoResult == null) result.SetStatus(false).SetErrorMessage("Not Found Data").SetMessage("Reset Code does not match !");

                bool checkDate = repoResult != null && DateTime.Now <= repoResult.ExpireDate;
                if(!checkDate) result.SetStatus(false).SetErrorMessage("Not Found Data").SetMessage("Code has expired. Please get a new code");

                var map = _mapper.Map<UserRegisterCodeDto>(repoResult);
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
