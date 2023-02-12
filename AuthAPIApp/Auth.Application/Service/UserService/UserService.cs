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
        public async Task<object> Create(RegisterDto user)
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
                    return new RegisterResponseDto()
                    {
                        Success = true,
                        Email = user.Email,
                        Token = _user.VerificationToken,
                        ErrorMessage = ""
                    };
                }
                else
                {
                    return new RegisterResponseDto()
                    {
                        Success = false,
                        ErrorMessage = "Could not created user."
                    };
                }
            }
            else
            {
                return new RegisterResponseDto()
                {
                    Success = false,
                    ErrorMessage = "User already exists."
                };
            }
        }

        public async Task<object> GetUserByEmail(string email)
        {
            var _user = await _userRepository.GetByEmail(email);
            return _user;
        }

        public async Task<object> ResetPassword(ResetPasswordDto newPassword)
        {
            var _user = await _userRepository.GetByEmail(newPassword.Email);

            if (_user != null)
            {
                _user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword.NewPassword);
                await _userRepository.Update(_user);
                return new BaseResponseDto() { Success = true };
            }
            else
            {
                return new BaseResponseDto() { Success = false, ErrorMessage="User not found"};
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
                return new BaseResponseDto()
                {
                    Success = true,
                };
            }
            else if (user.IsVerified)
            {
                return new BaseResponseDto()
                {
                    Success = false,
                    ErrorMessage = "User verified already"
                };
            }
            else
            {
                return new BaseResponseDto()
                {
                    Success = false,
                    ErrorMessage = "User is not found"
                }; ;
            }
        }
        private static string TokenGenerator()
        {
            Guid g = Guid.NewGuid();
            string GuidString = Convert.ToBase64String(g.ToByteArray());
            GuidString = GuidString.Replace("=", "");
            GuidString = GuidString.Replace("+", "");
            return GuidString;
        }
    }
}
