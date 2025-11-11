using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadereraMancino.Application.Dtos.Identity.Roles
{
    public class RoleRequestDto
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
    }
}
