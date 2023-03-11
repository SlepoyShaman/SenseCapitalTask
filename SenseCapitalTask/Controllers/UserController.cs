using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SenseCapitalTask.Models;
using SenseCapitalTask.Models.ViewModels;

namespace SenseCapitalTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody]RegisterViewModel model)
        {
            var user = new User() { UserName = model.Login };

            var result = await _userManager.CreateAsync(user, model.Password);

            if(result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return Ok();
            }

            return BadRequest(new { Error = "Register failed"});
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody]RegisterViewModel model)
        {
            var result = await _signInManager
                .PasswordSignInAsync(model.Login, model.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return Ok();
            }

            return BadRequest(new { Error = "Invalid username or password" });
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }
    }
}
