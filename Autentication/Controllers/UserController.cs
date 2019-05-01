using Autentication.Data;
using Authentication.Model;
using Authentication.Model.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserManager<ApplicationUser> userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        [Authorize]
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddUser([FromBody] AddUserModel addUser)
        {
            var appUser = new UserMapper().AddUserToApplicationUser(addUser);

            await userManager.CreateAsync(appUser);
            await userManager.AddPasswordAsync(appUser, addUser.Password);

            return Ok(appUser);
        }

        [Authorize]
        [HttpPut]
        [Route("edit")]
        public async Task<IActionResult> EditUser([FromBody] EditUserModel editUser)
        {
            var oldUser = await userManager.FindByIdAsync(editUser.Id);
            var editedUser = EditApplicationUser(oldUser, editUser);

            await userManager.UpdateAsync(editedUser);
            return Ok(editUser);
        }

        [Authorize]
        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteUser([FromBody] string deleteUserId)
        {
            var user = await userManager.FindByIdAsync(deleteUserId);
            await userManager.DeleteAsync(user);
            return Ok();
        }

        private ApplicationUser EditApplicationUser(ApplicationUser appUser, EditUserModel editUser)
        {
            appUser.UserName = editUser.Login;

            return appUser;
        }
    }
}