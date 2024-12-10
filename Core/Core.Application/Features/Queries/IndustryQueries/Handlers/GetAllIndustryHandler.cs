using AutoMapper;
using Core.Application.Dtos;
using Core.Application.Dtos.ResultDtos;
using Core.Application.Features.Queries.IndustryQueries.Queries;
using Core.Application.Interfaces.Dtos;
using Core.Application.Interfaces.Repositories;
using MediatR;

namespace Core.Application.Features.Queries.IndustryQueries.Handlers;

public class GetAllIndustryHandler : IRequestHandler<GetAllIndustryQuery, IResultDataDto<List<IndustryDto>>>
{
    private readonly IMapper _mapper;
    private readonly IIndustryRepository _industryRepository;

    public GetAllIndustryHandler(IMapper mapper, IIndustryRepository industryRepository)
    {
        _mapper = mapper;
        _industryRepository = industryRepository;
    }

    public async Task<IResultDataDto<List<IndustryDto>>> Handle(GetAllIndustryQuery request,
        CancellationToken cancellationToken)
    {
        IResultDataDto<List<IndustryDto>> result = new ResultDataDto<List<IndustryDto>>();
        try
        {
            var repoResult = _industryRepository.GetAll(predicate: d => d.IsEnable == true);
            if (repoResult.Count() <= 0)
                result.SetStatus(false).SetErrorMessage("Not Found Data").SetMessage("The list is empty!");
            var map = _mapper.Map<List<IndustryDto>>(repoResult);
            result.SetData(map);
            return result;
        }
        catch (Exception exception)
        {
            return result.SetStatus(false).SetErrorMessage(exception.Message);
        }
    }
}