using MicroServiceArchitecture.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MicroServiceArchitecture.Web.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        #region Fields
        private readonly IUserService _userService;

        #endregion

        #region Ctor

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        #endregion

        #region Methods

        public async Task<IActionResult> Index()
        {
            return View(await _userService.GetUserAsync());
        }

        #endregion
    }
}
