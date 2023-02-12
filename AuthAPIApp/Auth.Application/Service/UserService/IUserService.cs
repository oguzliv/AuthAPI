using Auth.Application.Dto.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Service.UserService
{
    public interface IUserService
    {
        Task<object> Create(RegisterDto user);
        Task<object> ResetPassword(ResetPasswordDto newPassword);  
        Task<object> Verify(string token);
        Task<object> GetUserByEmail(string email);
    }
}
