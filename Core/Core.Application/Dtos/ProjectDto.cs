using Core.Application.Dtos.CommonDtos;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Dtos
{
    public class ProjectDto : BaseDto
    {
        public string ProjectName { get; set; }
        public DateTime DeadlineDateTime { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public byte ProjectStatus { get; set; }
        public string Description { get; set; }

        public Guid ClientId { get; set; }
        public ClientDto Client { get; set; }

        public Guid WorkspaceId { get; set; }
        public WorkspaceDto Workspace { get; set; }


        public ICollection<ProjectStageDto> ProjectStages { get; set; }
        public ICollection<ProjectTaskDto> ProjectTasks { get; set; }
        public ICollection<ProjectCategoryDto> ProjectCategories { get; set; }
    }
}
