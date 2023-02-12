using Auth.DataAcces.Persistence.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.DataAcces.Repository
{
    public interface IUserRepository:IBaseRepository<User>
    {
        Task<User> GetByEmail(string email);
        Task<User> GetByToken(string token);
        Task<IEnumerable<User>> GetOnlineUsers();
        Task<IEnumerable<User>> GetNotVerified();
        Task<object> GetLoginTime();

    }
}
