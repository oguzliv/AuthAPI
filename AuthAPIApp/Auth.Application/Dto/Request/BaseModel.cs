﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Dto.Request
{
    public class BaseModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
