using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;
using Web.Services.Interfaces;

namespace Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IIdentityService _identityService;

        public AuthController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginInput loginInput)
        {
            if (!ModelState.IsValid)
                return View();

            var response = await _identityService.Login(loginInput);

            if (!response.IsSuccess)
            {
                response.Errors.ForEach(e =>
               {
                   ModelState.AddModelError(string.Empty, e);
               });

                return View();
            }

            return RedirectToAction(nameof(Index), "Home");

        }

    }
}
