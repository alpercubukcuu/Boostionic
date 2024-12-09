using Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities
{
    public class RelationUserToWorkspace : BaseEntity
    {
        public  bool IsOwner { get; set; }
        public byte UserType { get; set; }
        public Guid UserRelationId { get; set; }
        public Guid WorkspaceId { get; set; }
        public Workspace Workspace { get; set; }

    }
}
