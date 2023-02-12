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
        public async Task<object> Register([FromBody] RegisterDto user)
        {
            try
            {
                RegisterResponse response = (RegisterResponse)await _userService.Create(user);
                if(response.Success)
                {
                    //Email Service!!
                    var root = _configuration.GetSection("Root:BaseUrl").Value;
                    var message = new Message(
                        new string[] { user.Email },
                        "Verification email",
                        "Click for verification :\n" + root + "/auth/" + response.Id
                        ) ;

                    await _emailService.SendEmail(message);

                    //_emailService.SendEmail(message);
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
        [HttpGet("{id}")]
        public async Task<object> Verify([FromRoute] Guid id)
        {

            //return 0;
            try
            {
                BaseDto response = (BaseDto)await _userService.Verify(id);
                if (response.Success)
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
        public async Task<object> Login([FromBody] LoginDto user)
        {
            try
            {
                User UserInDb = (User)await _userService.Login(user);
                if (UserInDb == null)
                {
                    return NotFound("User not found, invalid email or password");
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
    }
}
