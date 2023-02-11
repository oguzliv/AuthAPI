using Auth.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.DataAcces.Repository
{
    public interface IUserRepository:IBaseRepository<User>
    {
        Task<User> GetOnlineUsers();

    }
}
