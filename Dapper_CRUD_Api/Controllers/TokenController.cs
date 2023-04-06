using Datas.Models;
using Datas.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Dapper_CRUD_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly UserRepository _userRepository;
        public TokenController(IConfiguration config, UserRepository userRepository)
        {
            _configuration = config;
            _userRepository = userRepository;
        }

        [HttpPost]
        public IActionResult GenerateToken([FromBody] UserInfo _userData)
        {

            TokenResponse tokenResponse = new TokenResponse();

            if (_userData != null && _userData.UserName != null && _userData.Password != null)
            {
                var user = GetUser(_userData.UserName, _userData.Password);

                if (user != null)
                {

                    var tokenhandler = new JwtSecurityTokenHandler();
                    var tokenkey = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(
                            new Claim[]
                            {
                             new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                            new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                            new Claim("UserId", user.UserId.ToString()),
                            new Claim(ClaimTypes.Name, user.UserName),
                            new Claim(ClaimTypes.Role, user.Role),
                            }
                        ),
                        Issuer = _configuration["Jwt:Issuer"],
                        Audience = _configuration["Jwt:Audience"],
                        Expires = DateTime.Now.AddMinutes(20),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenkey), SecurityAlgorithms.HmacSha256)
                    };
                    var token = tokenhandler.CreateToken(tokenDescriptor);
                    string finaltoken = tokenhandler.WriteToken(token);

                    tokenResponse.JWTToken = finaltoken;

                    return Ok(tokenResponse);
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return Unauthorized();
            }
        }

        private UserInfo GetUser(string username, string password)
        {
            return _userRepository.GetByuser(username, password);
        }
    }
}
