using Autentication.Data;
using Authentication.Model;
using Authentication.Model.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await userManager.FindByNameAsync(model.Username);

            if (await IsUserAndPasswordValid(user, model.Password))
            {
                var token = new TokenHandler().GetToken(user.UserName);
                return Ok(token);
            }

            return Unauthorized();
        }


        private async Task<bool> IsUserAndPasswordValid(ApplicationUser user, string password)
        {
            return (user != null && await userManager.CheckPasswordAsync(user, password));
        }
    }
}