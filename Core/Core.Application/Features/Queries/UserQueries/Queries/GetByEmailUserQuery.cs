using Core.Application.Dtos;
using Core.Application.Interfaces.Dtos;
using MediatR;


namespace Core.Application.Features.Queries.UserQueries.Queries
{
    public class GetByEmailUserQuery : IRequest<IResultDataDto<UserDto>>
    {
        public string Email { get; set; }
    }
}
