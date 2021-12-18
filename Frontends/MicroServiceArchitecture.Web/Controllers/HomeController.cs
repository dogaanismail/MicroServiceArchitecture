using MicroServiceArchitecture.Web.Exceptions;
using MicroServiceArchitecture.Web.Models;
using MicroServiceArchitecture.Web.Services.Interfaces;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MicroServiceArchitecture.Web.Controllers
{
    public class HomeController : Controller
    {
        #region Fields
        private readonly ILogger<HomeController> _logger;
        private readonly ICatalogService _catalogService;

        #endregion

        #region Ctor

        public HomeController(ILogger<HomeController> logger,
            ICatalogService catalogService)
        {
            _logger = logger;
            _catalogService = catalogService;
        }

        #endregion

        #region Methods

        public async Task<IActionResult> Index()
        {
            return View(await _catalogService.GetAllCourseAsync());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var errorFeature = HttpContext.Features.Get<IExceptionHandlerFeature>();

            if (errorFeature != null && errorFeature.Error is UnAuthorizeException)
                return RedirectToAction(nameof(AuthController.Logout), "Auth");
 
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Detail(string id)
        {
            return View(await _catalogService.GetByCourseId(id));
        }

        #endregion
    }
}
