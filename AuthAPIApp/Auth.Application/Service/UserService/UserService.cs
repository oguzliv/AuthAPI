using Auth.Application.Dto;
using Auth.Application.Dto.Request;
using Auth.Application.Dto.Response;
using Auth.DataAcces.Persistence.Entity;
using Auth.DataAcces.Repository;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Service.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository= userRepository;
            _mapper= mapper;
        }
        public async Task<object> Create(RegisterModel user)
        {
            var _user = await _userRepository.GetByEmail(user.Email);

            if (_user == null)
            {
                _user = _mapper.Map<User>(user);
                _user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);
                _user.IsAdmin = user.IsAdmin ? true : false;
                _user.IsVerified = false;
                _user.Id = Guid.NewGuid();
                _user.CreatedAt = DateTime.UtcNow;
                _user.VerificationToken = TokenGenerator();


                if (await _userRepository.Create(_user) != null)
                {
                    return _mapper.Map<RegisterDto>(_user);
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return null;
            }
        }

        public async Task<object> GetUserByEmail(string email)
        {
            var _user = await _userRepository.GetByEmail(email);
            return _user;
        }

        public async Task<object> ResetPassword(ResetPasswordModel newPassword)
        {
            var _user = await _userRepository.GetByEmail(newPassword.Email);

            if (_user != null)
            {
                _user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword.Password);
                await _userRepository.Update(_user);
                return _mapper.Map<UserDto>(_user);
            }
            else
            {
                return false;
            }
        }

        public async Task<object> Verify(string token)
        {
            User user = await _userRepository.GetByToken(token);
            if (user != null)
            {
                user.IsVerified = true;
                user.VerifiedAt = DateTime.UtcNow;
                user.VerificationToken = "";
                await _userRepository.Update(user);
                return _mapper.Map<UserDto>(user);
            }
            else if (user.IsVerified)
            {
                return null;
            }
            else
            {
                return false;
            }
        }

        public async Task<object> GetUserById(Guid id)
        {
            var user = await _userRepository.GetById(id);
            return _mapper.Map<UserDto>(user);
        }

        public async Task<object> Get()
        {
            var users = await _userRepository.Get();
            return _mapper.Map<List<UserDto>>(users);
        }

        public async Task<object> Update(Guid id ,UpdateUserModel user)
        {
            var _user = await _userRepository.GetById(id);
            if (_user == null)
                return null;

            _user.Name = user.Name;
            _user.Surname = user.Surname;
            _user.Email = user.Email;
            _user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);

            await _userRepository.Update(_user);
            return _mapper.Map<UserDto>(_user);
        }

        public async Task<bool> Delete(Guid id)
        {
            User user = await _userRepository.GetById(id);
            if (user == null)
            {
                return false;
            }
            else
            {
                var removed = await _userRepository.Delete(user);
                return true;
            }
        }
        public async Task<object> GetOnlineUsers()
        {
           var users = await _userRepository.GetOnlineUsers();
           return _mapper.Map<List<UserDto>>(users); 
        }

        public async Task<object> GetNotVerified()
        {
            var users = await _userRepository.GetNotVerified();
            return _mapper.Map<List<UserDto>>(users);
        }

        public async Task<object> GetLoginTime()
        {
            var loginTimes = await _userRepository.GetLoginTime();
            return loginTimes;
        }

        private static string TokenGenerator()
        {
            Guid g = Guid.NewGuid();
            string GuidString = Convert.ToBase64String(g.ToByteArray());
            GuidString = GuidString.Replace("=", "");
            GuidString = GuidString.Replace("+", "");
            GuidString = GuidString.Replace("/", "");
            GuidString = GuidString.Replace(@"\", "");
            return GuidString;
        }

    }
}
