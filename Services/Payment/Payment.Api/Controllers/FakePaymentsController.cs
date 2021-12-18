using MicroServiceArchitecture.Shared.ControllerBases;
using MicroServiceArchitecture.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;
using Payment.Api.Models;

namespace Payment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FakePaymentsController : CustomBaseController
    {
        [HttpPost]
        public IActionResult ReceivePayment(PaymentDto paymentDto)
        {
            return CreateActionResultInstance(Response<NoContent>.Success(200));
        }
    }
}
