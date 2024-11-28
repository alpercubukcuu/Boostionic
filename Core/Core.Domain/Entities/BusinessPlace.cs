using Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities
{
    public class BusinessPlace : OwnersEntity
    {
        public string JsonData { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }

    }
}
