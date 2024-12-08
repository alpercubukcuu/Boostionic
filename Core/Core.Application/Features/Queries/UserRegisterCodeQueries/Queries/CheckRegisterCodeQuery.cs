using Core.Application.Dtos;
using Core.Application.Interfaces.Dtos;
using MediatR;


namespace Core.Application.Features.Queries.UserRegisterCodeQueries.Queries
{
    public class CheckRegisterCodeQuery : UserRegisterCodeDto, IRequest<IResultDataDto<UserRegisterCodeDto>>
    {
        public string RegisterCode { get; set; }
        public Guid UserId { get; set; }
    }
}
