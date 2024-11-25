using AutoMapper;
using Core.Application.Dtos;
using Core.Application.Dtos.ResultDtos;
using Core.Application.Features.Queries.UserQueries.Queries;
using Core.Application.Interfaces.Dtos;
using Core.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Core.Application.Features.Queries.BusinessPlaceQueries.Handlers
{
    public class GetByEmailUserHandler : IRequestHandler<GetByEmailUserQuery, IResultDataDto<UserDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        public GetByEmailUserHandler(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<IResultDataDto<UserDto>> Handle(GetByEmailUserQuery request, CancellationToken cancellationToken)
        {
            IResultDataDto<UserDto> result = new ResultDataDto<UserDto>();
            try
            {
                var repoResult = _userRepository.GetSingle(predicate: d => d.IsEnable == true && d.Email == request.Email, include: d => d.Include(c => c.Company).Include(r=>r.UserRole));
                if (repoResult == null) result.SetStatus(false).SetErrorMessage("Not Found Data").SetMessage("Your account was not found! If you think there is a mistake, please contact the support team.");
                if(repoResult.IsEnable == false) result.SetStatus(false).SetErrorMessage("Not Found Data").SetMessage("Your account is not enable. Please contact the support team.");
                var map = _mapper.Map<UserDto>(repoResult);
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
