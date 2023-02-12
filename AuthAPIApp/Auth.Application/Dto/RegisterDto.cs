using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Dto
{
    public class RegisterDto:UserDto
    {
        public string VerificationToken { get; set; }
    }
}
