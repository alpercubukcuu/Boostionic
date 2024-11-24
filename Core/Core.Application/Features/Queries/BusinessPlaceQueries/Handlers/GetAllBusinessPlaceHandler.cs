using AutoMapper;
using Core.Application.Dtos;
using Core.Application.Dtos.ResultDtos;
using Core.Application.Features.Queries.BusinessPlaceQueries.Queries;
using Core.Application.Interfaces.Dtos;
using Core.Application.Interfaces.Repositories;
using MediatR;


namespace Core.Application.Features.Queries.BusinessPlaceQueries.Handlers
{
    public class GetAllBusinessPlaceHandler : IRequestHandler<GetAllBusinessPlaceQuery, IResultDataDto<List<BusinessPlaceDto>>>
    {
        private readonly IMapper _mapper;
        private readonly IBusinessPlaceRepository _businessPlaceRepository;
        public GetAllBusinessPlaceHandler(IMapper mapper, IBusinessPlaceRepository businessPlaceRepository)
        {
            _mapper = mapper;
            _businessPlaceRepository = businessPlaceRepository;
        }

        public async Task<IResultDataDto<List<BusinessPlaceDto>>> Handle(GetAllBusinessPlaceQuery request, CancellationToken cancellationToken)
        {
            IResultDataDto<List<BusinessPlaceDto>> result = new ResultDataDto<List<BusinessPlaceDto>>();
            try
            {

                var repoResult = _businessPlaceRepository.GetAll(predicate: d => d.IsEnable != true);
                if (repoResult.Count() <= 0) result.SetStatus(false).SetErrorMessage("Not Found Data").SetMessage("The list is empty!");
                var map = _mapper.Map<List<BusinessPlaceDto>>(repoResult);
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
