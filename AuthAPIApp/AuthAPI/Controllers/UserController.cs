using Auth.Application.Dto;
using Auth.Application.Dto.Request;
using Auth.Application.Service.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthAPI.Controllers
{
    [Route("users")]
    [ApiController] 
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        public UserController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }
        [HttpGet("online")]
        public async Task<object> GetOnline()
        {
            try
            {
                var users = await _userService.GetOnlineUsers();
                if(users == null)
                {
                    return NotFound();
                }
                else
                {

                    return Ok(users);
                }
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("not_verified")]
        public async Task<object> GetNotVerified()
        {
            try
            {
                var users = await _userService.GetNotVerified();
                if (users == null)
                {
                    return NotFound();
                }
                else
                {

                    return Ok(users);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("login_duration")]
        public async Task<object> GetLoginDuration()
        {
            var loginTimes = await _userService.GetLoginTime();
            return Ok(loginTimes);
        }
        [HttpGet]
        public async Task<object> Get()
        {
            try
            {
                var users = await _userService.Get();
                if (users == null)
                {
                    return BadRequest();
                }
                else
                {
                    return users;
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<object> Get([FromRoute] Guid id)
        {
            try
            {
                var user = await _userService.GetUserById(id);
                if (user == null)
                    return NotFound($"{id} user not found");
                else
                    return Ok(user);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<object> Update([FromRoute] Guid id, [FromBody] UpdateUserModel userDto)
        {
            try
            {
                var user = await _userService.Update(id, userDto);
                if (user == null)
                {
                    return NotFound($"{id} user not exist!");
                }
                else
                {
                    return Ok(user);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<object> Delete(Guid id)
        {
            try
            {
                var isDeleted = await _userService.Delete(id);
                if (isDeleted == false)
                {
                    return NotFound($"{id} user not exist!");
                }
                else
                {
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
