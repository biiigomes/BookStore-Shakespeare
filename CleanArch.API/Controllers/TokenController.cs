using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CleanArch.API.Models;
using CleanArch.Domain.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;

namespace CleanArch.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IAuthenticate _authentication;
        private readonly IConfiguration _configuration;

        public TokenController(IAuthenticate authentication, IConfiguration configuration)
        {
            _authentication = authentication ??
                throw new ArgumentNullException(nameof(authentication));
                
            _configuration = configuration;
        }

        [HttpPost("CreateUser")]
        [Authorize]
        public async Task<ActionResult> CreateUser([FromForm] LoginModel loginModel)
        {
            var result = await _authentication.RegisterUser(loginModel.Email, loginModel.Password);
            if(result)
            {
                return Ok($"User {loginModel.Email} login successfully");
            }
            else 
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return BadRequest(ModelState);
            }
        }

        [AllowAnonymous]
        [HttpPost("LoginUser")]
        public async Task<ActionResult<UserToken>> Login([FromForm] LoginModel loginModel)
        {
            var result = await _authentication.Authenticate(loginModel.Email, loginModel.Password);

            if(result)
            {
                return GenerateToken(loginModel);
                
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt");
                return BadRequest(ModelState);
            }
        }

        private UserToken GenerateToken(LoginModel loginModel)
        {
            //declarações do usuario
            var claims = new[]
            {
                new Claim("email", loginModel.Email),
                new Claim("meuvalor", "qualquercoisaae"), 
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            //chave privada
            var privateKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));

            //assinatura digital
            var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);

            //definir tempo de expiracao
            var expiration = DateTime.UtcNow.AddMinutes(10);

            //gerar token
            JwtSecurityToken token = new JwtSecurityToken(
                //emissor
                issuer: _configuration["Jwt:Issuer"],
                //audiencia
                audience: _configuration["Jwt:Audience"],
                //claims
                claims: claims,
                //data de expiracao
                expires: expiration,
                //assinatura digital
                signingCredentials: credentials
            );

            return new UserToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }

    }
}