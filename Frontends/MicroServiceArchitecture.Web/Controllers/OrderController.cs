using MicroServiceArchitecture.Web.Models.Orders;
using MicroServiceArchitecture.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace MicroServiceArchitecture.Web.Controllers
{
    public class OrderController : Controller
    {
        #region Fields
        private readonly IBasketService _basketService;
        private readonly IOrderService _orderService;

        #endregion

        #region Ctor

        public OrderController(IBasketService basketService,
            IOrderService orderService)
        {
            _basketService = basketService;
            _orderService = orderService;
        }

        #endregion

        #region Methods

        public async Task<IActionResult> Checkout()
        {
            var basket = await _basketService.GetAsync();

            ViewBag.basket = basket;
            return View(new CheckoutInfoInput());
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(CheckoutInfoInput checkoutInfoInput)
        {
            //1. yol senkron iletişim
            //  var orderStatus = await _orderService.CreateOrder(checkoutInfoInput);

            // 2.yol asenkron iletişim
            var orderSuspend = await _orderService.SuspendOrderAsync(checkoutInfoInput);

            if (!orderSuspend.IsSuccessful)
            {
                var basket = await _basketService.GetAsync();

                ViewBag.basket = basket;

                ViewBag.error = orderSuspend.Error;

                return View();
            }

            //1. yol senkron iletişim
            //  return RedirectToAction(nameof(SuccessfulCheckout), new { orderId = orderStatus.OrderId });

            //2.yol asenkron iletişim
            return RedirectToAction(nameof(SuccessfulCheckout), new { orderId = new Random().Next(1, 1000) });
        }

        public IActionResult SuccessfulCheckout(int orderId)
        {
            ViewBag.orderId = orderId;
            return View();
        }

        public async Task<IActionResult> CheckoutHistory()
        {
            return View(await _orderService.GetOrderAsync());
        }

        #endregion
    }
}
