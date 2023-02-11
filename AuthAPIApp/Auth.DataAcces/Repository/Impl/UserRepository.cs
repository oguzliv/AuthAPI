using Auth.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.DataAcces.Repository.Impl
{
    public class UserRepository : IUserRepository
    {
        public Task<User> Create(User entity)
        {
            throw new NotImplementedException();
        }

        public Task<User> Delete(User entity)
        {
            throw new NotImplementedException();
        }

        public Task<User> Get()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetById(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetOnlineUsers()
        {
            throw new NotImplementedException();
        }

        public Task<User> Update(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
