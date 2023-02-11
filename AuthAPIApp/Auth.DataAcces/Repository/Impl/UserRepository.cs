using Auth.DataAcces.Persistence;
using Auth.DataAcces.Persistence.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.DataAcces.Repository.Impl
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext db) : base(db) { }
        public async Task<User> GetOnlineUsers()
        {
            throw new NotImplementedException();
        }
        public async Task<User> GetByEmail(string email)
        {
            var user = await _db.Users.FirstOrDefaultAsync(user => user.Email == email);
            if (user == null) { return null; }
            else return user;

        }
    }
}
