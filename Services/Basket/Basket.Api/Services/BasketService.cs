using Basket.Api.Dtos;
using MicroServiceArchitecture.Shared.Dtos;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Basket.Api.Services
{
    public class BasketService : IBasketService
    {
        #region Fields
        private readonly RedisService _redisService;

        #endregion

        #region Ctor

        public BasketService(RedisService redisService)
        {
            _redisService = redisService;
        }

        #endregion

        #region Methods

        public async Task<Response<bool>> DeleteAsync(string userId)
        {
            var status = await _redisService.GetDb().KeyDeleteAsync(userId);

            return status ? Response<bool>.Success(204) : Response<bool>.Fail("Basket could not found!", (int)HttpStatusCode.BadRequest);
        }

        public async Task<Response<BasketDto>> GetBasketAsync(string userId)
        {
            var existBasket = await _redisService.GetDb().StringGetAsync(userId);

            if (String.IsNullOrEmpty(existBasket))
                return Response<BasketDto>.Fail("Basket not found!", 404);

            return Response<BasketDto>.Success(JsonSerializer.Deserialize<BasketDto>(existBasket), 200);
        }

        public async Task<Response<bool>> SaveOrUpdateAsync(BasketDto basketDto)
        {
            var status = await _redisService.GetDb().StringSetAsync(basketDto.UserId, JsonSerializer.Serialize(basketDto));

            return status ? Response<bool>.Success(204) : Response<bool>.Fail("Basket could not update or save", 500);
        }

        #endregion
    }
}
