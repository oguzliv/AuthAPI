using Auth.Application.Dto;
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
            try{
                var _user = await _userRepository.GetByEmail(user.Email);

                if (_user == null)
                {
                    _user = _mapper.Map<User>(user);
                    _user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);
                    _user.IsAdmin = user.IsAdmin ? true : false;
                    _user.IsVerified = false;
                    _user.IsVerificationMailSent = false;
                    _user.Id = Guid.NewGuid();
                    _user.CreatedAt = DateTime.UtcNow;

                    if(await _userRepository.Create(_user) != null)
                    {
                        return new ResponseDto()
                        {
                            Success= true,
                            ErrorMessage = ""
                        };
                    }
                    else
                    {
                        return new ResponseDto()
                        {
                            Success = true,
                            ErrorMessage = "Could not created user."
                        };
                    }
                }
                else
                {
                    return new ResponseDto()
                    {
                        Success = false,
                        ErrorMessage = "User Exists."
                    };
                }
            }
            catch(Exception ex)
            {
                return new ResponseDto()
                {
                    Success = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        public async Task<object> Login(LoginDto user)

        {
            throw new NotImplementedException();
        }
    }
}
