using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Dto.Request
{
    public class UpdateUserModel:BaseModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
