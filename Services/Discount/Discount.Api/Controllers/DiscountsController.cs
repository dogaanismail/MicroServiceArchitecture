using Discount.Api.Services;
using MicroServiceArchitecture.Shared.ControllerBases;
using MicroServiceArchitecture.Shared.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Discount.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountsController : CustomBaseController
    {
        #region Fields
        private readonly IDiscountService _discountService;
        private readonly ISharedIdentityService _sharedIdentityService;

        #endregion

        #region Ctor

        public DiscountsController(IDiscountService discountService,
            ISharedIdentityService sharedIdentityService)
        {
            _discountService = discountService;
            _sharedIdentityService = sharedIdentityService;
        }

        #endregion

        #region Methods

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return CreateActionResultInstance(await _discountService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var discount = await _discountService.GetByIdAsync(id);

            return CreateActionResultInstance(discount);
        }

        [HttpGet]
        [Route("/api/[controller]/GetByCode/{code}")]
        public async Task<IActionResult> GetByCodeAsync(string code)
        {
            var discount = await _discountService.GetByCodeAndUserIdAsync(code, _sharedIdentityService.GetUserId);

            return CreateActionResultInstance(discount);
        }

        [HttpPost]
        public async Task<IActionResult> SaveAsync(Models.Discount discount)
        {
            return CreateActionResultInstance(await _discountService.SaveAsync(discount));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(Models.Discount discount)
        {
            return CreateActionResultInstance(await _discountService.UpdateAsync(discount));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            return CreateActionResultInstance(await _discountService.DeleteAsync(id));
        }

        #endregion
    }
}
