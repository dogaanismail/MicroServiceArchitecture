using MicroServiceArchitecture.Shared.Services;
using MicroServiceArchitecture.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MicroServiceArchitecture.Web.Controllers
{
    [Authorize]
    public class CoursesController : Controller
    {
        #region Fields
        private readonly ICatalogService _catalogService;
        private readonly ISharedIdentityService _sharedIdentityService;

        #endregion

        #region Ctor

        public CoursesController(ICatalogService catalogService,
            ISharedIdentityService sharedIdentityService)
        {
            _catalogService = catalogService;
            _sharedIdentityService = sharedIdentityService;
        }

        #endregion

        #region Methods

        public async Task<IActionResult> Index()
        {
            return View(await _catalogService.GetAllCourseByUserIdAsync(_sharedIdentityService.GetUserId));
        }

        #endregion
    }
}
