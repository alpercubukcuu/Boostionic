using Core.Application.Dtos.EmailDtos;
using Core.Application.Interfaces.Dtos;
using MediatR;

namespace Core.Application.Features.Queries.EmailQueries.Queries
{
    public class GetEmailByTypeQuery : EmailDto, IRequest<IResultDataDto<EmailDto>>
    {       
    }
}
