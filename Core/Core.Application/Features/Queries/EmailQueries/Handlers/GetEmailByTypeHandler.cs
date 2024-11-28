using AutoMapper;
using Core.Application.Dtos.EmailDtos;
using Core.Application.Dtos.ResultDtos;
using Core.Application.Features.Queries.UserQueries.Queries;
using Core.Application.Interfaces.Dtos;
using Core.Application.Interfaces.Repositories;
using MediatR;


namespace Core.Application.Features.Queries.BusinessPlaceQueries.Handlers
{
    public class GetEmailByTypeHandler : IRequestHandler<GetEmailByTypeQuery, IResultDataDto<EmailDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IEmailRepository _emailRepository;
        public GetEmailByTypeHandler(IMapper mapper, IUserRepository userRepository, IEmailRepository emailRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _emailRepository = emailRepository;
        }

        public async Task<IResultDataDto<EmailDto>> Handle(GetEmailByTypeQuery request, CancellationToken cancellationToken)
        {
            IResultDataDto<EmailDto> result = new ResultDataDto<EmailDto>();
            try
            {
                var repoResult = _emailRepository.GetSingle(predicate: d => d.IsEnable == true && d.EmailType == request.EmailType);
                if (repoResult == null) result.SetStatus(false).SetErrorMessage("Not Found Data").SetMessage("Email not exist!");
               
                var map = _mapper.Map<EmailDto>(repoResult);
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
