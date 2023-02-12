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
        public async Task<IEnumerable<User>> GetOnlineUsers()
        {
            var onlineUsers = await _db.Users.Where((user => (DateTime.UtcNow - user.VerifiedAt).Hours < 24)).ToListAsync();
            return onlineUsers;
        }
        public async Task<User> GetByEmail(string email)
        {
            var user = await _db.Users.FirstOrDefaultAsync(user => user.Email == email);
            if (user == null) { return null; }
            else return user;

        }

        public async Task<User> GetByToken(string token)
        {
            var user = await _db.Users.FirstOrDefaultAsync(user => user.VerificationToken == token);
            if (user == null) { return null; }
            else return user;
        }

        public async Task<IEnumerable<User>> GetNotVerified()
        {
            var onlineUsers = await _db.Users
                .Where((user => (DateTime.UtcNow - user.CreatedAt).Hours > 24 && user.IsVerified == false))
                .ToListAsync();
            return onlineUsers;
        }
        public async Task<object> GetLoginTime()
        {
            double totalTime = 0;
            var loginTimes = await _db.Users.
                Where(user => user.IsVerified == true).
                Select(user => new { (user.VerifiedAt - user.CreatedAt).Minutes }).
                ToListAsync();

            foreach(var t in loginTimes)
            {
                totalTime += t.Minutes;
            }

             return totalTime/loginTimes.Count/60;

        }
    }
}
