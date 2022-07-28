using Services.CatalogAPI.Dtos;
using Services.CatalogAPI.Models;
using Shared.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.CatalogAPI.Services
{
    public interface ICategoryService
    {
        Task<Response<List<CategoryDto>>> GetAllAsync();
        Task<Response<CategoryDto>> CreateAsync(CategoryDto categoryDto);
        Task<Response<CategoryDto>> GetByIdAsync(string id);

    }
}
