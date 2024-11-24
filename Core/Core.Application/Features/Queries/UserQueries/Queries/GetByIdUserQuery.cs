using Core.Application.Dtos;
using Core.Application.Interfaces.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.Queries.UserQueries.Queries
{
    public class GetByIdUserQuery : IRequest<IResultDataDto<UserDto>>
    {
        public Guid Id { get; set; }
    }
}
