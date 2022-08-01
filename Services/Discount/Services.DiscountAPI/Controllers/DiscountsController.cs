using Microsoft.AspNetCore.Mvc;
using Services.DiscountAPI.Models;
using Services.DiscountAPI.Services;
using Shared.ControllerBases;
using Shared.Services;
using System.Threading.Tasks;

namespace Services.DiscountAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountsController : CustomBaseController
    {
        private readonly IDiscountService _discountService;
        private readonly ISharedIdentityService _sharedIdentityService;

        public DiscountsController(IDiscountService discountService, ISharedIdentityService sharedIdentityService)
        {
            _discountService = discountService;
            _sharedIdentityService = sharedIdentityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => CreateActionResultInstance(await _discountService.GetAll());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) => CreateActionResultInstance(await _discountService.GetById(id));

        [HttpGet,Route("[action]/{code}")]
        public async Task<IActionResult> GetByCode(string code) =>
            CreateActionResultInstance(await _discountService.GetByCodeAndUserId(code, _sharedIdentityService.GetUserId));

        [HttpPost]
        public async Task<IActionResult> Save(Discount discount) => CreateActionResultInstance(await _discountService.Save(discount));

        [HttpPut]
        public async Task<IActionResult> Update(Discount discount) => CreateActionResultInstance(await _discountService.Update(discount));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) => CreateActionResultInstance(await _discountService.Delete(id));



    }
}
