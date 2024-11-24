using Core.Application.Dtos;
using Core.Application.Interfaces.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.Queries.BusinessPlaceQueries.Queries
{
    public class GetByIdBusinessPlaceQuery : IRequest<IResultDataDto<BusinessPlaceDto>>
    {
        public Guid Id { get; set; }
    }
}
