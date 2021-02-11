using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ViverBackend.Entities;
using ViverBackend.Entities.Models;
using ViverBackend.Payloads;
using BC = BCrypt.Net.BCrypt;

namespace ViverBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IConfiguration Config { get; }
        private readonly ViverContext _db;

        public AccountController(ViverContext db, IConfiguration configuration)
        {
            Config = configuration;
            _db = db;
        }

        [AllowAnonymous]
        [EnableCors]
        [HttpPost("register")]

        public async Task<IActionResult> Register([FromBody] RegisterPayload registerPayload)
        {
            try
            {
                var existingUserWithMail = _db.Users
             .Any(u => u.Email == registerPayload.Email);

                if (existingUserWithMail)
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    var userToCreate = new User
                    {
                        Email = registerPayload.Email,
                        FirstName = registerPayload.FirstName,
                        LastName = registerPayload.LastName,
                        PasswordHash = BC.HashPassword(registerPayload.Password),
                        Role = "SimpleUser",
                    };

                    _db.Users.Add(userToCreate);

                    _db.SaveChanges();

                    return Ok(new { status = true, user = userToCreate });
                }
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginPayload loginPayload)
        {
            var foundUser = _db.Users
                .SingleOrDefault(u => u.Email == loginPayload.Email);

            if (foundUser != null)
            {
                if (BC.Verify(loginPayload.Password, foundUser.PasswordHash))
                {
                    var tokenString = GenerateJSONWebToken(foundUser);
                    return Ok(new { status = true, token = tokenString });
                }

                return BadRequest(new { status = false, message = "Wrong password or email" });
            }
            else
            {
                return BadRequest(new { status = false, message = "No user with this email found" });
            }

        }

        private string GenerateJSONWebToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("Role", user.Role),
            };

            var token = new JwtSecurityToken(Config["Jwt:Issuer"],
              Config["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddDays(30),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
