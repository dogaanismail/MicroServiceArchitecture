using MicroServiceArchitecture.Web.Models.Payments;
using MicroServiceArchitecture.Web.Services.Interfaces;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MicroServiceArchitecture.Web.Services
{
    public class PaymentService : IPaymentService
    {
        #region Fields
        private readonly HttpClient _httpClient;

        #endregion

        #region Ctor

        public PaymentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        #endregion

        #region Methods

        public async Task<bool> ReceivePaymentAsync(PaymentInfoInput paymentInfoInput)
        {
            var response = await _httpClient.PostAsJsonAsync("fakepayments", paymentInfoInput);

            return response.IsSuccessStatusCode;
        }

        #endregion
    }
}
