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
            try{
                var _user = await _userRepository.GetByEmail(user.Email);

                if (_user == null)
                {
                    _user = _mapper.Map<User>(user);
                    _user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);
                    _user.IsAdmin = user.IsAdmin ? true : false;
                    _user.IsVerified = false;
                    _user.Id = Guid.NewGuid();
                    _user.CreatedAt = DateTime.UtcNow;
                   

                    if(await _userRepository.Create(_user) != null)
                    {
                        return new RegisterResponse()
                        {
                            Success= true,
                            Email = user.Email,
                            Id = _user.Id,
                            ErrorMessage = ""
                        };
                    }
                    else
                    {
                        return new ResponseDto()
                        {
                            Success = true,
                            Data = null,
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

        public async Task<object> Verify(Guid id)
        {
            try
            {
                User user = await _userRepository.GetById(id);
                if (user != null)
                {
                    user.IsVerified = true;
                    await _userRepository.Update(user);
                    return new BaseDto()
                    {
                        Success = true,
                    }; 
                }
                else if (user.IsVerified)
                {
                    return new BaseDto()
                    {
                        Success = true,
                        ErrorMessage = "User verified already"
                    };
                }
                else
                {
                    return new BaseDto()
                    {
                        Success = false,
                        ErrorMessage = "User is not found"
                    }; ;
                }
            }
            catch(Exception ex)
            {
                return new BaseDto()
                {
                    Success = false,
                    ErrorMessage = ex.Message
                }; ;
            }
        }
    }
}
