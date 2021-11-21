using Dapper;
using MicroServiceArchitecture.Shared.Dtos;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Discount.Api.Services
{
    public class DiscountService : IDiscountService
    {
        #region Fields
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _dbConnection;

        #endregion

        #region Ctor

        public DiscountService(IConfiguration configuration)
        {
            _configuration = configuration;

            _dbConnection = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSql"));
        }

        #endregion

        #region Methods

        public async Task<Response<NoContent>> DeleteAsync(string id)
        {
            var status = await _dbConnection.ExecuteAsync("delete from discount where id = @Id", new { Id = id });

            return status > 0 ? Response<NoContent>.Success(204) : Response<NoContent>.Fail("Discount not found!", 404);
        }

        public async Task<Response<List<Models.Discount>>> GetAllAsync()
        {
            var discounts = await _dbConnection.QueryAsync<Models.Discount>("select * from discount");

            return Response<List<Models.Discount>>.Success(discounts.ToList(), 200);
        }

        public async Task<Response<Models.Discount>> GetByCodeAndUserIdAsync(string code, string userId)
        {
            var discounts = await _dbConnection.QueryAsync<Models.Discount>("select * from discount where userid = @UserId and code = @Code", new
            {
                UserId = userId,
                Code = code
            });

            var hasDiscount = discounts.FirstOrDefault();

            if (hasDiscount == null)
                return Response<Models.Discount>.Fail("Discount not found!", 404);

            return Response<Models.Discount>.Success(hasDiscount, 200);
        }

        public async Task<Response<Models.Discount>> GetByIdAsync(int id)
        {
            var discount = (await _dbConnection.QueryAsync<Models.Discount>("select * from discount where id = @Id", new { Id = id })).SingleOrDefault();

            if (discount == null)
                return Response<Models.Discount>.Fail("Discount not found!", 404);

            return Response<Models.Discount>.Success(discount, 200);
        }

        public async Task<Response<NoContent>> SaveAsync(Models.Discount discount)
        {
            var status = await _dbConnection.ExecuteAsync("INSERT INTO discount(userid,rate,code) VALUES (@UserId, @Rate, @Code)", discount);

            if (status > 0)
                return Response<NoContent>.Success(204);

            return Response<NoContent>.Fail("An error accured while adding", 500);
        }

        public async Task<Response<NoContent>> UpdateAsync(Models.Discount discount)
        {
            var status = await _dbConnection.ExecuteAsync("UPDATE discount set userid = @UserId, code = @Code, rate = @Rate where id = @Id", new
            {
                discount.Id,
                discount.UserId,
                discount.Code,
                discount.Rate
            });

            if (status > 0)
                return Response<NoContent>.Success(204);

            return Response<NoContent>.Fail("Discount not found!", 404);
        }

        #endregion
    }
}
