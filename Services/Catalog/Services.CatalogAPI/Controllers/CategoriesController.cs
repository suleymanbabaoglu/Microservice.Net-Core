using Microsoft.AspNetCore.Mvc;
using Services.CatalogAPI.Dtos;
using Services.CatalogAPI.Services;
using Shared.ControllerBases;
using System.Threading.Tasks;

namespace Services.CatalogAPI.Controllers
{
    [Route("api/[controller]"), ApiController]
    public class CategoriesController : CustomBaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            CreateActionResultInstance(await _categoryService.GetAllAsync());

        [HttpGet, Route("get/{id}")]
        public async Task<IActionResult> GetById(string id) =>
            CreateActionResultInstance(await _categoryService.GetByIdAsync(id));

        [HttpPost, Route("create")]
        public async Task<IActionResult> Create(CategoryDto categoryDto) =>
            CreateActionResultInstance(await _categoryService.CreateAsync(categoryDto));
    }
}
