using MicroServiceArchitecture.Web.Models;
using MicroServiceArchitecture.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace MicroServiceArchitecture.Web.Controllers
{
    public class AuthController : Controller
    {
        #region Fields
        private readonly IIdentityService _identityService;

        #endregion

        #region Ctor

        public AuthController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        #endregion

        #region Methods

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignInAsync(SigninInput signinInput)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var response = await _identityService.SignInAsync(signinInput);

            if (!response.IsSuccessful)
            {
                response.Errors.ForEach(x =>
                {
                    ModelState.AddModelError(String.Empty, x);
                });

                return View();
            }

            return RedirectToAction(nameof(Index), "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await _identityService.RevokeRefreshTokenAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        #endregion
    }
}
