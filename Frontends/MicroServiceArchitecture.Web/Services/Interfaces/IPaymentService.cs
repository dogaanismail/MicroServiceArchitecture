using MicroServiceArchitecture.Web.Models.Payments;
using System.Threading.Tasks;

namespace MicroServiceArchitecture.Web.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<bool> ReceivePaymentAsync(PaymentInfoInput paymentInfoInput);
    }
}
