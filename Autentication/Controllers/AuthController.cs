using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Autentication.Data;
using Authentication.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

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

            if (user != null && await this.userManager.CheckPasswordAsync(user, model.Password))
            {

                //generate claims 
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                // security key 
                var securityKey = "mySecurityKeydsfasdfasfasfasf.in";
                var symetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));

                // create token
                var token = new JwtSecurityToken(
                        issuer: "Biometria",
                        audience: "Biometria",
                        expires: DateTime.Now.AddHours(1),
                        claims: claims,
                        signingCredentials: new SigningCredentials(symetricSecurityKey, SecurityAlgorithms.HmacSha256)
                    );

                var writenToken = new JwtSecurityTokenHandler().WriteToken(token);

                return Ok(new
                {
                    token = writenToken,
                    expiration = token.ValidTo
                });
            }

            return Unauthorized();
        }
    }
}