using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Dtos.SettingDtos
{
    public class SettingDto
    {
        public UserDto UserDto { get; set; }
        public List<WorkspaceDto> WorkspaceDtos { get; set; }
    }
}
