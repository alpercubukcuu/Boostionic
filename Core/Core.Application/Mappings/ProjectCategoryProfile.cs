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
    public class ProjectCategoryProfile : Profile
    {
        public ProjectCategoryProfile()
        {
            CreateMap<ProjectCategory, ProjectCategoryDto>().ReverseMap();
            CreateMap<ProjectCategoryDto, ProjectCategory>().ReverseMap();
        }
    }
}
