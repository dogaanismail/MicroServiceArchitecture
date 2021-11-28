using MediatR;
using MicroServiceArchitecture.Shared.ControllerBases;
using MicroServiceArchitecture.Shared.Services;
using Microsoft.AspNetCore.Mvc;
using Order.Application.Commands;
using Order.Application.Queries;
using System.Threading.Tasks;

namespace Order.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : CustomBaseController
    {
        #region Fields
        private readonly IMediator _mediator;
        private readonly ISharedIdentityService _sharedIdentityService;

        #endregion

        #region Ctor

        public OrdersController(IMediator mediator,
            ISharedIdentityService sharedIdentityService)
        {
            _mediator = mediator;
            _sharedIdentityService = sharedIdentityService;
        }

        #endregion

        #region Methods

        [HttpGet]
        public async Task<IActionResult> GetOrdersAsync()
        {
            var response = await _mediator.Send(new GetOrdersByUserIdQuery { UserId = _sharedIdentityService.GetUserId });

            return CreateActionResultInstance(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrderAsync(CreateOrderCommand createOrderCommand)
        {
            var response = await _mediator.Send(createOrderCommand);

            return CreateActionResultInstance(response);
        }

        #endregion
    }
}
