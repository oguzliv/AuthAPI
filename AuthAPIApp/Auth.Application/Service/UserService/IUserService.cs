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
        Task<object> Create(RegisterModel user);
        Task<object> ResetPassword(ResetPasswordModel newPassword);  
        Task<object> Verify(string token);
        Task<object> GetUserByEmail(string email);
        Task<object> GetUserById(Guid id);
        Task<object> Get();
        Task<object> Update(Guid id, UpdateUserModel user);
        Task<bool> Delete(Guid id);
        Task<object> GetOnlineUsers();
        Task<object> GetNotVerified();
        Task<object> GetLoginTime();
    }
}
