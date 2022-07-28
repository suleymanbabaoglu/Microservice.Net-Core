using Microsoft.AspNetCore.Mvc;
using Services.CatalogAPI.Dtos;
using Services.CatalogAPI.Services;
using Shared.ControllerBases;
using System.Threading.Tasks;

namespace Services.CatalogAPI.Controllers
{
    [Route("api/[controller]"), ApiController]
    public class CoursesController : CustomBaseController
    {
        private readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => CreateActionResultInstance(await _courseService.GetAllAsync());

        [HttpGet, Route("get/{id}")]
        public async Task<IActionResult> GetById(string id)
            => CreateActionResultInstance(await _courseService.GetByIdAsync(id));

        [HttpGet, Route("userCourses/{userId}")]
        public async Task<IActionResult> GetAllByUserId(string userId)
            => CreateActionResultInstance(await _courseService.GetAllByUserIdAsync(userId));

        [HttpPost, Route("create")]
        public async Task<IActionResult> Create(CourseCreateDto courseCreateDto)
            => CreateActionResultInstance(await _courseService.CreateAsync(courseCreateDto));

        [HttpPut, Route("update")]
        public async Task<IActionResult> Update(CourseUpdateDto courseUpdateDto)
            => CreateActionResultInstance(await _courseService.UpdateAsync(courseUpdateDto));

        [HttpDelete, Route("delete/{id}")]
        public async Task<IActionResult> Delete(string id)
            => CreateActionResultInstance(await _courseService.DeleteAsync(id));

    }
}
