using Core.Application.Dtos.CommonDtos;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Dtos
{
    public class ProjectCategoryDto : BaseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<ProjectDto> Projects { get; set; }

        public int DisplayOrder { get; set; }
    }
}
