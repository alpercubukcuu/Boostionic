using Core.Application.Dtos;
using Core.Application.Interfaces.Dtos;
using MediatR;


namespace Core.Application.Features.Queries.UserResetPasswordQueries.Queries
{
    public class CheckResetCodeQuery : UserResetPasswordDto, IRequest<IResultDataDto<UserResetPasswordDto>>
    {
        public string ResetCode { get; set; }
        public Guid UserId { get; set; }
    }
}
