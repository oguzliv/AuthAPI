﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Service.EmailService
{
    public interface IEmailService
    {
        Task SendEmail(Message message);
    }
}