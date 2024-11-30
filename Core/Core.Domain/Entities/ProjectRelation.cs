using Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities
{
    public class ProjectRelation : BaseEntity
    {
        public string ProjectType { get; set; }
        public Guid AssignedUserId { get; set; }
        public User AssignedUser { get; set; }
        public Guid ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
