using MicroServiceArchitecture.Shared.Dtos;
using MicroServiceArchitecture.Web.Models.Discounts;
using MicroServiceArchitecture.Web.Services.Interfaces;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MicroServiceArchitecture.Web.Services
{
    public class DiscountService : IDiscountService
    {
        #region Fields
        private readonly HttpClient _httpClient;

        #endregion

        #region Ctor

        public DiscountService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        #endregion

        #region Methods

        public async Task<DiscountViewModel> GetDiscountAsync(string discountCode)
        {
            var response = await _httpClient.GetAsync($"discounts/GetByCode/{discountCode}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var discount = await response.Content.ReadFromJsonAsync<Response<DiscountViewModel>>();

            return discount.Data;
        }

        #endregion
    }
}
