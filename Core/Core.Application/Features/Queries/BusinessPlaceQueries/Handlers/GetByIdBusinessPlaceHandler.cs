using AutoMapper;
using Core.Application.Dtos;
using Core.Application.Dtos.ResultDtos;
using Core.Application.Features.Queries.BusinessPlaceQueries.Queries;
using Core.Application.Interfaces.Dtos;
using Core.Application.Interfaces.Repositories;
using MediatR;


namespace Core.Application.Features.Queries.BusinessPlaceQueries.Handlers
{
    public class GetByIdBusinessPlaceHandler : IRequestHandler<GetByIdBusinessPlaceQuery, IResultDataDto<BusinessPlaceDto>>
    {
        private readonly IMapper _mapper;
        private readonly IBusinessPlaceRepository _businessPlaceRepository;
        public GetByIdBusinessPlaceHandler(IMapper mapper, IBusinessPlaceRepository businessPlaceRepository)
        {
            _mapper = mapper;
            _businessPlaceRepository = businessPlaceRepository;
        }

        public async Task<IResultDataDto<BusinessPlaceDto>> Handle(GetByIdBusinessPlaceQuery request, CancellationToken cancellationToken)
        {
            IResultDataDto<BusinessPlaceDto> result = new ResultDataDto<BusinessPlaceDto>();
            try
            {
                var repoResult = _businessPlaceRepository.GetSingle(predicate: d => d.IsEnable == true && d.Id == request.Id);
                if (repoResult == null) result.SetStatus(false).SetErrorMessage("Not Found Data").SetMessage("No data found for the ID value");
                var map = _mapper.Map<BusinessPlaceDto>(repoResult);
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
