using Core.Application.Dtos.CommonDtos;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Dtos
{
    public class ProjectStageDto : BaseDto
    {
        public string Name { get; set; }
        public Guid ProjectId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime DeadlineDateTime { get; set; }
        public byte Status { get; set; }
        public string Description { get; set; }
        public ProjectDto Project { get; set; }
        public ICollection<ProjectTaskDto> ProjectTasks { get; set; }
        public bool IsCritical { get; set; }
        public int Order { get; set; }
        public int Progress { get; set; }
    }
}
