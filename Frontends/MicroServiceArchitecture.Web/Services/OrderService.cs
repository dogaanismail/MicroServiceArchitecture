using MicroServiceArchitecture.Shared.Dtos;
using MicroServiceArchitecture.Shared.Services;
using MicroServiceArchitecture.Web.Models.Orders;
using MicroServiceArchitecture.Web.Models.Payments;
using MicroServiceArchitecture.Web.Services.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MicroServiceArchitecture.Web.Services
{
    public class OrderService : IOrderService
    {
        #region Fields

        private readonly IPaymentService _paymentService;
        private readonly HttpClient _httpClient;
        private readonly IBasketService _basketService;
        private readonly ISharedIdentityService _sharedIdentityService;

        #endregion

        #region Ctor

        public OrderService(IPaymentService paymentService,
            HttpClient httpClient,
            IBasketService basketService,
            ISharedIdentityService sharedIdentityService)
        {
            _paymentService = paymentService;
            _httpClient = httpClient;
            _basketService = basketService;
            _sharedIdentityService = sharedIdentityService;
        }

        #endregion

        #region Methods

        public async Task<OrderCreatedViewModel> CreateOrderAsync(CheckoutInfoInput checkoutInfoInput)
        {
            var basket = await _basketService.GetAsync();

            var payment = new PaymentInfoInput()
            {
                CardName = checkoutInfoInput.CardName,
                CardNumber = checkoutInfoInput.CardNumber,
                CVV = checkoutInfoInput.CVV,
                Expiration = checkoutInfoInput.Expiration,
                TotalPrice = basket.TotalPrice
            };

            var responsePayment = await _paymentService.ReceivePaymentAsync(payment);

            if (!responsePayment)
                return new OrderCreatedViewModel() { Error = "Odeme alinamadi", IsSuccessful = false };

            var orderCreateInput = new OrderCreateInput()
            {
                BuyerId = _sharedIdentityService.GetUserId,
                Address = new AddressCreateInput
                {
                    Province = checkoutInfoInput.Province,
                    District = checkoutInfoInput.District,
                    Street = checkoutInfoInput.Street,
                    Line = checkoutInfoInput.Line,
                    ZipCode = checkoutInfoInput.ZipCode
                }
            };

            basket.BasketItems.ForEach(x =>
            {
                var orderItem = new OrderItemCreateInput()
                {
                    ProductId = x.CourseId,
                    Price = x.Price,
                    PictureUrl = "",
                    ProductName = x.CourseName
                };

                orderCreateInput.OrderItems.Add(orderItem);
            });

            var response = await _httpClient.PostAsJsonAsync("orders", orderCreateInput);

            if (!response.IsSuccessStatusCode)
                return new OrderCreatedViewModel() { Error = "Sipariş oluşturulamadı", IsSuccessful = false };

            var orderCreatedViewModel = await response.Content.ReadFromJsonAsync<Response<OrderCreatedViewModel>>();

            orderCreatedViewModel.Data.IsSuccessful = true;

            await _basketService.DeleteAsync();
            return orderCreatedViewModel.Data;
        }

        public async Task<List<OrderViewModel>> GetOrderAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<Response<List<OrderViewModel>>>("orders");

            return response.Data;
        }

        public async Task<OrderSuspendViewModel> SuspendOrderAsync(CheckoutInfoInput checkoutInfoInput)
        {
            var basket = await _basketService.GetAsync();

            var orderCreateInput = new OrderCreateInput()
            {
                BuyerId = _sharedIdentityService.GetUserId,
                Address = new AddressCreateInput
                {
                    Province = checkoutInfoInput.Province,
                    District = checkoutInfoInput.District,
                    Street = checkoutInfoInput.Street,
                    Line = checkoutInfoInput.Line,
                    ZipCode = checkoutInfoInput.ZipCode
                }
            };

            basket.BasketItems.ForEach(x =>
            {
                var orderItem = new OrderItemCreateInput()
                {
                    ProductId = x.CourseId,
                    Price = x.Price,
                    PictureUrl = "",
                    ProductName = x.CourseName
                };

                orderCreateInput.OrderItems.Add(orderItem);
            });

            var payment = new PaymentInfoInput()
            {
                CardName = checkoutInfoInput.CardName,
                CardNumber = checkoutInfoInput.CardNumber,
                CVV = checkoutInfoInput.CVV,
                Expiration = checkoutInfoInput.Expiration,
                TotalPrice = basket.TotalPrice,
                Order = orderCreateInput
            };

            var responsePayment = await _paymentService.ReceivePaymentAsync(payment);

            if (!responsePayment)       
                return new OrderSuspendViewModel() { Error = "Ödeme alınamadı", IsSuccessful = false };
            
            await _basketService.DeleteAsync();

            return new OrderSuspendViewModel() { IsSuccessful = true };
        }

        #endregion
    }
}
