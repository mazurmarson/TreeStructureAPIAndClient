using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TreeAPI.Dtos;
using TreeAPI.Models;
using TreeAPI.Services;

namespace TreeAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;
        private readonly IConfiguration _config;

        public AuthController(IAuthService service, IConfiguration config)
        {
            _service = service;
            _config = config;
        }


                [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDto userRegisterDto)
        {
            userRegisterDto.Role = "user";
            userRegisterDto.Name = userRegisterDto.Name.ToLower();
            if (await _service.UserIsExist(userRegisterDto.Name))
            {
            //    throw new Exception("Blad reczny");
                return BadRequest("Taki użytkownik już istnieje");
            }
            var userToCreate = new User
            {
                Role = userRegisterDto.Role,
                Name = userRegisterDto.Name,
                Mail = userRegisterDto.Mail
            };
            var createdUser = await _service.Register(
                userToCreate, userRegisterDto.Password);
            return StatusCode(201);
        }

                [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            var userFromRepo = await _service.Login(
                userLoginDto.Name.ToLower(), userLoginDto.Password);
            if (userFromRepo == null)
            {
                return Unauthorized();
            }
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),               //Tutaj w tokenie zapisane jest ID
                new Claim(ClaimTypes.Name, userFromRepo.Name),                                // Tutaj w tokenie jest zapisany login
                new Claim(ClaimTypes.Role, userFromRepo.Role)                                 //Tutaj dodaj stopien pracownika
                
                
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes
            (_config.GetSection("AppSettings:Token").Value));
            
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddHours(8),                                                               //Tu ustawia sie czas jak dlugo ma byc token przewaznie duzo krocej
                SigningCredentials = creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return Ok(new { token = tokenHandler.WriteToken(token) });
        }
    }
}