using MicroServiceArchitecture.IdentityServer.Dtos;
using MicroServiceArchitecture.IdentityServer.Models;
using MicroServiceArchitecture.Shared.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using static IdentityServer4.IdentityServerConstants;

namespace MicroServiceArchitecture.IdentityServer.Controllers
{
    [Authorize(LocalApi.PolicyName)]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        #region Fields
        private readonly UserManager<ApplicationUser> _userManager;

        #endregion

        #region Ctor

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        #endregion

        #region Methods

        [HttpPost]
        public async Task<IActionResult> SignUpAsync(SignupDto signupDto)
        {
            var user = new ApplicationUser
            {
                UserName = signupDto.Username,
                Email = signupDto.Email,
                City = signupDto.City
            };

            var result = await _userManager.CreateAsync(user, signupDto.Password);

            if (!result.Succeeded)
                return BadRequest(Response<NoContent>.Fail(result.Errors.Select(x => x.Description).ToList(), 400));

            return NoContent();
        }

        #endregion
    }
}
