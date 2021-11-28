using MicroServiceArchitecture.Shared.ControllerBases;
using MicroServiceArchitecture.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Payment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FakePaymentsController : CustomBaseController
    {
        [HttpPost]
        public IActionResult ReceivePayment()
        {
            return CreateActionResultInstance(Response<NoContent>.Success(200));
        }
    }
}
