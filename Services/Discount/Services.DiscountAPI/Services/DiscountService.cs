using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Services.DiscountAPI.Models;
using Shared.Dtos;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Services.DiscountAPI.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _dbConnection;

        public DiscountService(IConfiguration configuration)
        {
            _configuration = configuration;
            _dbConnection = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSql"));
        }

        public async Task<Response<List<Discount>>> GetAll()
        {
            var discounts = await _dbConnection.QueryAsync<Discount>("Select * from discount");

            return Response<List<Discount>>.Success(discounts.ToList(), 200);
        }

        public async Task<Response<Discount>> GetById(int id)
        {
            var discount = (await _dbConnection.QueryAsync<Discount>("select * from discount where id=@Id",
                new { Id = id })).SingleOrDefault();

            if (discount is null)
                return Response<Discount>.Fail("Discount not found", 404);

            return Response<Discount>.Success(discount, 200);
        }

        public async Task<Response<NoContent>> Save(Discount discount)
        {
            var saveStatus = await _dbConnection.ExecuteAsync("insert into discount (userid,rate,code) values (@UserId,@Rate,@Code)", discount);

            if (saveStatus > 0)
                return Response<NoContent>.Success(204);

            return Response<NoContent>.Fail("an error occured while adding", 500);
        }

        public async Task<Response<NoContent>> Update(Discount discount)
        {
            var updateStatus = await _dbConnection.ExecuteAsync("update discount set userid=@UserId,rate=@Rate,code=@Code where id=@Id", discount);

            return updateStatus > 0 ? Response<NoContent>.Success(204) : Response<NoContent>.Fail("Discount not found", 404);
        }

        public async Task<Response<NoContent>> Delete(int id)
        {
            var status = await _dbConnection.ExecuteAsync("delete from discount where id=@Id", new { Id = id });

            return status > 0 ? Response<NoContent>.Success(204) : Response<NoContent>.Fail("Discount not found", 404);
        }
        public async Task<Response<Discount>> GetByCodeAndUserId(string code, string userId)
        {
            var discount = await _dbConnection.QueryFirstAsync<Discount>("select * from discount where code=@Code and userid=@UserId", new { Code = code, UserId = userId });

            return discount is null ? Response<Discount>.Fail("Discount not found", 404) : Response<Discount>.Success(discount, 200);
        }
    }
}
