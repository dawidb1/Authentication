using Autentication.Data;
using Authentication.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private UserManager<ApplicationUser> userManager;

        public AuthController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public ActionResult GetToken()
        {

            return null; //TODO 
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await this.userManager.FindByNameAsync(model.Username);

            if (await this.IsUserAndPasswordValid(user, model.Password))
            {
                var claims = GetClaims(user.UserName);
                var symetricSecurityKey = GetSymmetricSecurityKey();
                var token = GetToken(symetricSecurityKey, claims);

                return Ok(WritenToken(token));
            }

            return Unauthorized();
        }

        private async Task<bool> IsUserAndPasswordValid(ApplicationUser user, string password)
        {
            return (user != null && await this.userManager.CheckPasswordAsync(user, password));
        }

        private JwtSecurityToken GetToken(SymmetricSecurityKey key, Claim[] claims)
        {
            return new JwtSecurityToken(
                        issuer: "Biometria",
                        audience: "Biometria",
                        expires: DateTime.Now.AddHours(1),
                        claims: claims,
                        signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                    );
        }

        private object WritenToken(JwtSecurityToken token)
        {
            var writenToken = new JwtSecurityTokenHandler().WriteToken(token);

            return new
            {
                token = writenToken,
                expiration = token.ValidTo
            };
        }

        private Claim[] GetClaims(string username)
        {
            return new[]
               {
                    new Claim(JwtRegisteredClaimNames.Sub, username),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };
        }

        private SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            var securityKey = "mySecurityKeydsfasdfasfasfasf.in";
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
        }

        public async Task<IActionResult> AddUser([FromBody] ApplicationUser user)
        {
            user.SecurityStamp = Guid.NewGuid().ToString();
            user.Status = false;

            await userManager.CreateAsync(user);
            await userManager.AddPasswordAsync(user, "admin123");

            return Ok(user);
        }
    }
}