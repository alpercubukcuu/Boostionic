using AutoMapper;
using Core.Application.Dtos;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Mappings
{
    public class OwnerEntityProfile : Profile
    {
        public OwnerEntityProfile()
        {
            CreateMap<OwnerEntity, OwnerEntityDto>().ReverseMap();
            CreateMap<OwnerEntityDto, OwnerEntity>().ReverseMap();
        }
    }
}
