using AutoMapper;
using Core.Application.Dtos;
using Core.Application.Dtos.ResultDtos;
using Core.Application.Features.Queries.IndustryQueries.Queries;
using Core.Application.Interfaces.Dtos;
using Core.Application.Interfaces.Repositories;
using MediatR;

namespace Core.Application.Features.Queries.IndustryQueries.Handlers;

public class GetByIdIndustryHandler : IRequestHandler<GetByIdIndustryQuery, IResultDataDto<IndustryDto>>
{
    private readonly IMapper _mapper;
    private readonly IIndustryRepository _industryRepository;

    public GetByIdIndustryHandler(IMapper mapper, IIndustryRepository industryRepository)
    {
        _mapper = mapper;
        _industryRepository = industryRepository;
    }

    public async Task<IResultDataDto<IndustryDto>> Handle(GetByIdIndustryQuery request,
        CancellationToken cancellationToken)
    {
        IResultDataDto<IndustryDto> result = new ResultDataDto<IndustryDto>();
        try
        {
            var repoResult = _industryRepository.GetSingle(predicate: d => d.IsEnable == true && d.Id == request.Id);
            if (repoResult == null)
                result.SetStatus(false).SetErrorMessage("Not Found Data").SetMessage("No data found for the ID value");
            var map = _mapper.Map<IndustryDto>(repoResult);
            return result;
        }
        catch (Exception exception)
        {
            return result.SetStatus(false).SetErrorMessage(exception.Message);
        }
    }
}