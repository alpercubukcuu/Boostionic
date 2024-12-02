using AutoMapper;
using Core.Application.Dtos;
using Core.Application.Dtos.ResultDtos;
using Core.Application.Features.Queries.SetupSettingQueries.Queries;
using Core.Application.Interfaces.Dtos;
using Core.Application.Interfaces.Repositories;
using MediatR;


namespace Core.Application.Features.Queries.SetupSettingQueries.Handlers
{
    public class GetByIdSetupSettingHandler : IRequestHandler<GetByIdSetupSettingQuery, IResultDataDto<SetupSettingDto>>
    {
        private readonly IMapper _mapper;
        private readonly ISetupSettingRepository _setupSettingRepository;
        public GetByIdSetupSettingHandler(IMapper mapper, ISetupSettingRepository setupSettingRepository)
        {
            _mapper = mapper;
            _setupSettingRepository = setupSettingRepository;
        }

        public async Task<IResultDataDto<SetupSettingDto>> Handle(GetByIdSetupSettingQuery request, CancellationToken cancellationToken)
        {
            IResultDataDto<SetupSettingDto> result = new ResultDataDto<SetupSettingDto>();
            try
            {
                var repoResult = _setupSettingRepository.GetSingle(predicate: d => d.IsEnable == true && d.Id == request.Id);
                if (repoResult == null) result.SetStatus(false).SetErrorMessage("Not Found Data").SetMessage("No data found for the ID value");
                var map = _mapper.Map<SetupSettingDto>(repoResult);
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
