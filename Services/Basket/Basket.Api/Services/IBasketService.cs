using Basket.Api.Dtos;
using MicroServiceArchitecture.Shared.Dtos;
using System.Threading.Tasks;

namespace Basket.Api.Services
{
    public interface IBasketService
    {
        Task<Response<BasketDto>> GetBasketAsync(string userId);

        Task<Response<bool>> SaveOrUpdateAsync(BasketDto basketDto);

        Task<Response<bool>> DeleteAsync(string userId);
    }
}
