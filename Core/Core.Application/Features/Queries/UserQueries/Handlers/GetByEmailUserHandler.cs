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
                var repoResult = _userRepository.GetSingle(predicate: d => d.IsEnable == true && d.Email == request.Email, include: d => d.Include(c => c.Company).Include(r => r.UserRole));

                if (repoResult == null) return result.SetStatus(false).SetErrorMessage("Your account was not found! If you think there is a mistake, please contact the support team.").SetMessage("Not Found Data");

                if (repoResult.IsEnable == false) return result.SetStatus(false).SetErrorMessage("Your account is not enabled. Please contact the support team.").SetMessage("Not Found Data");
                
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
