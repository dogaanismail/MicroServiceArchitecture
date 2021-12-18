using MicroServiceArchitecture.Web.Models.Baskets;
using MicroServiceArchitecture.Web.Models.Discounts;
using MicroServiceArchitecture.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace MicroServiceArchitecture.Web.Controllers
{
    [Authorize]
    public class BasketController : Controller
    {
        #region Fields
        private readonly ICatalogService _catalogService;
        private readonly IBasketService _basketService;

        #endregion

        #region Ctor

        public BasketController(ICatalogService catalogService,
            IBasketService basketService)
        {
            _catalogService = catalogService;
            _basketService = basketService;
        }

        #endregion

        #region Methods

        public async Task<IActionResult> Index()
        {
            return View(await _basketService.GetAsync());
        }

        public async Task<IActionResult> AddBasketItem(string courseId)
        {
            var course = await _catalogService.GetByCourseId(courseId);

            var basketItem = new BasketItemViewModel { CourseId = course.Id, CourseName = course.Name, Price = course.Price };

            await _basketService.AddBasketItemAsync(basketItem);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> RemoveBasketItem(string courseId)
        {
            var result = await _basketService.RemoveBasketItemAsync(courseId);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ApplyDiscount(DiscountApplyInput discountApplyInput)
        {
            if (!ModelState.IsValid)
            {
                TempData["discountError"] = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).First();
                return RedirectToAction(nameof(Index));
            }
            var discountStatus = await _basketService.ApplyDiscountAsync(discountApplyInput.Code);

            TempData["discountStatus"] = discountStatus;
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> CancelApplyDiscount()
        {
            await _basketService.CancelApplyDiscountAsync();
            return RedirectToAction(nameof(Index));
        }

        #endregion
    }
}
