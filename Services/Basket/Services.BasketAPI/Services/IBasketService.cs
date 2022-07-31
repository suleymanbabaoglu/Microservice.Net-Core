using Services.BasketAPI.Dtos;
using Shared.Dtos;
using System.Threading.Tasks;

namespace Services.BasketAPI.Services
{
    public interface IBasketService
    {
        Task<Response<BasketDto>> GetBasket(string userId);
        Task<Response<bool>> SaveOrUpdate(BasketDto basketDto);
        Task<Response<bool>> Delete(string userId);

    }
}
