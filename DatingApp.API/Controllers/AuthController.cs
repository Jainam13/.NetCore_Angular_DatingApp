using Microsoft.AspNetCore.Mvc;
using DatingApp.API.RepositoryInterface;
using System.Threading.Tasks;
using DatingApp.API.Models;
using DatingApp.API.Dtos;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using System;
using System.IdentityModel.Tokens.Jwt;
using DatingApp.API.Interface;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepo;
        private readonly ITokenService _tokenservice;

        public AuthController(IAuthRepository authRepo, ITokenService tokenService)
        {
            _authRepo = authRepo;
            _tokenservice = tokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(UserForRegisterDto userRegisterDto)
        {
            userRegisterDto.Username = userRegisterDto.Username.ToLower();

            if (await _authRepo.UserExists(userRegisterDto.Username))
            {
                return BadRequest("Username already exists");
            }

            var newUser = new User
            {
                UserName = userRegisterDto.Username
            };

            var userCreated = await _authRepo.Register(newUser, userRegisterDto.Password);

            return new UserDto
            {
                Username = userCreated.UserName,
                Token = _tokenservice.CreateToken(userCreated)
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(UserForLoginDto userForLoginDto)
        {
            var userFromRepo = await _authRepo.Login(userForLoginDto.Username.ToLower(), userForLoginDto.Password);

            if (userFromRepo == null)
            {
                return Unauthorized("Invalid User");
            }

            return new UserDto
            {
                Username = userFromRepo.UserName,
                Token = _tokenservice.CreateToken(userFromRepo)
            };
        }
    }
}