using FrozenCode.Note.Contract.DTO;
using FrozenCode.Note.Contract.Entities;
using FrozenCode.Note.Contract.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApi.Helpers;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
   
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        private readonly AppSettings _appSettings;

        public UsersController(IUserService userService, IOptions<AppSettings> appSettings)
        {
            _userService = userService;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")] //  /users/authenticate

        public IActionResult Authenticate([FromBody]UserDTO registerUser)
        {
            var authenticatedUser =_userService.Authenticate(ref registerUser);
            
            if (authenticatedUser == null)
                return BadRequest(new { message = "Username or password is incorrect" });
            
            return Ok(GenerateUserWithToken(new UserDTO {Id = authenticatedUser.Id, UserName = authenticatedUser.UserName }));
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("RegisterUser")]//  api/users/RegisterUser

        public IActionResult RegisterUser([FromBody]RegisterUserDTO userParam)
        {
            string message = string.Empty;

            var isRegistered = _userService.TryRegister(ref userParam);

            if (!isRegistered)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(true);
        }

       
        [HttpGet]
        [Route("GetAllUsers")] //Candidates/GetAllUser
        public IActionResult GetAll()
        {
            var users =  _userService.GetAll();
            return Ok(users.Result);
        }

        private UserDTO GenerateUserWithToken(UserDTO registeredUser)
        {
            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, registeredUser.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            registeredUser.Token = tokenHandler.WriteToken(token);

            // remove password before returning
            registeredUser.Password = null;

            return registeredUser;
        }
    }
}
