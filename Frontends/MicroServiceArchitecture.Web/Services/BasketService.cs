using MicroServiceArchitecture.Shared.Dtos;
using MicroServiceArchitecture.Web.Models.Baskets;
using MicroServiceArchitecture.Web.Services.Interfaces;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MicroServiceArchitecture.Web.Services
{
    public class BasketService : IBasketService
    {
        #region Fields
        private readonly HttpClient _httpClient;
        private readonly IDiscountService _discountService;

        #endregion

        #region Ctor

        public BasketService(HttpClient httpClient,
            IDiscountService discountService)
        {
            _httpClient = httpClient;
            _discountService = discountService;
        }

        #endregion

        #region Methods

        public async Task AddBasketItemAsync(BasketItemViewModel basketItemViewModel)
        {
            var basket = await GetAsync();

            if (basket != null)
            {
                if (!basket.BasketItems.Any(x => x.CourseId == basketItemViewModel.CourseId))
                {
                    basket.BasketItems.Add(basketItemViewModel);
                }
            }
            else
            {
                basket = new BasketViewModel();

                basket.BasketItems.Add(basketItemViewModel);
            }

            await SaveOrUpdateAsync(basket);
        }

        public async Task<bool> ApplyDiscountAsync(string discountCode)
        {
            await CancelApplyDiscountAsync();

            var basket = await GetAsync();
            if (basket == null)
            {
                return false;
            }

            var hasDiscount = await _discountService.GetDiscountAsync(discountCode);
            if (hasDiscount == null)
            {
                return false;
            }

            basket.ApplyDiscount(hasDiscount.Code, hasDiscount.Rate);
            await SaveOrUpdateAsync(basket);
            return true;
        }

        public async Task<bool> CancelApplyDiscountAsync()
        {
            var basket = await GetAsync();

            if (basket == null || basket.DiscountCode == null)
            {
                return false;
            }

            basket.CancelDiscount();
            await SaveOrUpdateAsync(basket);
            return true;
        }

        public async Task<bool> DeleteAsync()
        {
            var result = await _httpClient.DeleteAsync("baskets");

            return result.IsSuccessStatusCode;
        }

        public async Task<BasketViewModel> GetAsync()
        {
            var response = await _httpClient.GetAsync("baskets");

            if (!response.IsSuccessStatusCode)
                return null;

            var basketViewModel = await response.Content.ReadFromJsonAsync<Response<BasketViewModel>>();

            return basketViewModel.Data;
        }

        public async Task<bool> RemoveBasketItemAsync(string courseId)
        {
            var basket = await GetAsync();

            if (basket == null)
                return false;

            var deleteBasketItem = basket.BasketItems.FirstOrDefault(x => x.CourseId == courseId);

            if (deleteBasketItem == null)
                return false;

            var deleteResult = basket.BasketItems.Remove(deleteBasketItem);

            if (!deleteResult)
                return false;

            if (!basket.BasketItems.Any())
                basket.DiscountCode = null;

            return await SaveOrUpdateAsync(basket);
        }

        public async Task<bool> SaveOrUpdateAsync(BasketViewModel basketViewModel)
        {
            var response = await _httpClient.PostAsJsonAsync("baskets", basketViewModel);

            return response.IsSuccessStatusCode;
        }

        #endregion
    }
}
