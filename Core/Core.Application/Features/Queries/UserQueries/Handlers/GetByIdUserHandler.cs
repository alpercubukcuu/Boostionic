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
    public class GetByIdUserHandler : IRequestHandler<GetByIdUserQuery, IResultDataDto<UserDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        public GetByIdUserHandler(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<IResultDataDto<UserDto>> Handle(GetByIdUserQuery request, CancellationToken cancellationToken)
        {
            IResultDataDto<UserDto> result = new ResultDataDto<UserDto>();
            try
            {
                var repoResult = _userRepository.GetSingle(predicate: d => d.IsEnable == true && d.Id == request.Id, include: i => i.Include(i => i.UserRole).Include(i => i.Owner).Include(i => i.Client));
                if (repoResult == null) result.SetStatus(false).SetErrorMessage("Not Found Data").SetMessage("No data found for the ID value");
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
