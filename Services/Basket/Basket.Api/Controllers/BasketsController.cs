using Basket.Api.Dtos;
using Basket.Api.Services;
using MicroServiceArchitecture.Shared.ControllerBases;
using MicroServiceArchitecture.Shared.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Basket.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketsController : CustomBaseController
    {
        #region Fields
        private readonly IBasketService _basketService;
        private readonly ISharedIdentityService _sharedIdentityService;

        #endregion

        #region Ctor

        public BasketsController(IBasketService basketService,
        ISharedIdentityService sharedIdentityService)
        {
            _basketService = basketService;
            _sharedIdentityService = sharedIdentityService;
        }

        #endregion

        #region Method

        [HttpGet]
        public async Task<IActionResult> GetBasketAsync()
        {
            return CreateActionResultInstance(await _basketService.GetBasketAsync(_sharedIdentityService.GetUserId));
        }

        [HttpPost]
        public async Task<IActionResult> SaveOrUpdateAsync(BasketDto basketDto)
        {
            var response = await _basketService.SaveOrUpdateAsync(basketDto);

            return CreateActionResultInstance(response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync()
        {
            return CreateActionResultInstance(await _basketService.DeleteAsync(_sharedIdentityService.GetUserId));
        }

        #endregion
    }
}
