using Clean.Architecture.Api.Models;
using Clean.Architecture.Domain.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Architecture.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IAuthenticate _authentication;
        private readonly IConfiguration _configuration;

        public TokenController(IAuthenticate authentication, IConfiguration configuration)
        {
            _authentication = authentication ?? throw new ArgumentNullException(nameof(authentication));
            _configuration = configuration;
        }



        [HttpPost("CreateUser")]
        [ApiExplorerSettings(IgnoreApi = true)] // usado pra esconder do swagger a creacao do usuario
        public async Task<ActionResult> CreateUser([FromBody] LoginModel model)
        {
            var result = await _authentication.RegisterUser(model.Email, model.Password);
            if (result)
                return Ok($"User {model.Email} was create sucessfully");
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return BadRequest(ModelState);
            }

        }
            
        [HttpPost("LoginUser")]
        public async Task<ActionResult<UserTokens>> Login([FromBody] LoginModel model)
        {
            var result = await _authentication.Authenticate(model.Email, model.Password);
            if (result)
                return GenerateToken(model);
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return BadRequest(ModelState);
            }
        }

        private UserTokens GenerateToken(LoginModel model)
        {
            var clains = new[]
            {
                new Claim("email", model.Email),
                new Claim("meuValor", "oQueEuQuiser"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var privateKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));

            var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddMinutes(10);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: clains,
                expires: expiration,
                signingCredentials: credentials
                );

            return new UserTokens()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }
    }
}
