using IdentityServer.Dtos;
using IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignupDto signupDto)
        {
            var user = new ApplicationUser { UserName = signupDto.UserName, Email = signupDto.Email, City = signupDto.City };

            var result = await _userManager.CreateAsync(user, signupDto.Password);

            if (!result.Succeeded)
                return BadRequest(Response<NoContent>.Fail(result.Errors.Select(e => e.Description).ToList(), 400));

            return NoContent();


        }
    }
}
