using Auth.Application.Dto;
using Auth.Application.Dto.Request;
using Auth.Application.Dto.Response;
using Auth.Application.Helper;
using Auth.Application.Service.EmailService;
using Auth.Application.Service.UserService;
using Auth.DataAcces.Persistence.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthAPI.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;

        public AuthController(IUserService userService, IEmailService emailService, IConfiguration configuration)
        {
            _userService = userService;
            _emailService = emailService;
            _configuration = configuration;
        }
        [HttpPost("register")]
        public async Task<object> Register([FromBody] RegisterModel user)
        {
            try
            {
                RegisterDto _user = (RegisterDto)await _userService.Create(user);
                if(_user != null)
                {
                    //Email Service!!
                    var root = _configuration.GetSection("Root:BaseUrl").Value;
                    var message = new Message(
                        new string[] { user.Email },
                        "Verification email",
                        "Verification token is : " + _user.VerificationToken
                        ) ;

                    await _emailService.SendEmail(message);

                    //_emailService.SendEmail(message);
                    return Ok(_user);
                }
                else
                {
                    return BadRequest(_user);

                }
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("{token}")]
        public async Task<object> Verify([FromBody] string token)
        {

            try
            {
                var response = await _userService.Verify(token);
                if (response != null)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }
        [HttpPost("login")]
        public async Task<object> Login([FromBody] LoginModel user)
        {
            try
            {
                User UserInDb = (User)await _userService.GetUserByEmail(user.Email);
                if (UserInDb == null || !BCrypt.Net.BCrypt.Verify(user.Password, UserInDb.PasswordHash))
                {
                    return NotFound("User not found, invalid email or password");
                }
                else if(UserInDb.IsVerified == false)
                {
                    return BadRequest("User is not verified");
                }
                else
                {
                    return Ok(JWTTokenCreator.TokenCreator(UserInDb, _configuration));
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPatch()]
        public async Task<object> ResetPassword([FromBody] ResetPasswordModel newPassword)
        {
            try
            {
                BaseResponseDto response = (BaseResponseDto)await _userService.ResetPassword(newPassword);
                if (response.Success)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response);
                }
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
