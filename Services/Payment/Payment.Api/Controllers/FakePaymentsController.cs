using MassTransit;
using MicroServiceArchitecture.Shared.ControllerBases;
using MicroServiceArchitecture.Shared.Dtos;
using MicroServiceArchitecture.Shared.Messages;
using Microsoft.AspNetCore.Mvc;
using Payment.Api.Models;
using System.Threading.Tasks;

namespace Payment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FakePaymentsController : CustomBaseController
    {
        #region Fields

        private readonly ISendEndpointProvider _sendEndpointProvider;

        #endregion

        #region Ctor

        public FakePaymentsController(ISendEndpointProvider sendEndpointProvider)
        {
            _sendEndpointProvider = sendEndpointProvider;
        }

        #endregion

        #region Methods

        [HttpPost]
        public async Task<IActionResult> ReceivePayment(PaymentDto paymentDto)
        {
            var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new System.Uri("queue:create-order-service"));

            var createOrderMessageCommand = new CreateOrderMessageCommand
            {
                BuyerId = paymentDto.Order.BuyerId,
                Province = paymentDto.Order.Address.Province,
                District = paymentDto.Order.Address.District,
                Street = paymentDto.Order.Address.Street,
                Line = paymentDto.Order.Address.Line,
                ZipCode = paymentDto.Order.Address.ZipCode
            };

            paymentDto.Order.OrderItems.ForEach(x =>
            {
                createOrderMessageCommand.OrderItems.Add(new OrderItem
                {
                    PictureUrl = x.PictureUrl,
                    Price = x.Price,
                    ProductId = x.ProductId,
                    ProductName = x.ProductName
                });
            });

            await sendEndpoint.Send(createOrderMessageCommand);

            return CreateActionResultInstance(MicroServiceArchitecture.Shared.Dtos.Response<NoContent>.Success(200));
        }

        #endregion
    }
}
