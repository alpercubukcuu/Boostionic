using AutoMapper;
using Core.Application.Dtos;
using Core.Application.Dtos.ResultDtos;
using Core.Application.Features.Queries.UserQueries.Queries;
using Core.Application.Interfaces.Dtos;
using Core.Application.Interfaces.Repositories;
using MediatR;


namespace Core.Application.Features.Queries.UserQueries.Handlers
{
    public class GetAllUserHandler : IRequestHandler<GetAllUserQuery, IResultDataDto<List<UserDto>>>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        public GetAllUserHandler(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<IResultDataDto<List<UserDto>>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        {
            IResultDataDto<List<UserDto>> result = new ResultDataDto<List<UserDto>>();
            try
            {

                var repoResult = _userRepository.GetAll(predicate: d => d.IsEnable != true);
                if (repoResult.Count() <= 0) result.SetStatus(false).SetErrorMessage("Not Found Data").SetMessage("The list is empty!");
                var map = _mapper.Map<List<UserDto>>(repoResult);
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
