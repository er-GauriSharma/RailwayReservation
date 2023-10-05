using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Railway_Reservation.Model;
using Railway_Reservation.Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Railway_Reservation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private IConfiguration configuration;
        private IAdminInterface context1;
        private IUserInterface context2;
        private ILogger<AuthenticationController> logger;
        public AuthenticationController(IConfiguration configuration, IAdminInterface context1, IUserInterface context2)
        {
            this.configuration = configuration;
            this.context1 = context1;
            this.context2 = context2;
        }

        [HttpPost("Login")]
        public IActionResult UserLogin(Admin admin)
        {
            var user1 = context2.Authenticate(admin.PhoneNo, admin.Password);
            if (user1 == null)
            {
                return Unauthorized();
            }
            else
            {
                var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user1.UserId.ToString()),
                new Claim(ClaimTypes.Role, "User")
            };

                var token = GenerateJwtToken(claims);
                //logger.LogInformation("User loggedin");
                return Ok(new { token });
            }
        }

        [HttpPost("Admin/Login")]
        public IActionResult AdminLogin(Admin user)
        {
            var admin1 = context1.Authenticate(user.PhoneNo, user.Password);
            if (admin1 == null)
            {
                return Unauthorized();
            }
            else
            {
                var claims = new[]
            {
                new Claim(ClaimTypes.MobilePhone, admin1.PhoneNo),
                new Claim(ClaimTypes.Role, "Admin")
            };

                var token = GenerateJwtToken(claims);
                //logger.LogInformation("Admin loggedin");
                return Ok(new { token });
            }
        }

        private object GenerateJwtToken(Claim[] claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
