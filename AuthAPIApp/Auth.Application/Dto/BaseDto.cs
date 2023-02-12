using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Dto
{
    public class BaseDto
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
    }
}
